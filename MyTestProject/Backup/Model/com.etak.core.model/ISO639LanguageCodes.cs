using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// ISO 639 to specify languages, it is based on the 3 digit specification. 
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public enum ISO639LanguageCodes
    {
        // ReSharper disable InconsistentNaming
        ///<summary>
        ///Afar
        ///</summary>
        [EnumMember] aar = 0,

        ///<summary>
        ///Abkhazian
        ///</summary>
        [EnumMember] abk = 1,

        ///<summary>
        ///Achinese
        ///</summary>
        [EnumMember] ace = 2,

        ///<summary>
        ///Acoli
        ///</summary>
        [EnumMember] ach = 3,

        ///<summary>
        ///Adangme
        ///</summary>
        [EnumMember] ada = 4,

        ///<summary>
        ///Adyghe;
        ///Adygei
        ///</summary>
        [EnumMember] ady = 5,

        ///<summary>
        ///Afro-Asiatic languages
        ///</summary>
        [EnumMember] afa = 6,

        ///<summary>
        ///Afrihili
        ///</summary>
        [EnumMember] afh = 7,

        ///<summary>
        ///Afrikaans
        ///</summary>
        [EnumMember] afr = 8,

        ///<summary>
        ///Ainu
        ///</summary>
        [EnumMember] ain = 9,

        ///<summary>
        ///Akan
        ///</summary>
        [EnumMember] aka = 10,

        ///<summary>
        ///Akkadian
        ///</summary>
        [EnumMember] akk = 11,

        ///<summary>
        ///Albanian
        ///</summary>
        [EnumMember] alb = 12,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] sqi = 13,

        ///<summary>
        ///Aleut
        ///</summary>
        [EnumMember] ale = 14,

        ///<summary>
        ///Algonquian languages
        ///</summary>
        [EnumMember] alg = 15,

        ///<summary>
        ///Southern Altai
        ///</summary>
        [EnumMember] alt = 16,

        ///<summary>
        ///Amharic
        ///</summary>
        [EnumMember] amh = 17,

        ///<summary>
        ///English, Old (ca.450-1100)
        ///</summary>
        [EnumMember] ang = 18,

        ///<summary>
        ///Angika
        ///</summary>
        [EnumMember] anp = 19,

        ///<summary>
        ///Apache languages
        ///</summary>
        [EnumMember] apa = 20,

        ///<summary>
        ///Arabic
        ///</summary>
        [EnumMember] ara = 21,

        ///<summary>
        ///Official Aramaic (700-300 BCE); 
        ///Imperial Aramaic (700-300 BCE)
        ///</summary>
        [EnumMember] arc = 22,

        ///<summary>
        ///Aragonese
        ///</summary>
        [EnumMember] arg = 23,

        ///<summary>
        ///Armenian
        ///</summary>
        [EnumMember] arm = 24,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] hye = 25,

        ///<summary>
        ///Mapudungun;
        ///Mapuche
        ///</summary>
        [EnumMember] arn = 26,

        ///<summary>
        ///Arapaho
        ///</summary>
        [EnumMember] arp = 27,

        ///<summary>
        ///Artificial languages
        ///</summary>
        [EnumMember] art = 28,

        ///<summary>
        ///Arawak
        ///</summary>
        [EnumMember] arw = 29,

        ///<summary>
        ///Assamese
        ///</summary>
        [EnumMember] asm = 30,

        ///<summary>
        ///Asturian;
        ///Bable;
        ///Leonese;
        ///Asturleonese
        ///</summary>
        [EnumMember] ast = 31,

        ///<summary>
        ///Athapascan languages
        ///</summary>
        [EnumMember] ath = 32,

        ///<summary>
        ///Australian languages
        ///</summary>
        [EnumMember] aus = 33,

        ///<summary>
        ///Avaric
        ///</summary>
        [EnumMember] ava = 34,

        ///<summary>
        ///Avestan
        ///</summary>
        [EnumMember] ave = 35,

        ///<summary>
        ///Awadhi
        ///</summary>
        [EnumMember] awa = 36,

        ///<summary>
        ///Aymara
        ///</summary>
        [EnumMember] aym = 37,

        ///<summary>
        ///Azerbaijani
        ///</summary>
        [EnumMember] aze = 38,

        ///<summary>
        ///Banda languages
        ///</summary>
        [EnumMember] bad = 39,

        ///<summary>
        ///Bamileke languages
        ///</summary>
        [EnumMember] bai = 40,

        ///<summary>
        ///Bashkir
        ///</summary>
        [EnumMember] bak = 41,

        ///<summary>
        ///Baluchi
        ///</summary>
        [EnumMember] bal = 42,

        ///<summary>
        ///Bambara
        ///</summary>
        [EnumMember] bam = 43,

        ///<summary>
        ///Balinese
        ///</summary>
        [EnumMember] ban = 44,

        ///<summary>
        ///Basque
        ///</summary>
        [EnumMember] baq = 45,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] eus = 46,

        ///<summary>
        ///Basa
        ///</summary>
        [EnumMember] bas = 47,

        ///<summary>
        ///Baltic languages
        ///</summary>
        [EnumMember] bat = 48,

        ///<summary>
        ///Beja; Bedawiyet
        ///</summary>
        [EnumMember] bej = 49,

        ///<summary>
        ///Belarusian
        ///</summary>
        [EnumMember] bel = 50,

        ///<summary>
        ///Bemba
        ///</summary>
        [EnumMember] bem = 51,

        ///<summary>
        ///Bengali
        ///</summary>
        [EnumMember] ben = 52,

        ///<summary>
        ///Berber languages
        ///</summary>
        [EnumMember] ber = 53,

        ///<summary>
        ///Bhojpuri
        ///</summary>
        [EnumMember] bho = 54,

        ///<summary>
        ///Bihari languages
        ///</summary>
        [EnumMember] bih = 55,

        ///<summary>
        ///Bikol
        ///</summary>
        [EnumMember] bik = 56,

        ///<summary>
        ///Bini; Edo
        ///</summary>
        [EnumMember] bin = 57,

        ///<summary>
        ///Bislama
        ///</summary>
        [EnumMember] bis = 58,

        ///<summary>
        ///Siksika
        ///</summary>
        [EnumMember] bla = 59,

        ///<summary>
        ///Bantu languages
        ///</summary>
        [EnumMember] bnt = 60,

        ///<summary>
        ///Tibetan
        ///</summary>
        [EnumMember] tib = 61,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] bod = 62,

        ///<summary>
        ///Bosnian
        ///</summary>
        [EnumMember] bos = 63,

        ///<summary>
        ///Braj
        ///</summary>
        [EnumMember] bra = 64,

        ///<summary>
        ///Breton
        ///</summary>
        [EnumMember] bre = 65,

        ///<summary>
        ///Batak languages
        ///</summary>
        [EnumMember] btk = 66,

        ///<summary>
        ///Buriat
        ///</summary>
        [EnumMember] bua = 67,

        ///<summary>
        ///Buginese
        ///</summary>
        [EnumMember] bug = 68,

        ///<summary>
        ///Bulgarian
        ///</summary>
        [EnumMember] bul = 69,

        ///<summary>
        ///Burmese
        ///</summary>
        [EnumMember] bur = 70,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] mya = 71,

        ///<summary>
        ///Blin; Bilin
        ///</summary>
        [EnumMember] byn = 72,

        ///<summary>
        ///Caddo
        ///</summary>
        [EnumMember] cad = 73,

        ///<summary>
        ///Central American Indian languages
        ///</summary>
        [EnumMember] cai = 74,

        ///<summary>
        ///Galibi Carib
        ///</summary>
        [EnumMember] car = 75,

        ///<summary>
        ///Catalan; Valencian
        ///</summary>
        [EnumMember] cat = 76,

        ///<summary>
        ///Caucasian languages
        ///</summary>
        [EnumMember] cau = 77,

        ///<summary>
        ///Cebuano
        ///</summary>
        [EnumMember] ceb = 78,

        ///<summary>
        ///Celtic languages
        ///</summary>
        [EnumMember] cel = 79,

        ///<summary>
        ///Czech
        ///</summary>
        [EnumMember] cze = 80,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] ces = 81,

        ///<summary>
        ///Chamorro
        ///</summary>
        [EnumMember] cha = 82,

        ///<summary>
        ///Chibcha
        ///</summary>
        [EnumMember] chb = 83,

        ///<summary>
        ///Chechen
        ///</summary>
        [EnumMember] che = 84,

        ///<summary>
        ///Chagatai
        ///</summary>
        [EnumMember] chg = 85,

        ///<summary>
        ///Chinese
        ///</summary>
        [EnumMember] chi = 86,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] zho = 87,

        ///<summary>
        ///Chuukese
        ///</summary>
        [EnumMember] chk = 88,

        ///<summary>
        ///Mari
        ///</summary>
        [EnumMember] chm = 89,

        ///<summary>
        ///Chinook jargon
        ///</summary>
        [EnumMember] chn = 90,

        ///<summary>
        ///Choctaw
        ///</summary>
        [EnumMember] cho = 91,

        ///<summary>
        ///Chipewyan; Dene Suline
        ///</summary>
        [EnumMember] chp = 92,

        ///<summary>
        ///Cherokee
        ///</summary>
        [EnumMember] chr = 93,

        ///<summary>
        ///Church Slavic;
        ///Old Slavonic;
        ///Church Slavonic;
        ///Old Bulgarian;
        ///Old Church Slavonic
        ///</summary>
        [EnumMember] chu = 94,

        ///<summary>
        ///Chuvash
        ///</summary>
        [EnumMember] chv = 95,

        ///<summary>
        ///Cheyenne
        ///</summary>
        [EnumMember] chy = 96,

        ///<summary>
        ///Chamic languages
        ///</summary>
        [EnumMember] cmc = 97,

        ///<summary>
        ///Coptic
        ///</summary>
        [EnumMember] cop = 98,

        ///<summary>
        ///Cornish
        ///</summary>
        [EnumMember] cor = 99,

        ///<summary>
        ///Corsican
        ///</summary>
        [EnumMember] cos = 100,

        ///<summary>
        ///Creoles and pidgins, English based
        ///</summary>
        [EnumMember] cpe = 101,

        ///<summary>
        ///Creoles and pidgins, French-based
        ///</summary>
        [EnumMember] cpf = 102,

        ///<summary>
        ///Creoles and pidgins, Portuguese-based
        ///</summary>
        [EnumMember] cpp = 103,

        ///<summary>
        ///Cree
        ///</summary>
        [EnumMember] cre = 104,

        ///<summary>
        ///Crimean Tatar;
        ///Crimean Turkish
        ///</summary>
        [EnumMember] crh = 105,

        ///<summary>
        ///Creoles and pidgins
        ///</summary>
        [EnumMember] crp = 106,

        ///<summary>
        ///Kashubian
        ///</summary>
        [EnumMember] csb = 107,

        ///<summary>
        ///Cushitic languages
        ///</summary>
        [EnumMember] cus = 108,

        ///<summary>
        ///Welsh
        ///</summary>
        [EnumMember] wel = 109,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] cym = 110,

        ///<summary>
        ///Dakota
        ///</summary>
        [EnumMember] dak = 113,

        ///<summary>
        ///Danish
        ///</summary>
        [EnumMember] dan = 114,

        ///<summary>
        ///Dargwa
        ///</summary>
        [EnumMember] dar = 115,

        ///<summary>
        ///Land Dayak languages
        ///</summary>
        [EnumMember] day = 116,

        ///<summary>
        ///Delaware
        ///</summary>
        [EnumMember] del = 117,

        ///<summary>
        ///Slave (Athapascan)
        ///</summary>
        [EnumMember] den = 118,

        ///<summary>
        ///German
        ///</summary>
        [EnumMember] ger = 119,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] deu = 120,

        ///<summary>
        ///Dogrib
        ///</summary>
        [EnumMember] dgr = 121,

        ///<summary>
        ///Dinka
        ///</summary>
        [EnumMember] din = 122,

        ///<summary>
        ///Divehi; Dhivehi; Maldivian
        ///</summary>
        [EnumMember] div = 123,

        ///<summary>
        ///Dogri
        ///</summary>
        [EnumMember] doi = 124,

        ///<summary>
        ///Dravidian languages
        ///</summary>
        [EnumMember] dra = 125,

        ///<summary>
        ///Lower Sorbian
        ///</summary>
        [EnumMember] dsb = 126,

        ///<summary>
        ///Duala
        ///</summary>
        [EnumMember] dua = 127,

        ///<summary>
        ///Dutch, Middle (ca.1050-1350)
        ///</summary>
        [EnumMember] dum = 128,

        ///<summary>
        ///Dutch; Flemish
        ///</summary>
        [EnumMember] dut = 129,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] nld = 130,

        ///<summary>
        ///Dyula
        ///</summary>
        [EnumMember] dyu = 131,

        ///<summary>
        ///Dzongkha
        ///</summary>
        [EnumMember] dzo = 132,

        ///<summary>
        ///Efik
        ///</summary>
        [EnumMember] efi = 133,

        ///<summary>
        ///Egyptian (Ancient)
        ///</summary>
        [EnumMember] egy = 134,

        ///<summary>
        ///Ekajuk
        ///</summary>
        [EnumMember] eka = 135,

        ///<summary>
        ///Greek, Modern (1453-)
        ///</summary>
        [EnumMember] gre = 136,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] ell = 137,

        ///<summary>
        ///Elamite
        ///</summary>
        [EnumMember] elx = 138,

        ///<summary>
        ///English
        ///</summary>
        [EnumMember] eng = 139,

        ///<summary>
        ///English, Middle (1100-1500)
        ///</summary>
        [EnumMember] enm = 140,

        ///<summary>
        ///Esperanto
        ///</summary>
        [EnumMember] epo = 141,

        ///<summary>
        ///Estonian
        ///</summary>
        [EnumMember] est = 142,

        ///<summary>
        ///Ewe
        ///</summary>
        [EnumMember] ewe = 145,

        ///<summary>
        ///Ewondo
        ///</summary>
        [EnumMember] ewo = 146,

        ///<summary>
        ///Fang
        ///</summary>
        [EnumMember] fan = 147,

        ///<summary>
        ///Faroese
        ///</summary>
        [EnumMember] fao = 148,

        ///<summary>
        ///Persian
        ///</summary>
        [EnumMember] per = 149,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] fas = 150,

        ///<summary>
        ///Fanti
        ///</summary>
        [EnumMember] fat = 151,

        ///<summary>
        ///Fijian
        ///</summary>
        [EnumMember] fij = 152,

        ///<summary>
        ///Filipino; Pilipino
        ///</summary>
        [EnumMember] fil = 153,

        ///<summary>
        ///Finnish
        ///</summary>
        [EnumMember] fin = 154,

        ///<summary>
        ///Finno-Ugrian languages
        ///</summary>
        [EnumMember] fiu = 155,

        ///<summary>
        ///Fon
        ///</summary>
        [EnumMember] fon = 156,

        ///<summary>
        ///French
        ///</summary>
        [EnumMember] fre = 157,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] fra = 158,

        ///<summary>
        ///French, Middle (ca.1400-1600)
        ///</summary>
        [EnumMember] frm = 161,

        ///<summary>
        ///French, Old (842-ca.1400)
        ///</summary>
        [EnumMember] fro = 162,

        ///<summary>
        ///Northern Frisian
        ///</summary>
        [EnumMember] frr = 163,

        ///<summary>
        ///Eastern Frisian
        ///</summary>
        [EnumMember] frs = 164,

        ///<summary>
        ///Western Frisian
        ///</summary>
        [EnumMember] fry = 165,

        ///<summary>
        ///Fulah
        ///</summary>
        [EnumMember] ful = 166,

        ///<summary>
        ///Friulian
        ///</summary>
        [EnumMember] fur = 167,

        ///<summary>
        ///Ga
        ///</summary>
        [EnumMember] gaa = 168,

        ///<summary>
        ///Gayo
        ///</summary>
        [EnumMember] gay = 169,

        ///<summary>
        ///Gbaya
        ///</summary>
        [EnumMember] gba = 170,

        ///<summary>
        ///Germanic languages
        ///</summary>
        [EnumMember] gem = 171,

        ///<summary>
        ///Georgian
        ///</summary>
        [EnumMember] geo = 172,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] kat = 173,

        ///<summary>
        ///Geez
        ///</summary>
        [EnumMember] gez = 176,

        ///<summary>
        ///Gilbertese
        ///</summary>
        [EnumMember] gil = 177,

        ///<summary>
        ///Gaelic; Scottish Gaelic
        ///</summary>
        [EnumMember] gla = 178,

        ///<summary>
        ///Irish
        ///</summary>
        [EnumMember] gle = 179,

        ///<summary>
        ///Galician
        ///</summary>
        [EnumMember] glg = 180,

        ///<summary>
        ///Manx
        ///</summary>
        [EnumMember] glv = 181,

        ///<summary>
        ///German, Middle High (ca.1050-1500)
        ///</summary>
        [EnumMember] gmh = 182,

        ///<summary>
        ///German, Old High (ca.750-1050)
        ///</summary>
        [EnumMember] goh = 183,

        ///<summary>
        ///Gondi
        ///</summary>
        [EnumMember] gon = 184,

        ///<summary>
        ///Gorontalo
        ///</summary>
        [EnumMember] gor = 185,

        ///<summary>
        ///Gothic
        ///</summary>
        [EnumMember] got = 186,

        ///<summary>
        ///Grebo
        ///</summary>
        [EnumMember] grb = 187,

        ///<summary>
        ///Greek, Ancient (to 1453)
        ///</summary>
        [EnumMember] grc = 188,

        ///<summary>
        ///Guarani
        ///</summary>
        [EnumMember] grn = 191,

        ///<summary>
        ///Swiss German;
        ///Alemannic;
        ///Alsatian
        ///</summary>
        [EnumMember] gsw = 192,

        ///<summary>
        ///Gujarati
        ///</summary>
        [EnumMember] guj = 193,

        ///<summary>
        ///Gwich'in
        ///</summary>
        [EnumMember] gwi = 194,

        ///<summary>
        ///Haida
        ///</summary>
        [EnumMember] hai = 195,

        ///<summary>
        ///Haitian;
        ///Haitian Creole
        ///</summary>
        [EnumMember] hat = 196,

        ///<summary>
        ///Hausa
        ///</summary>
        [EnumMember] hau = 197,

        ///<summary>
        ///Hawaiian
        ///</summary>
        [EnumMember] haw = 198,

        ///<summary>
        ///Hebrew
        ///</summary>
        [EnumMember] heb = 199,

        ///<summary>
        ///Herero
        ///</summary>
        [EnumMember] her = 200,

        ///<summary>
        ///Hiligaynon
        ///</summary>
        [EnumMember] hil = 201,

        ///<summary>
        ///Himachali languages;
        ///Western Pahari languages
        ///</summary>
        [EnumMember] him = 202,

        ///<summary>
        ///Hindi
        ///</summary>
        [EnumMember] hin = 203,

        ///<summary>
        ///Hittite
        ///</summary>
        [EnumMember] hit = 204,

        ///<summary>
        ///Hmong; Mong
        ///</summary>
        [EnumMember] hmn = 205,

        ///<summary>
        ///Hiri Motu
        ///</summary>
        [EnumMember] hmo = 206,

        ///<summary>
        ///Croatian
        ///</summary>
        [EnumMember] hrv = 207,

        ///<summary>
        ///Upper Sorbian
        ///</summary>
        [EnumMember] hsb = 208,

        ///<summary>
        ///Hungarian
        ///</summary>
        [EnumMember] hun = 209,

        ///<summary>
        ///Hupa
        ///</summary>
        [EnumMember] hup = 210,

        ///<summary>
        ///Iban
        ///</summary>
        [EnumMember] iba = 213,

        ///<summary>
        ///Igbo
        ///</summary>
        [EnumMember] ibo = 214,

        ///<summary>
        ///Icelandic
        ///</summary>
        [EnumMember] ice = 215,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] isl = 216,

        ///<summary>
        ///Ido
        ///</summary>
        [EnumMember] ido = 217,

        ///<summary>
        ///Sichuan Yi; Nuosu
        ///</summary>
        [EnumMember] iii = 218,

        ///<summary>
        ///Ijo languages
        ///</summary>
        [EnumMember] ijo = 219,

        ///<summary>
        ///Inuktitut
        ///</summary>
        [EnumMember] iku = 220,

        ///<summary>
        ///Interlingue;
        ///Occidental
        ///</summary>
        [EnumMember] ile = 221,

        ///<summary>
        ///Iloko
        ///</summary>
        [EnumMember] ilo = 222,

        ///<summary>
        ///Interlingua (International Auxiliary Language Association)
        ///</summary>
        [EnumMember] ina = 223,

        ///<summary>
        ///Indic languages
        ///</summary>
        [EnumMember] inc = 224,

        ///<summary>
        ///Indonesian
        ///</summary>
        [EnumMember] ind = 225,

        ///<summary>
        ///Indo-European languages
        ///</summary>
        [EnumMember] ine = 226,

        ///<summary>
        ///Ingush
        ///</summary>
        [EnumMember] inh = 227,

        ///<summary>
        ///Inupiaq
        ///</summary>
        [EnumMember] ipk = 228,

        ///<summary>
        ///Iranian languages
        ///</summary>
        [EnumMember] ira = 229,

        ///<summary>
        ///Iroquoian languages
        ///</summary>
        [EnumMember] iro = 230,

        ///<summary>
        ///Italian
        ///</summary>
        [EnumMember] ita = 233,

        ///<summary>
        ///Javanese
        ///</summary>
        [EnumMember] jav = 234,

        ///<summary>
        ///Lojban
        ///</summary>
        [EnumMember] jbo = 235,

        ///<summary>
        ///Japanese
        ///</summary>
        [EnumMember] jpn = 236,

        ///<summary>
        ///Judeo-Persian
        ///</summary>
        [EnumMember] jpr = 237,

        ///<summary>
        ///Judeo-Arabic
        ///</summary>
        [EnumMember] jrb = 238,

        ///<summary>
        ///Kara-Kalpak
        ///</summary>
        [EnumMember] kaa = 239,

        ///<summary>
        ///Kabyle
        ///</summary>
        [EnumMember] kab = 240,

        ///<summary>
        ///Kachin; Jingpho
        ///</summary>
        [EnumMember] kac = 241,

        ///<summary>
        ///Kalaallisut; Greenlandic
        ///</summary>
        [EnumMember] kal = 242,

        ///<summary>
        ///Kamba
        ///</summary>
        [EnumMember] kam = 243,

        ///<summary>
        ///Kannada
        ///</summary>
        [EnumMember] kan = 244,

        ///<summary>
        ///Karen languages
        ///</summary>
        [EnumMember] kar = 245,

        ///<summary>
        ///Kashmiri
        ///</summary>
        [EnumMember] kas = 246,

        ///<summary>
        ///Kanuri
        ///</summary>
        [EnumMember] kau = 249,

        ///<summary>
        ///Kawi
        ///</summary>
        [EnumMember] kaw = 250,

        ///<summary>
        ///Kazakh
        ///</summary>
        [EnumMember] kaz = 251,

        ///<summary>
        ///Kabardian
        ///</summary>
        [EnumMember] kbd = 252,

        ///<summary>
        ///Khasi
        ///</summary>
        [EnumMember] kha = 253,

        ///<summary>
        ///Khoisan languages
        ///</summary>
        [EnumMember] khi = 254,

        ///<summary>
        ///Central Khmer
        ///</summary>
        [EnumMember] khm = 255,

        ///<summary>
        ///Khotanese; Sakan
        ///</summary>
        [EnumMember] kho = 256,

        ///<summary>
        ///Kikuyu; Gikuyu
        ///</summary>
        [EnumMember] kik = 257,

        ///<summary>
        ///Kinyarwanda
        ///</summary>
        [EnumMember] kin = 258,

        ///<summary>
        ///Kirghiz; Kyrgyz
        ///</summary>
        [EnumMember] kir = 259,

        ///<summary>
        ///Kimbundu
        ///</summary>
        [EnumMember] kmb = 260,

        ///<summary>
        ///Konkani
        ///</summary>
        [EnumMember] kok = 261,

        ///<summary>
        ///Komi
        ///</summary>
        [EnumMember] kom = 262,

        ///<summary>
        ///Kongo
        ///</summary>
        [EnumMember] kon = 263,

        ///<summary>
        ///Korean
        ///</summary>
        [EnumMember] kor = 264,

        ///<summary>
        ///Kosraean
        ///</summary>
        [EnumMember] kos = 265,

        ///<summary>
        ///Kpelle
        ///</summary>
        [EnumMember] kpe = 266,

        ///<summary>
        ///Karachay-Balkar
        ///</summary>
        [EnumMember] krc = 267,

        ///<summary>
        ///Karelian
        ///</summary>
        [EnumMember] krl = 268,

        ///<summary>
        ///Kru languages
        ///</summary>
        [EnumMember] kro = 269,

        ///<summary>
        ///Kurukh
        ///</summary>
        [EnumMember] kru = 270,

        ///<summary>
        ///Kuanyama\\n Kwanyama
        ///</summary>
        [EnumMember] kua = 271,

        ///<summary>
        ///Kumyk
        ///</summary>
        [EnumMember] kum = 272,

        ///<summary>
        ///Kurdish
        ///</summary>
        [EnumMember] kur = 273,

        ///<summary>
        ///Kutenai
        ///</summary>
        [EnumMember] kut = 274,

        ///<summary>
        ///Ladino
        ///</summary>
        [EnumMember] lad = 275,

        ///<summary>
        ///Lahnda
        ///</summary>
        [EnumMember] lah = 276,

        ///<summary>
        ///Lamba
        ///</summary>
        [EnumMember] lam = 277,

        ///<summary>
        ///Lao
        ///</summary>
        [EnumMember] lao = 278,

        ///<summary>
        ///Latin
        ///</summary>
        [EnumMember] lat = 279,

        ///<summary>
        ///Latvian
        ///</summary>
        [EnumMember] lav = 280,

        ///<summary>
        ///Lezghian
        ///</summary>
        [EnumMember] lez = 281,

        ///<summary>
        ///Limburgan
        ///Limburger
        ///Limburgish
        ///</summary>
        [EnumMember] lim = 282,

        ///<summary>
        ///Lingala
        ///</summary>
        [EnumMember] lin = 283,

        ///<summary>
        ///Lithuanian
        ///</summary>
        [EnumMember] lit = 284,

        ///<summary>
        ///Mongo
        ///</summary>
        [EnumMember] lol = 285,

        ///<summary>
        ///Lozi
        ///</summary>
        [EnumMember] loz = 286,

        ///<summary>
        ///Luxembourgish; 
        ///Letzeburgesch
        ///</summary>
        [EnumMember] ltz = 287,

        ///<summary>
        ///Luba-Lulua
        ///</summary>
        [EnumMember] lua = 288,

        ///<summary>
        ///Luba-Katanga
        ///</summary>
        [EnumMember] lub = 289,

        ///<summary>
        ///Ganda
        ///</summary>
        [EnumMember] lug = 290,

        ///<summary>
        ///Luiseno
        ///</summary>
        [EnumMember] lui = 291,

        ///<summary>
        ///Lunda
        ///</summary>
        [EnumMember] lun = 292,

        ///<summary>
        ///Luo (Kenya and Tanzania)
        ///</summary>
        [EnumMember] luo = 293,

        ///<summary>
        ///Lushai
        ///</summary>
        [EnumMember] lus = 294,

        ///<summary>
        ///Macedonian
        ///</summary>
        [EnumMember] mac = 295,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] mkd = 296,

        ///<summary>
        ///Madurese
        ///</summary>
        [EnumMember] mad = 297,

        ///<summary>
        ///Magahi
        ///</summary>
        [EnumMember] mag = 298,

        ///<summary>
        ///Marshallese
        ///</summary>
        [EnumMember] mah = 299,

        ///<summary>
        ///Maithili
        ///</summary>
        [EnumMember] mai = 300,

        ///<summary>
        ///Makasar
        ///</summary>
        [EnumMember] mak = 301,

        ///<summary>
        ///Malayalam
        ///</summary>
        [EnumMember] mal = 302,

        ///<summary>
        ///Mandingo
        ///</summary>
        [EnumMember] man = 303,

        ///<summary>
        ///Maori
        ///</summary>
        [EnumMember] mao = 304,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] mri = 305,

        ///<summary>
        ///Austronesian languages
        ///</summary>
        [EnumMember] map = 306,

        ///<summary>
        ///Marathi
        ///</summary>
        [EnumMember] mar = 307,

        ///<summary>
        ///Masai
        ///</summary>
        [EnumMember] mas = 308,

        ///<summary>
        ///Malay
        ///</summary>
        [EnumMember] may = 309,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] msa = 310,

        ///<summary>
        ///Moksha
        ///</summary>
        [EnumMember] mdf = 311,

        ///<summary>
        ///Mandar
        ///</summary>
        [EnumMember] mdr = 312,

        ///<summary>
        ///Mende
        ///</summary>
        [EnumMember] men = 313,

        ///<summary>
        ///Irish, Middle (900-1200)
        ///</summary>
        [EnumMember] mga = 314,

        ///<summary>
        ///Mi'kmaq; Micmac
        ///</summary>
        [EnumMember] mic = 315,

        ///<summary>
        ///Minangkabau
        ///</summary>
        [EnumMember] min = 316,

        ///<summary>
        ///Uncoded languages
        ///</summary>
        [EnumMember] mis = 317,

        ///<summary>
        ///Mon-Khmer languages
        ///</summary>
        [EnumMember] mkh = 320,

        ///<summary>
        ///Malagasy
        ///</summary>
        [EnumMember] mlg = 321,

        ///<summary>
        ///Maltese
        ///</summary>
        [EnumMember] mlt = 322,

        ///<summary>
        ///Manchu
        ///</summary>
        [EnumMember] mnc = 323,

        ///<summary>
        ///Manipuri
        ///</summary>
        [EnumMember] mni = 324,

        ///<summary>
        ///Manobo languages
        ///</summary>
        [EnumMember] mno = 325,

        ///<summary>
        ///Mohawk
        ///</summary>
        [EnumMember] moh = 326,

        ///<summary>
        ///Mongolian
        ///</summary>
        [EnumMember] mon = 327,

        ///<summary>
        ///Mossi
        ///</summary>
        [EnumMember] mos = 328,

        ///<summary>
        ///Multiple languages
        ///</summary>
        [EnumMember] mul = 333,

        ///<summary>
        ///Munda languages
        ///</summary>
        [EnumMember] mun = 334,

        ///<summary>
        ///Creek
        ///</summary>
        [EnumMember] mus = 335,

        ///<summary>
        ///Mirandese
        ///</summary>
        [EnumMember] mwl = 336,

        ///<summary>
        ///Marwari
        ///</summary>
        [EnumMember] mwr = 337,

        ///<summary>
        ///Mayan languages
        ///</summary>
        [EnumMember] myn = 340,

        ///<summary>
        ///Erzya
        ///</summary>
        [EnumMember] myv = 341,

        ///<summary>
        ///Nahuatl languages
        ///</summary>
        [EnumMember] nah = 342,

        ///<summary>
        ///North American Indian languages
        ///</summary>
        [EnumMember] nai = 343,

        ///<summary>
        ///Neapolitan
        ///</summary>
        [EnumMember] nap = 344,

        ///<summary>
        ///Nauru
        ///</summary>
        [EnumMember] nau = 345,

        ///<summary>
        ///Navajo; Navaho
        ///</summary>
        [EnumMember] nav = 346,

        ///<summary>
        ///Ndebele, South; South Ndebele
        ///</summary>
        [EnumMember] nbl = 347,

        ///<summary>
        ///Ndebele, North; North Ndebele
        ///</summary>
        [EnumMember] nde = 348,

        ///<summary>
        ///Ndonga
        ///</summary>
        [EnumMember] ndo = 349,

        ///<summary>
        ///Low German
        ///Low Saxon
        ///German, Low
        ///Saxon, Low
        ///</summary>
        [EnumMember] nds = 350,

        ///<summary>
        ///Nepali
        ///</summary>
        [EnumMember] nep = 351,

        ///<summary>
        ///Nepal Bhasa
        ///Newari
        ///</summary>
        [EnumMember] new_ = 352,

        ///<summary>
        ///Nias
        ///</summary>
        [EnumMember] nia = 353,

        ///<summary>
        ///Niger-Kordofanian languages
        ///</summary>
        [EnumMember] nic = 354,

        ///<summary>
        ///Niuean
        ///</summary>
        [EnumMember] niu = 355,

        ///<summary>
        ///Norwegian Nynorsk
        ///Nynorsk, Norwegian
        ///</summary>
        [EnumMember] nno = 358,

        ///<summary>
        ///Bokmål, Norwegian
        ///Norwegian Bokmål
        ///</summary>
        [EnumMember] nob = 359,

        ///<summary>
        ///Nogai
        ///</summary>
        [EnumMember] nog = 360,

        ///<summary>
        ///Norse, Old
        ///</summary>
        [EnumMember] non = 361,

        ///<summary>
        ///Norwegian
        ///</summary>
        [EnumMember] nor = 362,

        ///<summary>
        ///N'Ko
        ///</summary>
        [EnumMember] nqo = 363,

        ///<summary>
        ///Pedi
        ///Sepedi
        ///Northern Sotho
        ///</summary>
        [EnumMember] nso = 364,

        ///<summary>
        ///Nubian languages
        ///</summary>
        [EnumMember] nub = 365,

        ///<summary>
        ///Classical Newari
        ///Old Newari
        ///Classical Nepal Bhasa
        ///</summary>
        [EnumMember] nwc = 366,

        ///<summary>
        ///Chichewa; Chewa; Nyanja
        ///</summary>
        [EnumMember] nya = 367,

        ///<summary>
        ///Nyamwezi
        ///</summary>
        [EnumMember] nym = 368,

        ///<summary>
        ///Nyankole
        ///</summary>
        [EnumMember] nyn = 369,

        ///<summary>
        ///Nyoro
        ///</summary>
        [EnumMember] nyo = 370,

        ///<summary>
        ///Nzima
        ///</summary>
        [EnumMember] nzi = 371,

        ///<summary>
        ///Occitan (post 1500)
        ///</summary>
        [EnumMember] oci = 372,

        ///<summary>
        ///Ojibwa
        ///</summary>
        [EnumMember] oji = 373,

        ///<summary>
        ///Oriya
        ///</summary>
        [EnumMember] ori = 374,

        ///<summary>
        ///Oromo
        ///</summary>
        [EnumMember] orm = 375,

        ///<summary>
        ///Osage
        ///</summary>
        [EnumMember] osa = 376,

        ///<summary>
        ///Ossetian; Ossetic
        ///</summary>
        [EnumMember] oss = 377,

        ///<summary>
        ///Turkish, Ottoman (1500-1928)
        ///</summary>
        [EnumMember] ota = 378,

        ///<summary>
        ///Otomian languages
        ///</summary>
        [EnumMember] oto = 379,

        ///<summary>
        ///Papuan languages
        ///</summary>
        [EnumMember] paa = 380,

        ///<summary>
        ///Pangasinan
        ///</summary>
        [EnumMember] pag = 381,

        ///<summary>
        ///Pahlavi
        ///</summary>
        [EnumMember] pal = 382,

        ///<summary>
        ///Pampanga; Kapampangan
        ///</summary>
        [EnumMember] pam = 383,

        ///<summary>
        ///Panjabi
        ///Punjabi
        ///</summary>
        [EnumMember] pan = 384,

        ///<summary>
        ///Papiamento
        ///</summary>
        [EnumMember] pap = 385,

        ///<summary>
        ///Palauan
        ///</summary>
        [EnumMember] pau = 386,

        ///<summary>
        ///Persian, Old (ca.600-400 B.C.)
        ///</summary>
        [EnumMember] peo = 387,

        ///<summary>
        ///Philippine languages
        ///</summary>
        [EnumMember] phi = 390,

        ///<summary>
        ///Phoenician
        ///</summary>
        [EnumMember] phn = 391,

        ///<summary>
        ///Pali
        ///</summary>
        [EnumMember] pli = 392,

        ///<summary>
        ///Polish
        ///</summary>
        [EnumMember] pol = 393,

        ///<summary>
        ///Pohnpeian
        ///</summary>
        [EnumMember] pon = 394,

        ///<summary>
        ///Portuguese
        ///</summary>
        [EnumMember] por = 395,

        ///<summary>
        ///Prakrit languages
        ///</summary>
        [EnumMember] pra = 396,

        ///<summary>
        ///Provençal, Old (to 1500)
        ///Occitan, Old (to 1500)
        ///</summary>
        [EnumMember] pro = 397,

        ///<summary>
        ///Pushto
        ///Pashto
        ///</summary>
        [EnumMember] pus = 398,

        ///<summary>
        ///Reserved for local use
        ///</summary>
        [EnumMember] qaa_qtz = 399,

        ///<summary>
        ///Quechua
        ///</summary>
        [EnumMember] que = 400,

        ///<summary>
        ///Rajasthani
        ///</summary>
        [EnumMember] raj = 401,

        ///<summary>
        ///Rapanui
        ///</summary>
        [EnumMember] rap = 402,

        ///<summary>
        ///Rarotongan; Cook Islands Maori
        ///</summary>
        [EnumMember] rar = 403,

        ///<summary>
        ///Romance languages
        ///</summary>
        [EnumMember] roa = 404,

        ///<summary>
        ///Romansh
        ///</summary>
        [EnumMember] roh = 405,

        ///<summary>
        ///Romany
        ///</summary>
        [EnumMember] rom = 406,

        ///<summary>
        ///Romanian; Moldavian; Moldovan
        ///</summary>
        [EnumMember] rum = 407,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] ron = 408,

        ///<summary>
        ///Rundi
        ///</summary>
        [EnumMember] run = 411,

        ///<summary>
        ///Aromanian;
        ///Arumanian;
        ///Macedo-Romanian
        ///</summary>
        [EnumMember] rup = 412,

        ///<summary>
        ///Russian
        ///</summary>
        [EnumMember] rus = 413,

        ///<summary>
        ///Sandawe
        ///</summary>
        [EnumMember] sad = 414,

        ///<summary>
        ///Sango
        ///</summary>
        [EnumMember] sag = 415,

        ///<summary>
        ///Yakut
        ///</summary>
        [EnumMember] sah = 416,

        ///<summary>
        ///South American Indian languages
        ///</summary>
        [EnumMember] sai = 417,

        ///<summary>
        ///Salishan languages
        ///</summary>
        [EnumMember] sal = 418,

        ///<summary>
        ///Samaritan Aramaic
        ///</summary>
        [EnumMember] sam = 419,

        ///<summary>
        ///Sanskrit
        ///</summary>
        [EnumMember] san = 420,

        ///<summary>
        ///Sasak
        ///</summary>
        [EnumMember] sas = 421,

        ///<summary>
        ///Santali
        ///</summary>
        [EnumMember] sat = 422,

        ///<summary>
        ///Sicilian
        ///</summary>
        [EnumMember] scn = 423,

        ///<summary>
        ///Scots
        ///</summary>
        [EnumMember] sco = 424,

        ///<summary>
        ///Selkup
        ///</summary>
        [EnumMember] sel = 425,

        ///<summary>
        ///Semitic languages
        ///</summary>
        [EnumMember] sem = 426,

        ///<summary>
        ///Irish, Old (to 900)
        ///</summary>
        [EnumMember] sga = 427,

        ///<summary>
        ///Sign Languages
        ///</summary>
        [EnumMember] sgn = 428,

        ///<summary>
        ///Shan
        ///</summary>
        [EnumMember] shn = 429,

        ///<summary>
        ///Sidamo
        ///</summary>
        [EnumMember] sid = 430,

        ///<summary>
        ///Sinhala; Sinhalese
        ///</summary>
        [EnumMember] sin = 431,

        ///<summary>
        ///Siouan languages
        ///</summary>
        [EnumMember] sio = 432,

        ///<summary>
        ///Sino-Tibetan languages
        ///</summary>
        [EnumMember] sit = 433,

        ///<summary>
        ///Slavic languages
        ///</summary>
        [EnumMember] sla = 434,

        ///<summary>
        ///Slovak
        ///</summary>
        [EnumMember] slo = 435,

        ///<summary>
        ///
        ///</summary>
        [EnumMember] slk = 436,

        ///<summary>
        ///Slovenian
        ///</summary>
        [EnumMember] slv = 439,

        ///<summary>
        ///Southern Sami
        ///</summary>
        [EnumMember] sma = 440,

        ///<summary>
        ///Northern Sami
        ///</summary>
        [EnumMember] sme = 441,

        ///<summary>
        ///Sami languages
        ///</summary>
        [EnumMember] smi = 442,

        ///<summary>
        ///Lule Sami
        ///</summary>
        [EnumMember] smj = 443,

        ///<summary>
        ///Inari Sami
        ///</summary>
        [EnumMember] smn = 444,

        ///<summary>
        ///Samoan
        ///</summary>
        [EnumMember] smo = 445,

        ///<summary>
        ///Skolt Sami
        ///</summary>
        [EnumMember] sms = 446,

        ///<summary>
        ///Shona
        ///</summary>
        [EnumMember] sna = 447,

        ///<summary>
        ///Sindhi
        ///</summary>
        [EnumMember] snd = 448,

        ///<summary>
        ///Soninke
        ///</summary>
        [EnumMember] snk = 449,

        ///<summary>
        ///Sogdian
        ///</summary>
        [EnumMember] sog = 450,

        ///<summary>
        ///Somali
        ///</summary>
        [EnumMember] som = 451,

        ///<summary>
        ///Songhai languages
        ///</summary>
        [EnumMember] son = 452,

        ///<summary>
        ///Sotho, Southern
        ///</summary>
        [EnumMember] sot = 453,

        ///<summary>
        ///Spanish; Castilian
        ///</summary>
        [EnumMember] spa = 454,

        ///<summary>
        ///Sardinian
        ///</summary>
        [EnumMember] srd = 457,

        ///<summary>
        ///Sranan Tongo
        ///</summary>
        [EnumMember] srn = 458,

        ///<summary>
        ///Serbian
        ///</summary>
        [EnumMember] srp = 459,

        ///<summary>
        ///Serer
        ///</summary>
        [EnumMember] srr = 460,

        ///<summary>
        ///Nilo-Saharan languages
        ///</summary>
        [EnumMember] ssa = 461,

        ///<summary>
        ///Swati
        ///</summary>
        [EnumMember] ssw = 462,

        ///<summary>
        ///Sukuma
        ///</summary>
        [EnumMember] suk = 463,

        ///<summary>
        ///Sundanese
        ///</summary>
        [EnumMember] sun = 464,

        ///<summary>
        ///Susu
        ///</summary>
        [EnumMember] sus = 465,

        ///<summary>
        ///Sumerian
        ///</summary>
        [EnumMember] sux = 466,

        ///<summary>
        ///Swahili
        ///</summary>
        [EnumMember] swa = 467,

        ///<summary>
        ///Swedish
        ///</summary>
        [EnumMember] swe = 468,

        ///<summary>
        ///Classical Syriac
        ///</summary>
        [EnumMember] syc = 469,

        ///<summary>
        ///Syriac
        ///</summary>
        [EnumMember] syr = 470,

        ///<summary>
        ///Tahitian
        ///</summary>
        [EnumMember] tah = 471,

        ///<summary>
        ///Tai languages
        ///</summary>
        [EnumMember] tai = 472,

        ///<summary>
        ///Tamil
        ///</summary>
        [EnumMember] tam = 473,

        ///<summary>
        ///Tatar
        ///</summary>
        [EnumMember] tat = 474,

        ///<summary>
        ///Telugu
        ///</summary>
        [EnumMember] tel = 475,

        ///<summary>
        ///Timne
        ///</summary>
        [EnumMember] tem = 476,

        ///<summary>
        ///Tereno
        ///</summary>
        [EnumMember] ter = 477,

        ///<summary>
        ///Tetum
        ///</summary>
        [EnumMember] tet = 478,

        ///<summary>
        ///Tajik
        ///</summary>
        [EnumMember] tgk = 479,

        ///<summary>
        ///Tagalog
        ///</summary>
        [EnumMember] tgl = 480,

        ///<summary>
        ///Thai
        ///</summary>
        [EnumMember] tha = 481,

        ///<summary>
        ///Tigre
        ///</summary>
        [EnumMember] tig = 484,

        ///<summary>
        ///Tigrinya
        ///</summary>
        [EnumMember] tir = 485,

        ///<summary>
        ///Tiv
        ///</summary>
        [EnumMember] tiv = 486,

        ///<summary>
        ///Tokelau
        ///</summary>
        [EnumMember] tkl = 487,

        ///<summary>
        ///Klingon; tlhIngan-Hol
        ///</summary>
        [EnumMember] tlh = 488,

        ///<summary>
        ///Tlingit
        ///</summary>
        [EnumMember] tli = 489,

        ///<summary>
        ///Tamashek
        ///</summary>
        [EnumMember] tmh = 490,

        ///<summary>
        ///Tonga (Nyasa)
        ///</summary>
        [EnumMember] tog = 491,

        ///<summary>
        ///Tonga (Tonga Islands)
        ///</summary>
        [EnumMember] ton = 492,

        ///<summary>
        ///Tok Pisin
        ///</summary>
        [EnumMember] tpi = 493,

        ///<summary>
        ///Tsimshian
        ///</summary>
        [EnumMember] tsi = 494,

        ///<summary>
        ///Tswana
        ///</summary>
        [EnumMember] tsn = 495,

        ///<summary>
        ///Tsonga
        ///</summary>
        [EnumMember] tso = 496,

        ///<summary>
        ///Turkmen
        ///</summary>
        [EnumMember] tuk = 497,

        ///<summary>
        ///Tumbuka
        ///</summary>
        [EnumMember] tum = 498,

        ///<summary>
        ///Tupi languages
        ///</summary>
        [EnumMember] tup = 499,

        ///<summary>
        ///Turkish
        ///</summary>
        [EnumMember] tur = 500,

        ///<summary>
        ///Altaic languages
        ///</summary>
        [EnumMember] tut = 501,

        ///<summary>
        ///Tuvalu
        ///</summary>
        [EnumMember] tvl = 502,

        ///<summary>
        ///Twi
        ///</summary>
        [EnumMember] twi = 503,

        ///<summary>
        ///Tuvinian
        ///</summary>
        [EnumMember] tyv = 504,

        ///<summary>
        ///Udmurt
        ///</summary>
        [EnumMember] udm = 505,

        ///<summary>
        ///Ugaritic
        ///</summary>
        [EnumMember] uga = 506,

        ///<summary>
        ///Uighur; Uyghur
        ///</summary>
        [EnumMember] uig = 507,

        ///<summary>
        ///Ukrainian
        ///</summary>
        [EnumMember] ukr = 508,

        ///<summary>
        ///Umbundu
        ///</summary>
        [EnumMember] umb = 509,

        ///<summary>
        ///Undetermined
        ///</summary>
        [EnumMember] und = 510,

        ///<summary>
        ///Urdu
        ///</summary>
        [EnumMember] urd = 511,

        ///<summary>
        ///Uzbek
        ///</summary>
        [EnumMember] uzb = 512,

        ///<summary>
        ///Vai
        ///</summary>
        [EnumMember] vai = 513,

        ///<summary>
        ///Venda
        ///</summary>
        [EnumMember] ven = 514,

        ///<summary>
        ///Vietnamese
        ///</summary>
        [EnumMember] vie = 515,

        ///<summary>
        ///Volapük
        ///</summary>
        [EnumMember] vol = 516,

        ///<summary>
        ///Votic
        ///</summary>
        [EnumMember] vot = 517,

        ///<summary>
        ///Wakashan languages
        ///</summary>
        [EnumMember] wak = 518,

        ///<summary>
        ///Wolaitta; Wolaytta
        ///</summary>
        [EnumMember] wal = 519,

        ///<summary>
        ///Waray
        ///</summary>
        [EnumMember] war = 520,

        ///<summary>
        ///Washo
        ///</summary>
        [EnumMember] was = 521,

        ///<summary>
        ///Sorbian languages
        ///</summary>
        [EnumMember] wen = 524,

        ///<summary>
        ///Walloon
        ///</summary>
        [EnumMember] wln = 525,

        ///<summary>
        ///Wolof
        ///</summary>
        [EnumMember] wol = 526,

        ///<summary>
        ///Kalmyk; Oirat
        ///</summary>
        [EnumMember] xal = 527,

        ///<summary>
        ///Xhosa
        ///</summary>
        [EnumMember] xho = 528,

        ///<summary>
        ///Yao
        ///</summary>
        [EnumMember] yao = 529,

        ///<summary>
        ///Yapese
        ///</summary>
        [EnumMember] yap = 530,

        ///<summary>
        ///Yiddish
        ///</summary>
        [EnumMember] yid = 531,

        ///<summary>
        ///Yoruba
        ///</summary>
        [EnumMember] yor = 532,

        ///<summary>
        ///Yupik languages
        ///</summary>
        [EnumMember] ypk = 533,

        ///<summary>
        ///Zapotec
        ///</summary>
        [EnumMember] zap = 534,

        ///<summary>
        ///Blissymbols; Blissymbolics; Bliss
        ///</summary>
        [EnumMember] zbl = 535,

        ///<summary>
        ///Zenaga
        ///</summary>
        [EnumMember] zen = 536,

        ///<summary>
        ///Standard Moroccan Tamazight
        ///</summary>
        [EnumMember] zgh = 537,

        ///<summary>
        ///Zhuang; Chuang
        ///</summary>
        [EnumMember] zha = 538,

        ///<summary>
        ///Zande languages
        ///</summary>
        [EnumMember] znd = 541,

        ///<summary>
        ///Zulu
        ///</summary>
        [EnumMember] zul = 542,

        ///<summary>
        ///Zuni
        ///</summary>
        [EnumMember] zun = 543,

        ///<summary>
        ///No linguistic content; Not applicable
        ///</summary>
        [EnumMember] zxx = 544,

        ///<summary>
        ///Zaza; Dimili; Dimli; Kirdki; Kirmanjki; Zazaki
        ///</summary>
        [EnumMember] zza = 545,
        // ReSharper restore InconsistentNaming
    }
}