using System;
using System.Runtime.Serialization;

namespace com.etak.core.model
{
    /// <summary>
    /// Enumeration to specify the currency using the ISO4217
    /// </summary>
    [DataContract(Namespace = "http://com.etak.frontend")]
    [Serializable]
    public enum ISO4217CurrencyCodes
    {
        // ReSharper disable InconsistentNaming
        ///<summary>
        ///United Arab Emirates dirham
        ///</summary>
        [EnumMember] AED = 784,


        ///<summary>
        ///Afghan afghani
        ///</summary>
        [EnumMember] AFN = 971,

        ///<summary>
        ///Albanian lek
        ///</summary>
        [EnumMember] ALL = 8,

        ///<summary>
        ///Armenian dram
        ///</summary>
        [EnumMember] AMD = 51,

        ///<summary>
        ///Netherlands Antillean guilder
        ///</summary>
        [EnumMember] ANG = 532,

        ///<summary>
        ///Angolan kwanza
        ///</summary>
        [EnumMember] AOA = 973,

        ///<summary>
        ///Argentine peso
        ///</summary>
        [EnumMember] ARS = 32,

        ///<summary>
        ///Australian dollar
        ///</summary>
        [EnumMember] AUD = 36,

        ///<summary>
        ///Aruban florin
        ///</summary>
        [EnumMember] AWG = 533,

        ///<summary>
        ///Azerbaijani manat
        ///</summary>
        [EnumMember] AZN = 944,

        ///<summary>
        ///Bosnia and Herzegovina convertible mark
        ///</summary>
        [EnumMember] BAM = 977,

        ///<summary>
        ///Barbados dollar
        ///</summary>
        [EnumMember] BBD = 52,

        ///<summary>
        ///Bangladeshi taka
        ///</summary>
        [EnumMember] BDT = 50,

        ///<summary>
        ///Bulgarian lev
        ///</summary>
        [EnumMember] BGN = 975,

        ///<summary>
        ///Bahraini dinar
        ///</summary>
        [EnumMember] BHD = 48,

        ///<summary>
        ///Burundian franc
        ///</summary>
        [EnumMember] BIF = 108,

        ///<summary>
        ///Bermudian dollar
        ///</summary>
        [EnumMember] BMD = 60,

        ///<summary>
        ///Brunei dollar
        ///</summary>
        [EnumMember] BND = 96,

        ///<summary>
        ///Boliviano
        ///</summary>
        [EnumMember] BOB = 68,

        ///<summary>
        ///Bolivian Mvdol (funds code)
        ///</summary>
        [EnumMember] BOV = 984,

        ///<summary>
        ///Brazilian real
        ///</summary>
        [EnumMember] BRL = 986,

        ///<summary>
        ///Bahamian dollar
        ///</summary>
        [EnumMember] BSD = 44,

        ///<summary>
        ///Bhutanese ngultrum
        ///</summary>
        [EnumMember] BTN = 64,

        ///<summary>
        ///Botswana pula
        ///</summary>
        [EnumMember] BWP = 72,

        ///<summary>
        ///Belarusian ruble
        ///</summary>
        [EnumMember] BYR = 974,

        ///<summary>
        ///Belize dollar
        ///</summary>
        [EnumMember] BZD = 84,

        ///<summary>
        ///Canadian dollar
        ///</summary>
        [EnumMember] CAD = 124,

        ///<summary>
        ///Congolese franc
        ///</summary>
        [EnumMember] CDF = 976,

        ///<summary>
        ///WIR Euro (complementary currency)
        ///</summary>
        [EnumMember] CHE = 947,

        ///<summary>
        ///Swiss franc
        ///</summary>
        [EnumMember] CHF = 756,

        ///<summary>
        ///WIR Franc (complementary currency)
        ///</summary>
        [EnumMember] CHW = 948,

        ///<summary>
        ///Unidad de Fomento (funds code)
        ///</summary>
        [EnumMember] CLF = 990,

        ///<summary>
        ///Chilean peso
        ///</summary>
        [EnumMember] CLP = 152,

        ///<summary>
        ///Chinese yuan
        ///</summary>
        [EnumMember] CNY = 156,

        ///<summary>
        ///Colombian peso
        ///</summary>
        [EnumMember] COP = 170,

        ///<summary>
        ///Unidad de Valor Real (UVR) (funds code)[7]
        ///</summary>
        [EnumMember] COU = 970,

        ///<summary>
        ///Costa Rican colon
        ///</summary>
        [EnumMember] CRC = 188,

        ///<summary>
        ///Cuban convertible peso
        ///</summary>
        [EnumMember] CUC = 931,

        ///<summary>
        ///Cuban peso
        ///</summary>
        [EnumMember] CUP = 192,

        ///<summary>
        ///Cape Verde escudo
        ///</summary>
        [EnumMember] CVE = 132,

        ///<summary>
        ///Czech koruna
        ///</summary>
        [EnumMember] CZK = 203,

        ///<summary>
        ///Djiboutian franc
        ///</summary>
        [EnumMember] DJF = 262,

        ///<summary>
        ///Danish krone
        ///</summary>
        [EnumMember] DKK = 208,

        ///<summary>
        ///Dominican peso
        ///</summary>
        [EnumMember] DOP = 214,

        ///<summary>
        ///Algerian dinar
        ///</summary>
        [EnumMember] DZD = 12,

        ///<summary>
        ///Egyptian pound
        ///</summary>
        [EnumMember] EGP = 818,

        ///<summary>
        ///Eritrean nakfa
        ///</summary>
        [EnumMember] ERN = 232,

        ///<summary>
        ///Ethiopian birr
        ///</summary>
        [EnumMember] ETB = 230,

        ///<summary>
        ///Euro
        ///</summary>
        [EnumMember] EUR = 978,

        ///<summary>
        ///Fiji dollar
        ///</summary>
        [EnumMember] FJD = 242,

        ///<summary>
        ///Falkland Islands pound
        ///</summary>
        [EnumMember] FKP = 238,

        ///<summary>
        ///Pound sterling
        ///</summary>
        [EnumMember] GBP = 826,

        ///<summary>
        ///Georgian lari
        ///</summary>
        [EnumMember] GEL = 981,

        ///<summary>
        ///Ghanaian cedi
        ///</summary>
        [EnumMember] GHS = 936,

        ///<summary>
        ///Gibraltar pound
        ///</summary>
        [EnumMember] GIP = 292,

        ///<summary>
        ///Gambian dalasi
        ///</summary>
        [EnumMember] GMD = 270,

        ///<summary>
        ///Guinean franc
        ///</summary>
        [EnumMember] GNF = 324,

        ///<summary>
        ///Guatemalan quetzal
        ///</summary>
        [EnumMember] GTQ = 320,

        ///<summary>
        ///Guyanese dollar
        ///</summary>
        [EnumMember] GYD = 328,

        ///<summary>
        ///Hong Kong dollar
        ///</summary>
        [EnumMember] HKD = 344,

        ///<summary>
        ///Honduran lempira
        ///</summary>
        [EnumMember] HNL = 340,

        ///<summary>
        ///Croatian kuna
        ///</summary>
        [EnumMember] HRK = 191,

        ///<summary>
        ///Haitian gourde
        ///</summary>
        [EnumMember] HTG = 332,

        ///<summary>
        ///Hungarian forint
        ///</summary>
        [EnumMember] HUF = 348,

        ///<summary>
        ///Indonesian rupiah
        ///</summary>
        [EnumMember] IDR = 360,

        ///<summary>
        ///Israeli new shekel
        ///</summary>
        [EnumMember] ILS = 376,

        ///<summary>
        ///Indian rupee
        ///</summary>
        [EnumMember] INR = 356,

        ///<summary>
        ///Iraqi dinar
        ///</summary>
        [EnumMember] IQD = 368,

        ///<summary>
        ///Iranian rial
        ///</summary>
        [EnumMember] IRR = 364,

        ///<summary>
        ///Icelandic króna
        ///</summary>
        [EnumMember] ISK = 352,

        ///<summary>
        ///Jamaican dollar
        ///</summary>
        [EnumMember] JMD = 388,

        ///<summary>
        ///Jordanian dinar
        ///</summary>
        [EnumMember] JOD = 400,

        ///<summary>
        ///Japanese yen
        ///</summary>
        [EnumMember] JPY = 392,

        ///<summary>
        ///Kenyan shilling
        ///</summary>
        [EnumMember] KES = 404,

        ///<summary>
        ///Kyrgyzstani som
        ///</summary>
        [EnumMember] KGS = 417,

        ///<summary>
        ///Cambodian riel
        ///</summary>
        [EnumMember] KHR = 116,

        ///<summary>
        ///Comoro franc
        ///</summary>
        [EnumMember] KMF = 174,

        ///<summary>
        ///North Korean won
        ///</summary>
        [EnumMember] KPW = 408,

        ///<summary>
        ///South Korean won
        ///</summary>
        [EnumMember] KRW = 410,

        ///<summary>
        ///Kuwaiti dinar
        ///</summary>
        [EnumMember] KWD = 414,

        ///<summary>
        ///Cayman Islands dollar
        ///</summary>
        [EnumMember] KYD = 136,

        ///<summary>
        ///Kazakhstani tenge
        ///</summary>
        [EnumMember] KZT = 398,

        ///<summary>
        ///Lao kip
        ///</summary>
        [EnumMember] LAK = 418,

        ///<summary>
        ///Lebanese pound
        ///</summary>
        [EnumMember] LBP = 422,

        ///<summary>
        ///Sri Lankan rupee
        ///</summary>
        [EnumMember] LKR = 144,

        ///<summary>
        ///Liberian dollar
        ///</summary>
        [EnumMember] LRD = 430,

        ///<summary>
        ///Lesotho loti
        ///</summary>
        [EnumMember] LSL = 426,

        ///<summary>
        ///Lithuanian litas
        ///</summary>
        [EnumMember] LTL = 440,

        ///<summary>
        ///Libyan dinar
        ///</summary>
        [EnumMember] LYD = 434,

        ///<summary>
        ///Moroccan dirham
        ///</summary>
        [EnumMember] MAD = 504,

        ///<summary>
        ///Moldovan leu
        ///</summary>
        [EnumMember] MDL = 498,

        ///<summary>
        ///Malagasy ariary
        ///</summary>
        [EnumMember] MGA = 969,

        ///<summary>
        ///Macedonian denar
        ///</summary>
        [EnumMember] MKD = 807,

        ///<summary>
        ///Myanma kyat
        ///</summary>
        [EnumMember] MMK = 104,

        ///<summary>
        ///Mongolian tugrik
        ///</summary>
        [EnumMember] MNT = 496,

        ///<summary>
        ///Macanese pataca
        ///</summary>
        [EnumMember] MOP = 446,

        ///<summary>
        ///Mauritanian ouguiya
        ///</summary>
        [EnumMember] MRO = 478,

        ///<summary>
        ///Mauritian rupee
        ///</summary>
        [EnumMember] MUR = 480,

        ///<summary>
        ///Maldivian rufiyaa
        ///</summary>
        [EnumMember] MVR = 462,

        ///<summary>
        ///Malawian kwacha
        ///</summary>
        [EnumMember] MWK = 454,

        ///<summary>
        ///Mexican peso
        ///</summary>
        [EnumMember] MXN = 484,

        ///<summary>
        ///Mexican Unidad de Inversion (UDI) (funds code)
        ///</summary>
        [EnumMember] MXV = 979,

        ///<summary>
        ///Malaysian ringgit
        ///</summary>
        [EnumMember] MYR = 458,

        ///<summary>
        ///Mozambican metical
        ///</summary>
        [EnumMember] MZN = 943,

        ///<summary>
        ///Namibian dollar
        ///</summary>
        [EnumMember] NAD = 516,

        ///<summary>
        ///Nigerian naira
        ///</summary>
        [EnumMember] NGN = 566,

        ///<summary>
        ///Nicaraguan córdoba
        ///</summary>
        [EnumMember] NIO = 558,

        ///<summary>
        ///Norwegian krone
        ///</summary>
        [EnumMember] NOK = 578,

        ///<summary>
        ///Nepalese rupee
        ///</summary>
        [EnumMember] NPR = 524,

        ///<summary>
        ///New Zealand dollar
        ///</summary>
        [EnumMember] NZD = 554,

        ///<summary>
        ///Omani rial
        ///</summary>
        [EnumMember] OMR = 512,

        ///<summary>
        ///Panamanian balboa
        ///</summary>
        [EnumMember] PAB = 590,

        ///<summary>
        ///Peruvian nuevo sol
        ///</summary>
        [EnumMember] PEN = 604,

        ///<summary>
        ///Papua New Guinean kina
        ///</summary>
        [EnumMember] PGK = 598,

        ///<summary>
        ///Philippine peso
        ///</summary>
        [EnumMember] PHP = 608,

        ///<summary>
        ///Pakistani rupee
        ///</summary>
        [EnumMember] PKR = 586,

        ///<summary>
        ///Polish złoty
        ///</summary>
        [EnumMember] PLN = 985,

        ///<summary>
        ///Paraguayan guaraní
        ///</summary>
        [EnumMember] PYG = 600,

        ///<summary>
        ///Qatari riyal
        ///</summary>
        [EnumMember] QAR = 634,

        ///<summary>
        ///Romanian new leu
        ///</summary>
        [EnumMember] RON = 946,

        ///<summary>
        ///Serbian dinar
        ///</summary>
        [EnumMember] RSD = 941,

        ///<summary>
        ///Russian ruble
        ///</summary>
        [EnumMember] RUB = 643,

        ///<summary>
        ///Rwandan franc
        ///</summary>
        [EnumMember] RWF = 646,

        ///<summary>
        ///Saudi riyal
        ///</summary>
        [EnumMember] SAR = 682,

        ///<summary>
        ///Solomon Islands dollar
        ///</summary>
        [EnumMember] SBD = 90,

        ///<summary>
        ///Seychelles rupee
        ///</summary>
        [EnumMember] SCR = 690,

        ///<summary>
        ///Sudanese pound
        ///</summary>
        [EnumMember] SDG = 938,

        ///<summary>
        ///Swedish krona/kronor
        ///</summary>
        [EnumMember] SEK = 752,

        ///<summary>
        ///Singapore dollar
        ///</summary>
        [EnumMember] SGD = 702,

        ///<summary>
        ///Saint Helena pound
        ///</summary>
        [EnumMember] SHP = 654,

        ///<summary>
        ///Sierra Leonean leone
        ///</summary>
        [EnumMember] SLL = 694,

        ///<summary>
        ///Somali shilling
        ///</summary>
        [EnumMember] SOS = 706,

        ///<summary>
        ///Surinamese dollar
        ///</summary>
        [EnumMember] SRD = 968,

        ///<summary>
        ///South Sudanese pound
        ///</summary>
        [EnumMember] SSP = 728,

        ///<summary>
        ///São Tomé and Príncipe dobra
        ///</summary>
        [EnumMember] STD = 678,

        ///<summary>
        ///Syrian pound
        ///</summary>
        [EnumMember] SYP = 760,

        ///<summary>
        ///Swazi lilangeni
        ///</summary>
        [EnumMember] SZL = 748,

        ///<summary>
        ///Thai baht
        ///</summary>
        [EnumMember] THB = 764,

        ///<summary>
        ///Tajikistani somoni
        ///</summary>
        [EnumMember] TJS = 972,

        ///<summary>
        ///Turkmenistani manat
        ///</summary>
        [EnumMember] TMT = 934,

        ///<summary>
        ///Tunisian dinar
        ///</summary>
        [EnumMember] TND = 788,

        ///<summary>
        ///Tongan paʻanga
        ///</summary>
        [EnumMember] TOP = 776,

        ///<summary>
        ///Turkish lira
        ///</summary>
        [EnumMember] TRY = 949,

        ///<summary>
        ///Trinidad and Tobago dollar
        ///</summary>
        [EnumMember] TTD = 780,

        ///<summary>
        ///New Taiwan dollar
        ///</summary>
        [EnumMember] TWD = 901,

        ///<summary>
        ///Tanzanian shilling
        ///</summary>
        [EnumMember] TZS = 834,

        ///<summary>
        ///Ukrainian hryvnia
        ///</summary>
        [EnumMember] UAH = 980,

        ///<summary>
        ///Ugandan shilling
        ///</summary>
        [EnumMember] UGX = 800,

        ///<summary>
        ///United States dollar
        ///</summary>
        [EnumMember] USD = 840,

        ///<summary>
        ///United States dollar (next day) (funds code)
        ///</summary>
        [EnumMember] USN = 997,

        ///<summary>
        ///United States dollar (same day) (funds code)[10]
        ///</summary>
        [EnumMember] USS = 998,

        ///<summary>
        ///Uruguay Peso en Unidades Indexadas (URUIURUI) (funds code)
        ///</summary>
        [EnumMember] UYI = 940,

        ///<summary>
        ///Uruguayan peso
        ///</summary>
        [EnumMember] UYU = 858,

        ///<summary>
        ///Uzbekistan som
        ///</summary>
        [EnumMember] UZS = 860,

        ///<summary>
        ///Venezuelan bolívar
        ///</summary>
        [EnumMember] VEF = 937,

        ///<summary>
        ///Vietnamese dong
        ///</summary>
        [EnumMember] VND = 704,

        ///<summary>
        ///Vanuatu vatu
        ///</summary>
        [EnumMember] VUV = 548,

        ///<summary>
        ///Samoan tala
        ///</summary>
        [EnumMember] WST = 882,

        ///<summary>
        ///CFA franc BEAC
        ///</summary>
        [EnumMember] XAF = 950,

        ///<summary>
        ///Silver (one troy ounce)
        ///</summary>
        [EnumMember] XAG = 961,

        ///<summary>
        ///Gold (one troy ounce)
        ///</summary>
        [EnumMember] XAU = 959,

        ///<summary>
        ///European Composite Unit (EURCO) (bond market unit)
        ///</summary>
        [EnumMember] XBA = 955,

        ///<summary>
        ///European Monetary Unit (E.M.U.-6) (bond market unit)
        ///</summary>
        [EnumMember] XBB = 956,

        ///<summary>
        ///European Unit of Account 9 (E.U.A.-9) (bond market unit)
        ///</summary>
        [EnumMember] XBC = 957,

        ///<summary>
        ///European Unit of Account 17 (E.U.A.-17) (bond market unit)
        ///</summary>
        [EnumMember] XBD = 958,

        ///<summary>
        ///East Caribbean dollar
        ///</summary>
        [EnumMember] XCD = 951,

        ///<summary>
        ///Special drawing rights
        ///</summary>
        [EnumMember] XDR = 960,

        ///<summary>
        ///UIC franc (special settlement currency)
        ///</summary>
        [EnumMember] XFU = -1,

        ///<summary>
        ///CFA franc BCEAO
        ///</summary>
        [EnumMember] XOF = 952,

        ///<summary>
        ///Palladium (one troy ounce)
        ///</summary>
        [EnumMember] XPD = 964,

        ///<summary>
        ///CFP franc (franc Pacifique)
        ///</summary>
        [EnumMember] XPF = 953,

        ///<summary>
        ///Platinum (one troy ounce)
        ///</summary>
        [EnumMember] XPT = 962,

        ///<summary>
        ///SUCRE
        ///</summary>
        [EnumMember] XSU = 994,

        ///<summary>
        ///Code reserved for testing purposes
        ///</summary>
        [EnumMember] XTS = 963,

        ///<summary>
        ///ADB Unit of Account
        ///</summary>
        [EnumMember] XUA = 965,

        ///<summary>
        ///No currency 
        ///</summary>
        [EnumMember] XXX = 999,

        ///<summary>
        ///Yemeni rial
        ///</summary>
        [EnumMember] YER = 886,

        ///<summary>
        ///South African rand
        ///</summary>
        [EnumMember] ZAR = 710,

        ///<summary>
        ///Zambian kwacha
        ///</summary>
        [EnumMember] ZMW = 967,

        ///<summary>
        ///Zimbabwe dollar
        ///</summary>
        [EnumMember] ZWD = 932,
        // ReSharper restore InconsistentNaming
    }
}