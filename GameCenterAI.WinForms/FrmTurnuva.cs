using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GameCenterAI.DataAccess;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for tournament management with dynamic bracket visualization.
    /// </summary>
    public partial class FrmTurnuva : DevExpress.XtraEditors.XtraForm
    {
        private XtraScrollableControl _scrollableControl;
        private SimpleButton _btnTurnuvayiBaslat;
        private SimpleButton _btnYeni;
        private SimpleButton _btnDuzenle;
        private SimpleButton _btnSil;
        private SimpleButton _btnYenile;
        private SimpleButton _btnSonrakiTur;
        private ComboBoxEdit _cmbTurnuvalar;
        private LabelControl _lblTurnuvaSec;
        private DevExpress.XtraGrid.GridControl _gridControlTurnuvalar;
        private DevExpress.XtraGrid.Views.Grid.GridView _gridViewTurnuvalar;
        private STurnuva _turnuvaService;
        private SUyeler _uyeService;
        private List<KeyValuePair<int, int>> _eslestirmeler;
        private int _seciliTurnuvaID;

        /// <summary>
        /// Initializes a new instance of the FrmTurnuva class.
        /// </summary>
        public FrmTurnuva()
        {
            InitializeComponent();
            _turnuvaService = new STurnuva();
            _uyeService = new SUyeler();
            _eslestirmeler = new List<KeyValuePair<int, int>>();
            _seciliTurnuvaID = -1;
            LoadTurnuvalar();
            LoadTurnuvalarGrid();
        }

        /// <summary>
        /// Loads all tournaments into the combo box.
        /// </summary>
        private void LoadTurnuvalar()
        {
            try
            {
                var turnuvalar = _turnuvaService.Listele();
                _cmbTurnuvalar.Properties.Items.Clear();
                _cmbTurnuvalar.Properties.Items.Add("-- Turnuva Seçiniz --");

                foreach (var turnuva in turnuvalar)
                {
                    _cmbTurnuvalar.Properties.Items.Add($"{turnuva.TurnuvaAdi} (ID: {turnuva.TurnuvaID})");
                }

                _cmbTurnuvalar.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Turnuvalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the start tournament button click event.
        /// </summary>
        private void BtnTurnuvayiBaslat_Click(object sender, EventArgs e)
        {
            if (_cmbTurnuvalar.SelectedIndex <= 0)
            {
                XtraMessageBox.Show("Lütfen bir turnuva seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Get selected tournament ID
                var turnuvalar = _turnuvaService.Listele();
                if (_cmbTurnuvalar.SelectedIndex > 0 && _cmbTurnuvalar.SelectedIndex <= turnuvalar.Count)
                {
                    _seciliTurnuvaID = turnuvalar[_cmbTurnuvalar.SelectedIndex - 1].TurnuvaID;
                }
                else
                {
                    XtraMessageBox.Show("Geçersiz turnuva seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Check if matches already exist
                var mevcutMaclar = _turnuvaService.MaclariGetir(_seciliTurnuvaID);
                if (mevcutMaclar.Count > 0)
                {
                    if (XtraMessageBox.Show("Bu turnuva için zaten maçlar oluşturulmuş. Yeni maçlar oluşturmak istiyor musunuz? (Mevcut maçlar silinecek)", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        BracketGoster();
                        return;
                    }
                    
                    // Mevcut maçları sil
                    try
                    {
                        Tools.OpenConnection();
                        SqlCommand deleteCommand = new SqlCommand();
                        deleteCommand.Connection = Tools.Connection;
                        deleteCommand.CommandType = CommandType.Text;
                        deleteCommand.CommandText = "DELETE FROM TurnuvaMaclari WHERE TurnuvaID = @TurnuvaID";
                        deleteCommand.Parameters.AddWithValue("@TurnuvaID", _seciliTurnuvaID);
                        deleteCommand.ExecuteNonQuery();
                        Tools.CloseConnection();
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Eski maçlar silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Create pairings
                _eslestirmeler = _turnuvaService.EslestirmeleriOlustur(_seciliTurnuvaID);

                if (_eslestirmeler.Count == 0)
                {
                    XtraMessageBox.Show("Yeterli üye bulunamadı. En az 2 üye gereklidir.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save matches to database
                bool maclarOlusturuldu = _turnuvaService.MaclariOlustur(_seciliTurnuvaID, _eslestirmeler);

                if (maclarOlusturuldu)
                {
                    // Visualize bracket
                    BracketGoster();

                    XtraMessageBox.Show($"Turnuva başlatıldı! {_eslestirmeler.Count} eşleşme oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show("Maçlar oluşturulamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Turnuva başlatma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Displays the tournament bracket dynamically from database.
        /// </summary>
        private void BracketGoster()
        {
            // Clear existing controls
            _scrollableControl.Controls.Clear();

            if (_seciliTurnuvaID <= 0)
            {
                return;
            }

            try
            {
                // Get all matches from database
                var maclar = _turnuvaService.MaclariGetir(_seciliTurnuvaID);

                if (maclar == null || maclar.Count == 0)
                {
                    LabelControl lblBilgi = new LabelControl();
                    lblBilgi.Text = "Henüz maç oluşturulmamış. 'Turnuvayı Başlat' butonuna tıklayın.";
                    lblBilgi.Location = new Point(20, 20);
                    lblBilgi.Size = new Size(400, 20);
                    lblBilgi.Appearance.Font = new Font("Tahoma", 10F, FontStyle.Italic);
                    _scrollableControl.Controls.Add(lblBilgi);
                    return;
                }

                // Group matches by round
                var ceyrekFinal = maclar.Where(m => m.Tur == "Çeyrek Final").OrderBy(m => m.MacNo).ToList();
                var yariFinal = maclar.Where(m => m.Tur == "Yarı Final").OrderBy(m => m.MacNo).ToList();
                var final = maclar.Where(m => m.Tur == "Final").OrderBy(m => m.MacNo).ToList();

                int yPosition = 20;
                int matchWidth = 280;
                int matchHeight = 120;
                int spacing = 20;
                int turSpacing = 50;

                // Draw Quarter Finals
                if (ceyrekFinal.Count > 0)
                {
                    LabelControl lblTur = new LabelControl();
                    lblTur.Text = "🏆 ÇEYREK FİNAL";
                    lblTur.Location = new Point(20, yPosition);
                    lblTur.Size = new Size(300, 30);
                    lblTur.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    lblTur.Appearance.ForeColor = Color.FromArgb(0, 102, 204);
                    lblTur.Appearance.Options.UseFont = true;
                    lblTur.Appearance.Options.UseForeColor = true;
                    _scrollableControl.Controls.Add(lblTur);
                    yPosition += 30;

                    for (int i = 0; i < ceyrekFinal.Count; i++)
                    {
                        var mac = ceyrekFinal[i];
                        yPosition = MacKutusuOlustur(mac, yPosition, matchWidth, matchHeight, spacing);
                    }
                    yPosition += turSpacing;
                }

                // Draw Semi Finals
                if (yariFinal.Count > 0)
                {
                    LabelControl lblTur = new LabelControl();
                    lblTur.Text = "🥈 YARI FİNAL";
                    lblTur.Location = new Point(20, yPosition);
                    lblTur.Size = new Size(300, 30);
                    lblTur.Appearance.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                    lblTur.Appearance.ForeColor = Color.FromArgb(255, 193, 7);
                    lblTur.Appearance.Options.UseFont = true;
                    lblTur.Appearance.Options.UseForeColor = true;
                    _scrollableControl.Controls.Add(lblTur);
                    yPosition += 30;

                    for (int i = 0; i < yariFinal.Count; i++)
                    {
                        var mac = yariFinal[i];
                        yPosition = MacKutusuOlustur(mac, yPosition, matchWidth, matchHeight, spacing);
                    }
                    yPosition += turSpacing;
                }

                // Draw Final
                if (final.Count > 0)
                {
                    LabelControl lblTur = new LabelControl();
                    lblTur.Text = "🏆 FİNAL 🏆";
                    lblTur.Location = new Point(20, yPosition);
                    lblTur.Size = new Size(300, 35);
                    lblTur.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                    lblTur.Appearance.ForeColor = Color.FromArgb(220, 53, 69);
                    lblTur.Appearance.Options.UseFont = true;
                    lblTur.Appearance.Options.UseForeColor = true;
                    _scrollableControl.Controls.Add(lblTur);
                    yPosition += 30;

                    for (int i = 0; i < final.Count; i++)
                    {
                        var mac = final[i];
                        yPosition = MacKutusuOlustur(mac, yPosition, matchWidth, matchHeight, spacing);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bracket yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Creates a match box control with players, scores, and result button.
        /// </summary>
        private int MacKutusuOlustur(TurnuvaMaclari mac, int yPosition, int matchWidth, int matchHeight, int spacing)
        {
            Uyeler uye1 = _uyeService.Getir(mac.Uye1ID);
            Uyeler uye2 = _uyeService.Getir(mac.Uye2ID);

            string uye1Adi = uye1 != null ? uye1.AdSoyad : $"Üye ID: {mac.Uye1ID}";
            string uye2Adi = uye2 != null ? uye2.AdSoyad : $"Üye ID: {mac.Uye2ID}";

            // Create GroupControl for match - Modern design
            GroupControl matchGroup = new GroupControl();
            matchGroup.Text = $"🎮 {mac.Tur} - Maç {mac.MacNo}";
            matchGroup.Location = new Point(20, yPosition);
            matchGroup.Size = new Size(matchWidth, matchHeight);
            matchGroup.AppearanceCaption.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            matchGroup.AppearanceCaption.ForeColor = Color.FromArgb(0, 102, 204);
            matchGroup.AppearanceCaption.Options.UseFont = true;
            matchGroup.AppearanceCaption.Options.UseForeColor = true;
            if (mac.Durum == "Sonuçlandı")
            {
                matchGroup.Appearance.BackColor = Color.FromArgb(240, 255, 240);
            }
            else
            {
                matchGroup.Appearance.BackColor = Color.FromArgb(255, 255, 240);
            }
            matchGroup.Appearance.Options.UseBackColor = true;

            // Player 1
            LabelControl lblUye1 = new LabelControl();
            lblUye1.Text = uye1Adi;
            lblUye1.Location = new Point(10, 30);
            lblUye1.Size = new Size(150, 20);
            lblUye1.Appearance.Font = new Font("Tahoma", 9F, FontStyle.Bold);

            // Player 1 Score
            LabelControl lblSkor1 = new LabelControl();
            lblSkor1.Text = mac.Skor1.HasValue ? mac.Skor1.Value.ToString() : "-";
            lblSkor1.Location = new Point(170, 30);
            lblSkor1.Size = new Size(40, 20);
            lblSkor1.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSkor1.Appearance.ForeColor = mac.Skor1.HasValue ? Color.FromArgb(0, 102, 204) : Color.Gray;
            lblSkor1.Appearance.Options.UseForeColor = true;

            // VS or Result
            LabelControl lblVS = new LabelControl();
            if (mac.Durum == "Sonuçlandı" && mac.KazananID.HasValue)
            {
                lblVS.Text = mac.KazananID == mac.Uye1ID ? "✓ KAZANDI" : "✓ KAZANDI";
                lblVS.Appearance.ForeColor = Color.FromArgb(40, 167, 69);
                lblVS.Appearance.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }
            else
            {
                lblVS.Text = "⚔ VS ⚔";
                lblVS.Appearance.ForeColor = Color.FromArgb(220, 53, 69);
                lblVS.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
            lblVS.Location = new Point(10, 55);
            lblVS.Size = new Size(200, 20);
            lblVS.Appearance.Options.UseForeColor = true;
            lblVS.Appearance.Options.UseFont = true;
            lblVS.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblVS.Appearance.Options.UseTextOptions = true;

            // Player 2
            LabelControl lblUye2 = new LabelControl();
            lblUye2.Text = uye2Adi;
            lblUye2.Location = new Point(10, 75);
            lblUye2.Size = new Size(150, 20);
            lblUye2.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUye2.Appearance.ForeColor = Color.FromArgb(0, 102, 204);
            lblUye2.Appearance.Options.UseForeColor = true;

            // Player 2 Score
            LabelControl lblSkor2 = new LabelControl();
            lblSkor2.Text = mac.Skor2.HasValue ? mac.Skor2.Value.ToString() : "-";
            lblSkor2.Location = new Point(170, 75);
            lblSkor2.Size = new Size(40, 20);
            lblSkor2.Appearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSkor2.Appearance.ForeColor = mac.Skor2.HasValue ? Color.FromArgb(0, 102, 204) : Color.Gray;
            lblSkor2.Appearance.Options.UseForeColor = true;

            // Result button - Modern design
            SimpleButton btnSonuc = new SimpleButton();
            btnSonuc.Text = mac.Durum == "Sonuçlandı" ? "✏️ Düzenle" : "✅ Sonuçlandır";
            btnSonuc.Location = new Point(210, 30);
            btnSonuc.Size = new Size(70, 70);
            btnSonuc.Tag = mac.MacID;
            btnSonuc.Appearance.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSonuc.Appearance.Options.UseFont = true;
            if (mac.Durum == "Sonuçlandı")
            {
                btnSonuc.Appearance.BackColor = Color.FromArgb(255, 193, 7);
                btnSonuc.Appearance.ForeColor = Color.White;
            }
            else
            {
                btnSonuc.Appearance.BackColor = Color.FromArgb(40, 167, 69);
                btnSonuc.Appearance.ForeColor = Color.White;
            }
            btnSonuc.Appearance.Options.UseBackColor = true;
            btnSonuc.Appearance.Options.UseForeColor = true;
            btnSonuc.AppearanceHovered.BackColor = Color.FromArgb(30, 150, 60);
            btnSonuc.AppearanceHovered.Options.UseBackColor = true;
            btnSonuc.Click += BtnMacSonucu_Click;

            matchGroup.Controls.Add(lblUye1);
            matchGroup.Controls.Add(lblSkor1);
            matchGroup.Controls.Add(lblVS);
            matchGroup.Controls.Add(lblUye2);
            matchGroup.Controls.Add(lblSkor2);
            matchGroup.Controls.Add(btnSonuc);

            _scrollableControl.Controls.Add(matchGroup);

            return yPosition + matchHeight + spacing;
        }

        /// <summary>
        /// Handles match result button click event.
        /// </summary>
        private void BtnMacSonucu_Click(object sender, EventArgs e)
        {
            try
            {
                SimpleButton btn = sender as SimpleButton;
                if (btn != null && btn.Tag != null)
                {
                    int macID = Convert.ToInt32(btn.Tag);
                    TurnuvaMaclari mac = _turnuvaService.MacGetir(macID);

                    if (mac != null)
                    {
                        FrmMacSonucu frmMacSonucu = new FrmMacSonucu(mac);
                        if (frmMacSonucu.ShowDialog() == DialogResult.OK)
                        {
                            BracketGoster();
                            
                            // Final maçı sonuçlandırıldıysa turnuvayı tamamla
                            var guncellenenMac = _turnuvaService.MacGetir(mac.MacID);
                            if (guncellenenMac != null && guncellenenMac.Tur == "Final" && guncellenenMac.Durum == "Sonuçlandı" && guncellenenMac.KazananID.HasValue)
                            {
                                FinalTamamlandiKontrol();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles tournament selection change event.
        /// </summary>
        private void CmbTurnuvalar_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var turnuvalar = _turnuvaService.Listele();
                if (_cmbTurnuvalar.SelectedIndex > 0 && _cmbTurnuvalar.SelectedIndex <= turnuvalar.Count)
                {
                    _seciliTurnuvaID = turnuvalar[_cmbTurnuvalar.SelectedIndex - 1].TurnuvaID;
                    BracketGoster();
                }
                else
                {
                    _scrollableControl.Controls.Clear();
                    _seciliTurnuvaID = -1;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads tournaments into the grid control.
        /// </summary>
        private void LoadTurnuvalarGrid()
        {
            try
            {
                var turnuvalar = _turnuvaService.Listele();
                _gridControlTurnuvalar.DataSource = turnuvalar;
                _gridViewTurnuvalar.PopulateColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Turnuvalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the new tournament button click event.
        /// </summary>
        private void BtnYeni_Click(object sender, EventArgs e)
        {
            FrmTurnuvaKayit frmTurnuvaKayit = new FrmTurnuvaKayit();
            if (frmTurnuvaKayit.ShowDialog() == DialogResult.OK)
            {
                LoadTurnuvalar();
                LoadTurnuvalarGrid();
            }
        }

        /// <summary>
        /// Handles the edit tournament button click event.
        /// </summary>
        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (_gridViewTurnuvalar.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen düzenlemek için bir turnuva seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int turnuvaID = Convert.ToInt32(_gridViewTurnuvalar.GetFocusedRowCellValue("TurnuvaID"));
            Turnuvalar turnuva = _turnuvaService.Getir(turnuvaID);
            
            if (turnuva != null)
            {
                FrmTurnuvaKayit frmTurnuvaKayit = new FrmTurnuvaKayit(turnuva);
                if (frmTurnuvaKayit.ShowDialog() == DialogResult.OK)
                {
                    LoadTurnuvalar();
                    LoadTurnuvalarGrid();
                }
            }
        }

        /// <summary>
        /// Handles the delete tournament button click event.
        /// </summary>
        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (_gridViewTurnuvalar.GetSelectedRows().Length == 0)
            {
                XtraMessageBox.Show("Lütfen silmek için bir turnuva seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int turnuvaID = Convert.ToInt32(_gridViewTurnuvalar.GetFocusedRowCellValue("TurnuvaID"));
            string turnuvaAdi = _gridViewTurnuvalar.GetFocusedRowCellValue("TurnuvaAdi")?.ToString() ?? "Seçili Turnuva";

            if (XtraMessageBox.Show($"{turnuvaAdi} turnuvasını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    bool result = _turnuvaService.Sil(turnuvaID);
                    if (result)
                    {
                        XtraMessageBox.Show("Turnuva silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTurnuvalar();
                        LoadTurnuvalarGrid();
                    }
                    else
                    {
                        XtraMessageBox.Show("Turnuva silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Turnuva silme işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the refresh button click event.
        /// </summary>
        private void BtnYenile_Click(object sender, EventArgs e)
        {
            LoadTurnuvalar();
            LoadTurnuvalarGrid();
            if (_seciliTurnuvaID > 0)
            {
                BracketGoster();
            }
        }

        /// <summary>
        /// Handles the next round button click event.
        /// </summary>
        private void BtnSonrakiTur_Click(object sender, EventArgs e)
        {
            if (_seciliTurnuvaID <= 0)
            {
                XtraMessageBox.Show("Lütfen bir turnuva seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var maclar = _turnuvaService.MaclariGetir(_seciliTurnuvaID);
                
                // Tüm turları grupla
                var turler = maclar.GroupBy(m => m.Tur).OrderBy(g => 
                    g.Key == "Final" ? 3 : 
                    g.Key.Contains("Yarı Final") ? 2 : 
                    g.Key.Contains("Çeyrek Final") || g.Key.Contains("Son 16") || g.Key.Contains("İlk Tur") ? 1 : 0
                ).ToList();

                string mevcutTur = "";
                bool turBulundu = false;

                // Her turu kontrol et (sırayla)
                foreach (var turGrubu in turler)
                {
                    string turAdi = turGrubu.Key;
                    var turMaclari = turGrubu.ToList();
                    var tamamlananMaclar = turMaclari.Where(m => m.Durum == "Sonuçlandı").ToList();

                    // Eğer bu turun tüm maçları tamamlandıysa ve sonraki tur yoksa
                    if (tamamlananMaclar.Count == turMaclari.Count && turMaclari.Count > 0)
                    {
                        // Final değilse ve sonraki tur yoksa
                        if (!turAdi.Contains("Final") || (turAdi.Contains("Yarı Final") && !maclar.Any(m => m.Tur.Contains("Final") && !turAdi.Contains("Final"))))
                        {
                            mevcutTur = turAdi;
                            turBulundu = true;

                            // Çift sayı kontrolü
                            if (tamamlananMaclar.Count % 2 != 0)
                            {
                                XtraMessageBox.Show($"{turAdi}'de {tamamlananMaclar.Count} maç sonuçlandı. Sonraki tura geçmek için çift sayıda maç gerekiyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            break;
                        }
                    }
                }

                if (!turBulundu)
                {
                    // Detaylı hata mesajı
                    string mesaj = "Sonraki tura geçmek için mevcut turun tüm maçlarının sonuçlandırılması gerekiyor.\n\n";
                    foreach (var turGrubu in turler)
                    {
                        var turMaclari = turGrubu.ToList();
                        var tamamlananMaclar = turMaclari.Where(m => m.Durum == "Sonuçlandı").ToList();
                        if (turMaclari.Count > 0)
                        {
                            mesaj += $"{turGrubu.Key}: {tamamlananMaclar.Count}/{turMaclari.Count} maç sonuçlandı\n";
                        }
                    }
                    XtraMessageBox.Show(mesaj, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (XtraMessageBox.Show($"{mevcutTur} tamamlandı. Sonraki tura geçmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bool result = _turnuvaService.SonrakiTuraGec(_seciliTurnuvaID, mevcutTur);
                    if (result)
                    {
                        XtraMessageBox.Show("Sonraki tur oluşturuldu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BracketGoster();
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Checks if the final match is completed and finalizes the tournament.
        /// </summary>
        private void FinalTamamlandiKontrol()
        {
            try
            {
                var finalMaclari = _turnuvaService.MaclariGetirByTur(_seciliTurnuvaID, "Final");
                if (finalMaclari.Count > 0)
                {
                    var finalMac = finalMaclari[0];
                    if (finalMac.Durum == "Sonuçlandı" && finalMac.KazananID.HasValue)
                    {
                        // Turnuvayı tamamla
                        int? kazananID = _turnuvaService.TurnuvayiTamamla(_seciliTurnuvaID);
                        
                        if (kazananID.HasValue)
                        {
                            SUyeler uyeService = new SUyeler();
                            Uyeler kazanan = uyeService.Getir(kazananID.Value);
                            Turnuvalar turnuva = _turnuvaService.Getir(_seciliTurnuvaID);
                            
                            string mesaj = $"🏆 TEBRİKLER! 🏆\n\n";
                            mesaj += $"Turnuva: {turnuva.TurnuvaAdi}\n";
                            mesaj += $"Kazanan: {kazanan?.AdSoyad ?? $"Üye ID: {kazananID.Value}"}\n";
                            if (turnuva.Odul > 0)
                            {
                                mesaj += $"Ödül: {turnuva.Odul:N2} TL hesabınıza eklendi!\n";
                            }
                            mesaj += $"\nTurnuva başarıyla tamamlandı!";
                            
                            XtraMessageBox.Show(mesaj, "🎉 Turnuva Tamamlandı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Bracket'i yeniden göster
                            BracketGoster();
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda sessizce devam et
            }
        }
    }
}

