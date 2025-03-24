# Omni-Tank Bots - Tubes 1 IF2211 2025
**Kelompok 60 - Strategi Algoritma**

> Repository ini berisi implementasi 4 bot C# Robocode Tank Royale dengan pendekatan strategi Greedy yang berbeda, dikembangkan dalam rangka Tugas Besar 1 IF2211 Strategi Algoritma 2025 di Institut Teknologi Bandung.

---

##  Anggota Kelompok (Authors)
| Nama                         | NIM      |
|------------------------------|----------|
| Muhammad Luqman Hakim        | 13523044 |
| Muhammad Edo Raduputu Aprima | 13523096 |
| Ferdin Arsenarendra Purtadi  | 13523117 |

---

##  Isi Repository
```
Tubes1_Omni-Tank/
├── src/
│   ├── main-bot/
│   │   └── Conquest/
│   └── alternative-bots/
│       ├── Omniman/
│       ├── Invincible/
│       └── Allen/
├── doc/
│   └── laporan.pdf
├── README.md
```

---

## 📌 Deskripsi Bot

###  Conquest (Bot Utama)
Bot defensif yang bergerak spiral untuk menghindari peluru, memindai musuh terus-menerus, dan memperkirakan posisi musuh berdasarkan kecepatan & arah. Menyesuaikan kekuatan tembakan berdasarkan jarak.

###  Omniman
Bot agresif & brutal: akan menabrak musuh pada jarak dekat sambil menembak kuat. Jika musuh jauh, akan menembak hemat untuk efisiensi. Cocok untuk 1 vs 1 atau arena kecil.

###  Invincible
Bot yang mengejar musuh dan melakukan ramming. Menyesuaikan kekuatan tembakan berdasarkan jarak & energi bot sendiri. Memiliki sistem penghindaran tembok otomatis.

###  Allen
Bot sederhana & efisien. Bergerak memutar dan selalu memindai. Menembak dengan kekuatan tetap. Tidak menyesuaikan jarak, tetapi efektif untuk baseline perbandingan.

---

## Strategi Greedy

| Bot       | Strategi Greedy                             | Fungsi Objektif                      |
|-----------|---------------------------------------------|--------------------------------------|
| Conquest  | Prediksi posisi musuh + gerak spiral        | Maksimal skor sambil bertahan lama  |
| Omniman   | Ram & tembak agresif berdasarkan jarak      | Maksimal damage + survival agresif  |
| Invincible| Dekati musuh berdasarkan jarak adaptif      | Damage maksimal + ram + efisiensi   |
| Allen     | Gerakan dan tembakan konstan                | Stabilitas & kesederhanaan strategi |

---

## Requirement
- .NET 6 SDK
- Robocode Tank Royale Game Engine (versi modifikasi oleh asisten)
- OS: Windows / Linux / macOS

---

## Cara Menjalankan

Masuk ke folder bot, lalu:
```bash
java -jar robocode-tankroyale-gui-0.30.0.jar
```


> Gantilah `BotName` dengan nama bot yang ingin dijalankan (Conquest, Omniman, Invincible, Allen)

---

## Referensi
- [RoboCode Documentation – Robowiki](https://robowiki.net/wiki/Robocode_Documentation)
- [Tank Royale Docs](https://robocode-dev.github.io/tank-royale/)

---