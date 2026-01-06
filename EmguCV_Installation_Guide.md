# EmguCV Kurulum Rehberi

## Sorun
EmguCV'nin yeni versiyonları (.NET Standard 2.0) .NET Framework 4.8 ile tam uyumlu değildir ve runtime paketleri yüklenemez.

## Çözüm Seçenekleri

### Seçenek 1: EmguCV Olmadan Devam Et (Önerilen)
Uygulama şu anda EmguCV olmadan çalışabilir. Yüz tanıma özelliği opsiyonel olarak kodlanmıştır ve EmguCV yüklü olmadığında hata mesajı gösterir.

### Seçenek 2: Eski EmguCV Versiyonu Kullan (Gelişmiş)
Eğer yüz tanıma özelliğini kullanmak istiyorsanız, .NET Framework ile uyumlu eski bir EmguCV versiyonu kullanabilirsiniz:

1. **Mevcut EmguCV paketlerini kaldırın:**
   - Visual Studio'da: Solution Explorer > Her projede "References" > "Emgu.CV" sağ tık > "Remove"
   - Veya Package Manager Console'da: `Uninstall-Package Emgu.CV -Force`

2. **Eski versiyonu yükleyin (3.4.x serisi):**
   ```
   Install-Package EmguCV -Version 3.4.3.3016
   ```
   
   **NOT:** Bu versiyon eski ve artık aktif olarak desteklenmiyor. Sadece .NET Framework projeleri için geçici bir çözümdür.

### Seçenek 3: Manuel DLL Referansı (En Güvenilir)
1. EmguCV'nin eski bir versiyonunu (3.4.x) manuel olarak indirin
2. DLL'leri projeye manuel olarak referans olarak ekleyin
3. Native DLL'leri (x86/x64) çıktı klasörüne kopyalayın

## Mevcut Durum
- ✅ Uygulama EmguCV olmadan çalışabilir
- ✅ Yüz tanıma özelliği opsiyonel olarak kodlanmış
- ✅ EmguCV yoksa kullanıcıya bilgilendirme mesajı gösterilir

## Öneri
Şimdilik EmguCV referanslarını kaldırıp uygulamayı EmguCV olmadan çalıştırın. İleride yüz tanıma özelliğine ihtiyaç duyarsanız, projeyi .NET 6+ veya .NET Core'ya geçirmeyi düşünün.

