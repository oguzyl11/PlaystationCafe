using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using GameCenterAI.Entity;
using GameCenterAI.Service;

namespace GameCenterAI.WinForms
{
    /// <summary>
    /// Form for displaying and managing game tables/desks with detailed information panel.
    /// </summary>
    public partial class FrmMasalar : DevExpress.XtraEditors.XtraForm
    {
        private TileControl _tileControlMasalar;
        private TileGroup _tileGroupMasalar;
        private SMasalar _masaService;
        private SHareket _hareketService;
        private STarife _tarifeService;
        private SSiparis _siparisService;
        private SUrun _urunService;
        private SAiService _aiService;
        private SFatura _faturaService;
        private SOyunlar _oyunService;
        private System.Windows.Forms.Timer _timer;
        
        // Sol Panel - Masa Detayları
        private GroupControl _grpMasaDetay;
        private LabelControl _lblMasaAdi;
        private LabelControl _lblOyun;
        private TextEdit _txtMusteri;
        private TextEdit _txtBaslamaSaati;
        private TextEdit _txtGecenSure;
        private TextEdit _txtTarife;
        private TextEdit _txtSureSiniri;
        private TextEdit _txtKalanSure;
        private TextEdit _txtKullanimUcreti;
        private TextEdit _txtSiparisToplami;
        private TextEdit _txtPesinAlinan;
        private LabelControl _lblToplam;
        private SimpleButton _btnMasaAcKapat;
        private SimpleButton _btnSiparisEkle;
        private SimpleButton _btnOdemeAl;
        private SimpleButton _btnTarifeDegistir;
        private SimpleButton _btnMasaEkle;
        private SimpleButton _btnMasaSil;
        private ComboBoxEdit _cmbTarifeler;
        private GridControl _gridControlSiparisDetay;
        private GridView _gridViewSiparisDetay;
        
        private Masalar _seciliMasa;
        private Hareketler _aktifHareket;

        /// <summary>
        /// Initializes a new instance of the FrmMasalar class.
        /// </summary>
        public FrmMasalar()
        {
            InitializeComponent();
            _masaService = new SMasalar();
            _hareketService = new SHareket();
            _tarifeService = new STarife();
            _siparisService = new SSiparis();
            _urunService = new SUrun();
            _aiService = new SAiService();
            _faturaService = new SFatura();
            _oyunService = new SOyunlar();
            
            InitializeTimer();
            
            // Form yüklendikten sonra verileri yükle (Load event'inde)
            this.Load += FrmMasalar_Load;
        }

        /// <summary>
        /// Handles the form load event - loads data after form is fully initialized.
        /// </summary>
        private void FrmMasalar_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMasalar();
                LoadTarifeler();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Masalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Initializes the timer for real-time updates.
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 1000; // 1 second
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        /// <summary>
        /// Timer tick event handler for updating elapsed time and fees.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_aktifHareket != null && _aktifHareket.HareketID > 0)
            {
                try
                {
                    string hataSure = _hareketService.GecenSureGetir(_aktifHareket.HareketID, out int gecenSure);
                    string hataUcret = _hareketService.UcretHesapla(_aktifHareket.HareketID, out decimal ucret);
                    
                    if (hataSure == null)
                    {
                        _txtGecenSure.Text = $"{gecenSure} Dk.";
                    }
                    
                    if (hataUcret == null)
                    {
                        _txtKullanimUcreti.Text = ucret.ToString("N2");
                    }
                    
                    // Kalan süre hesaplama (eğer tarife varsa)
                    if (_aktifHareket.TarifeID.HasValue)
                    {
                        string hataTarife = _tarifeService.Getir(_aktifHareket.TarifeID.Value, out Tarifeler tarife);
                        if (hataTarife == null && tarife != null && tarife.SureSiniri > 0)
                        {
                            int kalanSure = tarife.SureSiniri - gecenSure;
                            _txtKalanSure.Text = kalanSure > 0 ? $"{kalanSure} Dk." : "0 Dk.";
                        }
                    }
                    
                    // Toplam hesaplama
                    if (hataUcret == null)
                    {
                        decimal toplam = ucret + _aktifHareket.SiparisToplami - _aktifHareket.PesinAlinan;
                        _lblToplam.Text = $"Toplam: {toplam:N2} TL";
                    }
                }
                catch
                {
                    // Hata durumunda sessizce devam et
                }
            }
        }

        /// <summary>
        /// Loads all tables from the database and displays them in the TileControl.
        /// </summary>
        private void LoadMasalar()
        {
            try
            {
                _tileGroupMasalar.Items.Clear();

                string hata = _masaService.GetAllMasalar(out List<Masalar> masalar);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Masalar yüklenirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (Masalar masa in masalar)
                {
                    DevExpress.XtraEditors.TileItem tileItem = new DevExpress.XtraEditors.TileItem();
                    
                    // Aktif hareket kontrolü
                    string hataAktif = _hareketService.GetirAktifByMasaID(masa.MasaID, out Hareketler aktifHareket);
                    if (hataAktif == null && aktifHareket != null)
                    {
                        string hataUcretTile = _hareketService.UcretHesapla(aktifHareket.HareketID, out decimal ucret);
                        tileItem.Text = masa.MasaAdi;
                        if (hataUcretTile == null)
                        {
                            tileItem.Text2 = $"{ucret:N2} TL";
                        }
                        masa.Durum = "Dolu";
                    }
                    else
                    {
                        tileItem.Text = masa.MasaAdi;
                        tileItem.Text2 = "Boş";
                        masa.Durum = "Boş";
                    }
                    
                    tileItem.Tag = masa;

                    // Modern design with gradient effects and shadows
                    if (masa.Durum == "Dolu" || masa.Durum == "Full" || masa.Durum == "1")
                    {
                        // Modern Red gradient for occupied - Premium look
                        tileItem.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(231, 76, 60);
                        tileItem.AppearanceItem.Normal.ForeColor = System.Drawing.Color.White;
                        tileItem.AppearanceItem.Normal.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
                        
                        // Pressed effect - darker red
                        tileItem.AppearanceItem.Pressed.BackColor = System.Drawing.Color.FromArgb(192, 57, 43);
                        tileItem.AppearanceItem.Pressed.ForeColor = System.Drawing.Color.White;
                        tileItem.AppearanceItem.Pressed.BorderColor = System.Drawing.Color.FromArgb(169, 50, 38);
                    }
                    else
                    {
                        // Modern Green gradient for available - Premium look
                        tileItem.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(46, 213, 115);
                        tileItem.AppearanceItem.Normal.ForeColor = System.Drawing.Color.White;
                        tileItem.AppearanceItem.Normal.BorderColor = System.Drawing.Color.FromArgb(39, 174, 96);
                        
                        // Pressed effect - darker green
                        tileItem.AppearanceItem.Pressed.BackColor = System.Drawing.Color.FromArgb(39, 174, 96);
                        tileItem.AppearanceItem.Pressed.ForeColor = System.Drawing.Color.White;
                        tileItem.AppearanceItem.Pressed.BorderColor = System.Drawing.Color.FromArgb(33, 150, 83);
                    }
                    
                    // Modern typography - Premium font with better spacing
                    tileItem.AppearanceItem.Normal.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
                    tileItem.AppearanceItem.Normal.Options.UseFont = true;
                    tileItem.AppearanceItem.Normal.Options.UseBorderColor = true;
                    tileItem.AppearanceItem.Normal.Options.UseBackColor = true;
                    tileItem.AppearanceItem.Normal.Options.UseForeColor = true;
                    
                    // Text alignment - center both horizontally and vertically
                    tileItem.AppearanceItem.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    tileItem.AppearanceItem.Normal.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    tileItem.AppearanceItem.Normal.Options.UseTextOptions = true;
                    
                    // Enable all appearance options for smooth transitions
                    tileItem.AppearanceItem.Pressed.Options.UseBackColor = true;
                    tileItem.AppearanceItem.Pressed.Options.UseForeColor = true;
                    tileItem.AppearanceItem.Pressed.Options.UseBorderColor = true;

                    tileItem.ItemClick += TileItem_ItemClick;
                    _tileGroupMasalar.Items.Add(tileItem);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Masalar yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads tariffs into the combo box.
        /// </summary>
        /// <summary>
        /// Loads tariffs into the combo box.
        /// </summary>
        private void LoadTarifeler()
        {
            try
            {
                string hata = _tarifeService.Listele(out List<Tarifeler> tarifeler);
                if (hata != null)
                {
                    XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                _cmbTarifeler.Properties.Items.Clear();
                _cmbTarifeler.Properties.Items.Add("Tarife Seçiniz");
                
                foreach (var tarife in tarifeler)
                {
                    _cmbTarifeler.Properties.Items.Add($"{tarife.TarifeAdi} - {tarife.SaatlikUcret:N2} TL/saat");
                }
                
                _cmbTarifeler.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the tile item click event.
        /// </summary>
        private void TileItem_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            DevExpress.XtraEditors.TileItem tileItem = sender as DevExpress.XtraEditors.TileItem;
            if (tileItem != null && tileItem.Tag is Masalar)
            {
                _seciliMasa = tileItem.Tag as Masalar;
                MasaDetaylariniYukle();
            }
        }

        /// <summary>
        /// Loads details for the selected table.
        /// </summary>
        private void MasaDetaylariniYukle()
        {
            if (_seciliMasa == null) return;

            _lblMasaAdi.Text = _seciliMasa.MasaAdi;
            
            // Aktif hareket kontrolü
            string hataAktif = _hareketService.GetirAktifByMasaID(_seciliMasa.MasaID, out _aktifHareket);
            if (hataAktif != null)
            {
                _aktifHareket = null;
            }
            
            if (_aktifHareket != null)
            {
                // Müşteri bilgisi
                SUyeler uyeService = new SUyeler();
                string hata = uyeService.Getir(_aktifHareket.UyeID, out Uyeler uye);
                if (hata != null)
                {
                    _txtMusteri.Text = $"Üye ID: {_aktifHareket.UyeID}";
                }
                else
                {
                    _txtMusteri.Text = uye != null ? uye.AdSoyad : $"Üye ID: {_aktifHareket.UyeID}";
                }
                _txtBaslamaSaati.Text = _aktifHareket.Baslangic.ToString("HH:mm:ss");
                
                // Oyun bilgisi (AI tahmini veya kayıtlı)
                if (_aktifHareket.OyunID.HasValue)
                {
                    Oyunlar oyun = OyunGetir(_aktifHareket.OyunID.Value);
                    _lblOyun.Text = oyun != null ? $"🎮 {oyun.OyunAdi}" : "🎮 Oyun Bilgisi Yok";
                    _lblOyun.Visible = true;
                }
                else
                {
                    // AI ile oyun tahmini yap
                    int? tahminEdilenOyunID = _aiService.OyunTahminEt(_aktifHareket.UyeID, _seciliMasa.MasaID);
                    if (tahminEdilenOyunID.HasValue)
                    {
                        Oyunlar oyun = OyunGetir(tahminEdilenOyunID.Value);
                        if (oyun != null)
                        {
                            _lblOyun.Text = $"🎮 {oyun.OyunAdi} (Tahmin)";
                            _lblOyun.Visible = true;
                            // Tahmin edilen oyunu kaydet
                            string hataOyun = _hareketService.OyunGuncelle(_aktifHareket.HareketID, tahminEdilenOyunID.Value);
                            if (hataOyun != null)
                            {
                                // Hata olsa bile devam et
                            }
                        }
                    }
                    else
                    {
                        _lblOyun.Text = "🎮 Oyun Bilgisi Yok";
                        _lblOyun.Visible = true;
                    }
                }
                
                string hataSureDetay = _hareketService.GecenSureGetir(_aktifHareket.HareketID, out int gecenSure);
                if (hataSureDetay == null)
                {
                    _txtGecenSure.Text = $"{gecenSure} Dk.";
                }
                
                // Tarife bilgisi
                if (_aktifHareket.TarifeID.HasValue)
                {
                    string hataTarifeDetay = _tarifeService.Getir(_aktifHareket.TarifeID.Value, out Tarifeler tarife);
                    if (hataTarifeDetay == null && tarife != null)
                    {
                        _txtTarife.Text = tarife.TarifeAdi;
                        _txtSureSiniri.Text = $"{tarife.SureSiniri} Dk.";
                        if (hataSureDetay == null)
                        {
                            int kalanSure = tarife.SureSiniri - gecenSure;
                            _txtKalanSure.Text = kalanSure > 0 ? $"{kalanSure} Dk." : "0 Dk.";
                        }
                    }
                }
                else
                {
                    _txtTarife.Text = "Standart";
                    _txtSureSiniri.Text = "0 Dk.";
                    _txtKalanSure.Text = "-";
                }
                
                string hataUcretDetay = _hareketService.UcretHesapla(_aktifHareket.HareketID, out decimal ucret);
                if (hataUcretDetay == null)
                {
                    _txtKullanimUcreti.Text = ucret.ToString("N2");
                }
                _txtSiparisToplami.Text = _aktifHareket.SiparisToplami.ToString("N2");
                _txtPesinAlinan.Text = _aktifHareket.PesinAlinan.ToString("N2");
                
                if (hataUcretDetay == null)
                {
                    decimal toplam = ucret + _aktifHareket.SiparisToplami - _aktifHareket.PesinAlinan;
                    _lblToplam.Text = $"Toplam: {toplam:N2} TL";
                }
                
                _btnMasaAcKapat.Enabled = true;
                _btnMasaAcKapat.Text = "■ Masa Kapat";
                _btnMasaAcKapat.Appearance.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
                _btnOdemeAl.Enabled = true;
                
                LoadSiparisDetaylar();
            }
            else
            {
                // Masa boş
                _txtMusteri.Text = string.Empty;
                _txtBaslamaSaati.Text = string.Empty;
                _txtGecenSure.Text = "0 Dk.";
                _txtTarife.Text = string.Empty;
                _txtSureSiniri.Text = "0 Dk.";
                _txtKalanSure.Text = "0 Dk.";
                _txtKullanimUcreti.Text = "0,00";
                _txtSiparisToplami.Text = "0,00";
                _txtPesinAlinan.Text = "0,00";
                _lblToplam.Text = "Toplam: 0,00 TL";
                
                _lblOyun.Text = "🎮 Oyun Bilgisi Yok";
                _lblOyun.Visible = false;
                
                _btnMasaAcKapat.Enabled = true;
                _btnMasaAcKapat.Text = "▶ Masa Aç";
                _btnMasaAcKapat.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
                _btnOdemeAl.Enabled = false;
                
                _gridControlSiparisDetay.DataSource = new List<SiparisDetaylar>();
            }
        }

        /// <summary>
        /// Gets a game by ID from the database.
        /// </summary>
        /// <param name="oyunId">The game ID.</param>
        /// <returns>The game entity, or null if not found.</returns>
        private Oyunlar OyunGetir(int oyunId)
        {
            string hata = _oyunService.Getir(oyunId, out Oyunlar oyun);
            if (hata != null)
            {
                return null;
            }
            return oyun;
        }

        /// <summary>
        /// Handles the masa aç/kapat button click event - dynamic based on table status.
        /// </summary>
        private void BtnMasaAcKapat_Click(object sender, EventArgs e)
        {
            if (_seciliMasa == null)
            {
                XtraMessageBox.Show("Lütfen bir masa seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Masa açık mı kontrol et
            if (_aktifHareket != null)
            {
                // Masa kapat
                if (XtraMessageBox.Show("Masa kapatılacak. Devam etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string hataBitir = _hareketService.Bitir(_aktifHareket.HareketID);
                        
                        if (hataBitir == null)
                        {
                            // Masa durumunu "Boş" olarak güncelle
                            string hataDurum = _masaService.DurumGuncelle(_seciliMasa.MasaID, "Boş");
                            if (hataDurum != null)
                            {
                                XtraMessageBox.Show($"Masa durumu güncellenirken hata oluştu: {hataDurum}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                            XtraMessageBox.Show("Masa kapatıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _aktifHareket = null;
                            LoadMasalar();
                            MasaDetaylariniYukle();
                        }
                        else
                        {
                            XtraMessageBox.Show($"Masa kapatma işlemi sırasında hata oluştu: {hataBitir}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Masa kapatma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Masa aç
                // Tarife seçimi zorunlu
                if (_cmbTarifeler.SelectedIndex <= 0)
                {
                    XtraMessageBox.Show("Lütfen bir tarife seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _cmbTarifeler.Focus();
                    return;
                }

                // Üye seçimi
                FrmUyeSec frmUyeSec = new FrmUyeSec();
                if (frmUyeSec.ShowDialog() != DialogResult.OK || frmUyeSec.SecilenUye == null)
                {
                    return;
                }

                try
                {
                    string hataTarifeListe = _tarifeService.Listele(out List<Tarifeler> tarifeler);
                    if (hataTarifeListe != null)
                    {
                        XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {hataTarifeListe}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    int tarifeIndex = _cmbTarifeler.SelectedIndex - 1;
                    
                    if (tarifeIndex < 0 || tarifeIndex >= tarifeler.Count)
                    {
                        XtraMessageBox.Show("Geçersiz tarife seçimi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Hareketler yeniHareket = new Hareketler
                    {
                        UyeID = frmUyeSec.SecilenUye.UyeID,
                        MasaID = _seciliMasa.MasaID,
                        Baslangic = DateTime.Now,
                        Ucret = 0,
                        PesinAlinan = 0,
                        SiparisToplami = 0,
                        Durum = "Aktif",
                        TarifeID = tarifeler[tarifeIndex].TarifeID
                    };

                    // AI ile oyun tahmini yap
                    int? tahminEdilenOyunID = _aiService.OyunTahminEt(frmUyeSec.SecilenUye.UyeID, _seciliMasa.MasaID);
                    if (tahminEdilenOyunID.HasValue)
                    {
                        yeniHareket.OyunID = tahminEdilenOyunID.Value;
                    }

                    // Dinamik fiyatlandırma hesapla
                    DateTime simdi = DateTime.Now;
                    decimal dinamikCarpan = _aiService.DinamikFiyatHesapla(_seciliMasa.MasaID, simdi.Hour, (int)simdi.DayOfWeek);
                    if (dinamikCarpan > 1.0m)
                    {
                        // Saatlik ücreti dinamik fiyatla güncelle
                        decimal yeniSaatlikUcret = _seciliMasa.SaatlikUcret * dinamikCarpan;
                        string hataGuncelle = _masaService.Guncelle(new Masalar
                        {
                            MasaID = _seciliMasa.MasaID,
                            MasaAdi = _seciliMasa.MasaAdi,
                            SaatlikUcret = yeniSaatlikUcret,
                            Durum = _seciliMasa.Durum
                        });
                        if (hataGuncelle == null)
                        {
                            XtraMessageBox.Show($"Yoğun saat nedeniyle saatlik ücret %{(dinamikCarpan - 1) * 100:F0} artırıldı: {yeniSaatlikUcret:N2} TL", "Dinamik Fiyatlandırma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    string hataBaslat = _hareketService.Baslat(yeniHareket, out int hareketID);
                    
                    if (hataBaslat == null && hareketID > 0)
                    {
                        // Masa durumunu "Dolu" olarak güncelle
                        string hataDurumDolu = _masaService.DurumGuncelle(_seciliMasa.MasaID, "Dolu");
                        if (hataDurumDolu != null)
                        {
                            // Hata olsa bile devam et
                        }
                        
                        // Upsell önerileri göster
                        var upsellOneriler = _aiService.UpsellOneriGetir(frmUyeSec.SecilenUye.UyeID, tahminEdilenOyunID);
                        if (upsellOneriler.Count > 0)
                        {
                            string mesaj = "💡 Önerilerimiz:\n\n";
                            foreach (var oneri in upsellOneriler.Take(3))
                            {
                                mesaj += $"• {oneri.Value}\n";
                            }
                            mesaj += "\nSipariş eklemek için 'Sipariş Ekle' butonunu kullanabilirsiniz.";
                            
                            XtraMessageBox.Show(mesaj, "AI Önerileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                        XtraMessageBox.Show("Masa başlatıldı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadMasalar();
                        MasaDetaylariniYukle();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Masa başlatma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the add order button click event.
        /// </summary>
        private void BtnSiparisEkle_Click(object sender, EventArgs e)
        {
            if (_aktifHareket == null)
            {
                XtraMessageBox.Show("Önce masayı başlatmalısınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmSiparisEkle frmSiparisEkle = new FrmSiparisEkle(_aktifHareket.HareketID);
            if (frmSiparisEkle.ShowDialog() == DialogResult.OK)
            {
                MasaDetaylariniYukle();
            }
        }

        /// <summary>
        /// Handles the payment button click event.
        /// </summary>
        private void BtnOdemeAl_Click(object sender, EventArgs e)
        {
            if (_aktifHareket == null)
            {
                XtraMessageBox.Show("Aktif hareket bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string hataUcretOdeme = _hareketService.UcretHesapla(_aktifHareket.HareketID, out decimal ucretOdeme);
                if (hataUcretOdeme != null)
                {
                    XtraMessageBox.Show($"Ücret hesaplanırken hata oluştu: {hataUcretOdeme}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                decimal toplamUcret = ucretOdeme + _aktifHareket.SiparisToplami;
                decimal kalan = toplamUcret - _aktifHareket.PesinAlinan;

                if (kalan <= 0)
                {
                    // Ödeme tamamlanmış - Fatura oluştur
                    if (XtraMessageBox.Show("Ödeme tamamlanmış! Fatura oluşturmak ister misiniz?", "Fatura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FaturaOlustur();
                    }
                    return;
                }

                // Ödeme alma dialog'u
                string odemeStr = DevExpress.XtraEditors.XtraInputBox.Show($"Kalan Tutar: {kalan:N2} TL\n\nÖdeme Miktarı:", "Ödeme Al", kalan.ToString("N2"));
                
                if (!string.IsNullOrEmpty(odemeStr))
                {
                    if (decimal.TryParse(odemeStr, out decimal odemeMiktari))
                    {
                        _aktifHareket.PesinAlinan += odemeMiktari;
                        string hataPesin = _hareketService.PesinAlinanGuncelle(_aktifHareket.HareketID, _aktifHareket.PesinAlinan);
                        if (hataPesin != null)
                        {
                            XtraMessageBox.Show($"Peşin güncellenirken hata oluştu: {hataPesin}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        
                        // Güncel toplam hesapla
                        string hataUcretKalan = _hareketService.UcretHesapla(_aktifHareket.HareketID, out decimal ucretKalan);
                        if (hataUcretKalan != null)
                        {
                            XtraMessageBox.Show($"Ücret hesaplanırken hata oluştu: {hataUcretKalan}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        decimal yeniKalan = (ucretKalan + _aktifHareket.SiparisToplami) - _aktifHareket.PesinAlinan;
                        
                        if (yeniKalan <= 0)
                        {
                            // Ödeme tamamlandı - Fatura oluştur
                            if (XtraMessageBox.Show($"Ödeme tamamlandı! Toplam peşin: {_aktifHareket.PesinAlinan:N2} TL\n\nFatura oluşturmak ister misiniz?", "Fatura", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                FaturaOlustur();
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show($"Ödeme alındı! Toplam peşin: {_aktifHareket.PesinAlinan:N2} TL\nKalan: {yeniKalan:N2} TL", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        
                        MasaDetaylariniYukle();
                    }
                    else
                    {
                        XtraMessageBox.Show("Geçerli bir tutar giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Ödeme alma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the change tariff button click event.
        /// </summary>
        private void BtnTarifeDegistir_Click(object sender, EventArgs e)
        {
            if (_aktifHareket == null)
            {
                XtraMessageBox.Show("Aktif hareket bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_cmbTarifeler.SelectedIndex > 0)
            {
                string hataListe = _tarifeService.Listele(out List<Tarifeler> tarifeler);
                if (hataListe != null)
                {
                    XtraMessageBox.Show($"Tarifeler yüklenirken hata oluştu: {hataListe}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (_cmbTarifeler.SelectedIndex <= tarifeler.Count)
                {
                    int yeniTarifeID = tarifeler[_cmbTarifeler.SelectedIndex - 1].TarifeID;
                    string hataTarife = _hareketService.TarifeGuncelle(_aktifHareket.HareketID, yeniTarifeID);
                    if (hataTarife == null)
                    {
                        XtraMessageBox.Show("Tarife güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        XtraMessageBox.Show($"Tarife güncellenirken hata oluştu: {hataTarife}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    MasaDetaylariniYukle();
                }
            }
        }

        /// <summary>
        /// Loads order details for the active transaction.
        /// </summary>
        private void LoadSiparisDetaylar()
        {
            if (_aktifHareket != null)
            {
                try
                {
                    string hataSiparis = _siparisService.GetirByHareketID(_aktifHareket.HareketID, out List<Siparisler> siparisler);
                    if (hataSiparis != null)
                    {
                        _gridControlSiparisDetay.DataSource = new List<SiparisDetaylar>();
                        return;
                    }
                    
                    List<SiparisDetaylar> tumDetaylar = new List<SiparisDetaylar>();
                    
                    foreach (var siparis in siparisler)
                    {
                        string hataDetay = _siparisService.GetDetaylar(siparis.SiparisID, out List<SiparisDetaylar> detaylar);
                        if (hataDetay == null)
                        {
                            tumDetaylar.AddRange(detaylar);
                        }
                    }
                    
                    _gridControlSiparisDetay.DataSource = tumDetaylar;
                }
                catch (Exception)
                {
                    // Hata durumunda boş liste
                    _gridControlSiparisDetay.DataSource = new List<SiparisDetaylar>();
                }
            }
            else
            {
                _gridControlSiparisDetay.DataSource = new List<SiparisDetaylar>();
            }
        }

        /// <summary>
        /// Cleanup when form is closing.
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Handles the add table button click event.
        /// </summary>
        private void BtnMasaEkle_Click(object sender, EventArgs e)
        {
            FrmMasaKayit frmMasaKayit = new FrmMasaKayit();
            if (frmMasaKayit.ShowDialog() == DialogResult.OK)
            {
                LoadMasalar();
            }
        }

        /// <summary>
        /// Handles the delete table button click event.
        /// </summary>
        private void BtnMasaSil_Click(object sender, EventArgs e)
        {
            if (_seciliMasa == null)
            {
                XtraMessageBox.Show("Lütfen silmek için bir masa seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aktif hareket kontrolü
            string hataAktifSil = _hareketService.GetirAktifByMasaID(_seciliMasa.MasaID, out Hareketler aktifHareket);
            if (hataAktifSil == null && aktifHareket != null)
            {
                XtraMessageBox.Show("Bu masa şu anda kullanımda. Önce masayı kapatınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show($"{_seciliMasa.MasaAdi} masasını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string hata = _masaService.Sil(_seciliMasa.MasaID);
                    if (hata != null)
                    {
                        XtraMessageBox.Show($"Masa silme işlemi sırasında hata oluştu: {hata}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        XtraMessageBox.Show("Masa silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _seciliMasa = null;
                        LoadMasalar();
                        MasaDetaylariniYukle();
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Masa silme işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Creates an invoice for the completed transaction.
        /// </summary>
        private void FaturaOlustur()
        {
            try
            {
                if (_aktifHareket == null)
                {
                    XtraMessageBox.Show("Aktif hareket bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if invoice already exists
                string hataMevcut = _faturaService.HareketIDyeGoreGetir(_aktifHareket.HareketID, out Faturalar mevcutFatura);
                if (hataMevcut == null && mevcutFatura != null)
                {
                    XtraMessageBox.Show($"Bu hareket için zaten fatura oluşturulmuş!\nFatura No: {mevcutFatura.FaturaNo}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Calculate totals
                string hataKullanimUcreti = _hareketService.UcretHesapla(_aktifHareket.HareketID, out decimal kullanimUcreti);
                if (hataKullanimUcreti != null)
                {
                    kullanimUcreti = 0;
                }
                decimal toplamTutar = kullanimUcreti + _aktifHareket.SiparisToplami;
                decimal kdvOrani = 20; // %20 KDV
                decimal kdvTutari = toplamTutar * (kdvOrani / 100);
                decimal genelToplam = toplamTutar + kdvTutari;

                // Generate invoice number
                string hataFaturaNo = _faturaService.FaturaNoOlustur(out string faturaNo);
                if (hataFaturaNo != null)
                {
                    XtraMessageBox.Show($"Fatura numarası oluşturulurken hata oluştu: {hataFaturaNo}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create invoice
                Faturalar fatura = new Faturalar
                {
                    HareketID = _aktifHareket.HareketID,
                    FaturaNo = faturaNo,
                    FaturaTarihi = DateTime.Now,
                    ToplamTutar = toplamTutar,
                    KdvOrani = kdvOrani,
                    KdvTutari = kdvTutari,
                    GenelToplam = genelToplam,
                    Durum = "Aktif",
                    Notlar = $"Masa: {_seciliMasa?.MasaAdi ?? "Bilinmiyor"}"
                };

                string hataOlustur = _faturaService.Olustur(fatura, out int faturaID);

                if (hataOlustur == null && faturaID > 0)
                {
                    string mesaj = $"✅ Fatura başarıyla oluşturuldu!\n\n";
                    mesaj += $"Fatura No: {fatura.FaturaNo}\n";
                    mesaj += $"Tarih: {fatura.FaturaTarihi:dd.MM.yyyy HH:mm}\n";
                    mesaj += $"Ara Toplam: {toplamTutar:N2} TL\n";
                    mesaj += $"KDV (%{kdvOrani}): {kdvTutari:N2} TL\n";
                    mesaj += $"Genel Toplam: {genelToplam:N2} TL\n\n";
                    mesaj += $"Fatura detaylarını görmek için Faturalar menüsünü kullanabilirsiniz.";

                    XtraMessageBox.Show(mesaj, "🎉 Fatura Oluşturuldu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Fatura oluşturma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
