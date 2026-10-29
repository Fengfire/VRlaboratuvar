# ELEMENT — VR Kimya Laboratuvarı

Unity 6000.3.23f1 / XR Interaction Toolkit 3.5.1 / URP 17.3.

## Başlatma
Assets/ChemistryLab/ChemistryLab.unity sahnesini açıp Play düğmesine basın. Game penceresini genişletin ve kontrol için içine tıklayın. Önce F1 ile defteri okuyun, sonra kapatın. Mevcut SampleScene korunmuştur.

## Masaüstü
- WASD: yürü; sağ fare basılı: bak.
- Merkez artısını şişeye yöneltip E: tut; tekrar E: bırak.
- Tekerlek: tutma mesafesi. Nesneler yerçekimi ve çarpışma kullanır.
- Merkez kaba yaklaşarak Q: tutulan reaktiften sembolik aktarım. Nesnenin kaba teması da ekler.
- İki farklı reaktif eklenince sonuç ve denklem gösterilir.
- F1: deney defteri. Masadaki deftere bakıp E de kullanılabilir.
- F6: malzemeleri geri getir, kabı ve efektleri temizle. Keşif sayacı oturum boyunca korunur.
- F7: sanal ışığı aç/kapat; hidrojen-klor deneyini başlatır.
- F8: Unity XR Interaction Simulator / masaüstü geçişi.

## XR simülatörü
Tab: FPS ve cihaz modu; [ ve ]: sol/sağ cihaz; WASD: seçili cihazı taşı; sağ fare: döndür; sol fare: seç; G: kavrama; T: tetik. Shift: sol cihazın düğmeleri. Simülatörün kendi alt köşe yardım arayüzü de vardır. Kontrolcüyle kavradığınız reaktifi merkez kabın algılama hacmine taşıyın. Gerçek gözlük gerekmez. F6 ve F1 iki modda da çalışır.

## Deneyler
1. CH3COOH + NaHCO3 → CH3COONa + H2O + CO2: gaz kabarcıkları.
2. HCl(aq) + NaOH → NaCl + H2O: nötrleşme; yeşil sanal pH gösterimi (doğal ürün rengi değil).
3. Fe + CuSO4 → FeSO4 + Cu: soluk yeşil çözelti ve bakır çökmesi.
4. CaCl2 + Na2CO3 → CaCO3 + 2NaCl: beyaz çökelek.
5. İyot/iyodür ve nişasta: koyu mavi kompleks. Denklem şematiktir.
6. H2(g) + Cl2(g) → 2HCl(g): ışık gerektiren sanal gaz hücresi, kısa ışık efekti.
H2O + NaCl için bu modelde gözlenebilir kimyasal tepkime yoktur. Çözünme kimyasal tepkime olarak modellenmez.

## Kapsam
Eğitsel prototip: miktar, derişim, sıcaklık, gerçek pH, akışkan dökme ve stokiyometrik hesap yapılmaz. Her şişe deney başına bir sembolik katkı verir; şişeler tükenmez. Bilinmeyen ikililer 'modelde tanımlı değil' bildirimi verir; gerçek dünyada inert veya güvenli oldukları iddia edilmez. Cam kaplar ve pipet taşınabilir dekoratif ekipmandır; spatül fiziksel olarak taşınabilir. Terazi, üstündeki Rigidbody kütlelerini gösterir; kimyasal doz ölçmez. Gaz hücresi ve çökelti görselleri temsili, hızlar kısaltılmıştır. Gerçek tehlikeli deney uygulama talimatları içermez.

## Genişletme
ReactionDefinition ScriptableObject varlıkları eşleşen reaktif kimliklerini, denklem, açıklama, renk, gaz/çökelek ve ışık koşulunu tutar. Yeni varlığı LabSession.recipes dizisine ekleyin; şişedeki LabItem.reagent aynı kimliği kullanmalıdır. LabBuilder yalnızca sahne üretim aracıdır. Chemistry Lab > Build Prototype mevcut laboratuvar sahnesini yeniden üretir; elle yapılan sahne değişikliklerinin üstüne yazar.

## Doğrulama — 29 Ekim 2026
- Unity Editor derleme ve son çalışma kontrolünde hata yok.
- Altı tepkime normal ve ters reaktif sırasında çalıştırıldı.
- H2/Cl2 ışık koşulu ve tanımsız ikili geri bildirimi sınandı.
- Fiziksel algılama hacmine taşınan CH3COOH sisteme eklendi.
- Reset: tüm nesnelerin başlangıç pozisyonu ve consumed durumu doğrulandı.
- XRSimulatedHMD ve iki XRSimulatedController oluştu; XR rig içinde beş interactor bulundu.
- XRInteractionManager üzerinden tutma/bırakma çağrısı başarılı.
- Play Mode ekran görüntüleri incelendi; uyumsuz hazır materyaller için ayrı URP materyalleri üretildi.
- Uçtan uca insan klavye/fare kullanım testi, gerçek VR donanımı ve standalone build yapılmadı. Ses oluşturuldu; işitsel dinleme doğrulaması yapılmadı.
- Unity hesap servisi bağlantı uyarısı görüldü; laboratuvar script hatası görülmedi.

Kaynaklar:
https://chem.uiuc.edu/rogers/Text8/Tx82/tx82.html
https://openstax.org/books/chemistry-atoms-first/pages/18-11-occurrence-preparation-and-properties-of-halogens
https://chem.libretexts.org/Ancillary_Materials/Demos_Techniques_and_Experiments/Lecture_Demonstrations/Iodine_Clock
XR kontrolleri: kurulu XRI 3.5.1 Documentation~/xr-interaction-simulator.md.

## Referans stili güncellemesi
Poly Haven CC0 ahşap/mermer dokuları eklendi (THIRD_PARTY.md). Ek dolap, tavan, raf ve sahne içi deney defteri oluşturuldu. Defter sayfaları sağ/sol okla değişir. F8 artık XR Hands rig kullanır ve simülatör Hand modunda açılır: K kavra, N işaret et, M çimdik, O açık el. Masaüstü P işaret et. Masaüstü tekerlek nesne mesafesini değiştirir; XR yerleşik sağ fare + tekerlek seçili cihazı ileri geri taşır. XR tekerlek etkileşimi uçtan uca test edilmedi. Katı/çözelti/gaz görselleri temsili; akışkan çözümü değildir. El modu ve el meshleri Play Mode'da doğrulandı. Build Prototype komutu temel sahneyi yeniden üretir; bu güncellenmiş sahneyi korumak için çalıştırmayın.

## Etkileşim ve dökme güncellemesi
- XR eller başlangıçta kameranın 32 cm altında, 60 cm önünde yerleşir. Home yeniden ortalar.
- Kavrama sürerken fare tekerleği, nesneyi tutan elin simülatör konumunu değiştirir. 120 birim scroll için 18 cm hareket; sol el seçiliyken yalnızca sol elin hareketi doğrulandı. İşleyici testi yapıldı; fiziksel fare ile kullanıcı testi ayrıca gerekir.
- Masaüstü E tut/bırak, tekerlek mesafe, Q basılı tutarak görsel kabı eğme. XR K kavra, O aç, N işaret, M çimdik, Q dökme.
- Dökülebilir kaplar temasla madde eklemez. Kap deney hacminin üzerinde olmalı, ağız aşağı eğilmeli veya Q basılı tutulmalı. Doğru alanda 0,65 saniye akış sonrası sembolik katkı eklenir. Boşa dökme de içeriği azaltır; F6 yeniden doldurur.
- Katı taneler parçacıklarla; sıvı akışı çizgiyle gösterilir. Q yardımcı eğmesi görsel modele uygulanır, gerçek akışkan çözümü değildir. Gazlar sıvı gibi dökülmez; Q yakın alanda sembolik kapalı aktarım yapar.
- Tepkimelerde 3 saniyelik renk geçişi ve çökelti büyümesi, gaz kabarcıkları ve ışık efekti vardır. Reset devam eden animasyonu iptal eder.
- Yeni üst menü tıklanabilir; deney defteri her sayfada tek deney gösterir. Alt şerit bağlama göre kontrolleri açıklar.
- LabHandReach ve StartWithHands, kurulu XRI 3.5.1 simülatörünün dahili durumuna erişir. Paket sürümü yükseltildiğinde uyumluluk testi gerekir.

## Son düzeltmeler — Q, tekerlek ve sonuç tabelası
Önceki dökme açıklamasının yerine: DÖKME YALNIZCA Q BASILIYKEN mümkündür. Kap ters çevrilse de Q olmadan akış veya içerik kaybı oluşmaz. Q bırakılınca yeni emisyon durur; havadaki taneler düşüşünü tamamlar.
XR tekerlek artık el durumunu değiştirmek yerine NearFarInteractor'ın IInteractionAttachController.MoveTo API'siyle kavrama bağlantısını taşır. Normalize (1) ve Windows (120) tekerlek değerleri desteklenir. Mesafe 0,35–3,2 m ile sınırlandırılır.
Q/E simülatörün dikey hareket bağlarından çıkarıldı; dikey cihaz hareketi PageDown/PageUp tuşlarındadır.
Her başarılı tepkimede sağda dünya uzayında bir sonuç tabelası 0,85 saniyede aşağıdan yükselir. Denklem ve ürün açıklamasını gösterir. F6 tabelayı gizler ve efektleri temizler.
Işık gerektiren hidrojen-klor deneyinde temsili kontrollü patlama: kıvılcım parçacıkları, halka dalgası ve ışık parlaması. Diğer deneylerde bilimsel olarak uygun gaz/çökelti/renk efektleri korunur.
Doğrulama: ters held kap Q olmadan remaining=1, delivered=0; Q ile delivered=0,66 ve CH3COOH aktarımı. XR manuel kavrama altında bağlantı 0,16 m ilerledi ve izleyen karelerde cismin z konumu -0,65'ten -0,49'a geldi. Tabela ve patlama oluşturuldu, görüntü kontrolü yapıldı; sıfırlama tabelayı gizledi. Son çalışma kontrolünde Console hatası yok.

## Kütle, ışık ve boy kontrolü
Dökülebilir kaplarda fiziksel kütle = containerMass + contentsMass × remaining. Varsayılan boş kap 100 g, başlangıç içeriği 150 g; bunlar eğitsel model değerleridir. Yarım içerikle 175 g terazi göstergesi Play Mode'da doğrulandı. F6 başlangıç içeriğini ve kütleyi geri getirir.
F7 loş/aydınlık geçişi yapar. Loş modda ana ışıklar %12 düzeyine iner. Başarılı tepkimede ortam 3,5 saniye kararır, kap emisyon ve noktasal ışıkla parlar, ardından önceki moda döner.
Üst sıra veya sayısal tuş takımı 8: yüksel, 2: alçal. Boy farkı -0,65 ile +0,8 m arasında sınırlandırılmıştır. XR kamera ve eller aynı Camera Offset altında birlikte taşınır; testte ikisi de 0,2 m yükseldi. M çimdik ifadesi arayüzden kaldırıldı.

## Güvenlik alanı
Sol masadaki önlük, sarı eldiven ve gözlük: masaüstünde bakıp E, XR'da tutarak giyilir. Önlük simgesel olarak gövdeye yerleşir; eldivenler görünür elleri sarıya boyar. G gözlüğü başa kaldırır / tekrar takar; takılıyken hafif cam rengi ve kenar çerçevesi vardır.
HCl(aq) aktif dökme akışı el modelinin sınırına değerse eldivensiz elde kızarıklık ve 'ELİN TAHRİŞ OLDU' mesajı oluşur. Eldivenli durumda bu eğitim modelinde kızarıklık oluşmaz. Eldivenin rengi gerçek kimyasal dayanım garantisi değildir. Bu yalnızca eğitsel görselleştirmedir.
F6 tahriş gösterimini temizler; giyilmiş ekipman korunur. Gerçek yaralanmayı iyileştirme simülasyonu değildir.
Test: HCl akışı/çıplak el kesişimi uyarı verdi; eldivenle aynı kesişim kızarıklık oluşturmadı. G girdisiyle gözlük kaldırma ve sarı el görüntüsü doğrulandı. Console hatası yok.
