﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FF8
{
    public class Font
    {
        private Texture2D sysfnt; //21x10 characters; char is always 12x12
        private TextureHandler sysfntbig; //21x10 characters; char is always 24x24; 2 files side by side; sysfnt00 is same as sysfld00, but sysfnt00 is missing sysfnt01
        private Texture2D menuFont;

        #region CharTable

        private static readonly Dictionary<byte, string> chartable = new Dictionary<byte, string>
        {
            {0x00, "\0"},// pos:-32, col:0, row:-1 -- is end of a string. MSG files have more than one string sepperated by \0
            {0x01, ""},// pos:-31, col:0, row:0 --
            {0x02, "\n"},// pos:-30, col:0, row:0 -- new line
            {0x03, ""},// pos:-29, col:0, row:0 -- special character. {0x03,0x40 = [Angelo's Name]}
            {0x04, ""},// pos:-28, col:0, row:0 --
            {0x05, ""},// pos:-27, col:0, row:0 --
            {0x06, ""},// pos:-26, col:0, row:0 --
            {0x07, ""},// pos:-25, col:0, row:0 --
            {0x08, ""},// pos:-24, col:0, row:0 --
            {0x09, ""},// pos:-23, col:0, row:0 --
            {0x0A, ""},// pos:-22, col:0, row:0 --
            {0x0B, ""},// pos:-21, col:0, row:0 --
            {0x0C, ""},// pos:-20, col:0, row:0 -- <MAGBYTE>
            {0x0D, ""},// pos:-19, col:0, row:0 --
            {0x0E, ""},// pos:-18, col:0, row:0 -- <$>
            {0x0F, ""},// pos:-17, col:0, row:0 --
            {0x10, ""},// pos:-16, col:0, row:0 --
            {0x11, ""},// pos:-15, col:0, row:0 --
            {0x12, ""},// pos:-14, col:0, row:0 --
            {0x13, ""},// pos:-13, col:0, row:0 --
            {0x14, ""},// pos:-12, col:0, row:0 --
            {0x15, ""},// pos:-11, col:0, row:0 --
            {0x16, ""},// pos:-10, col:0, row:1 --
            {0x17, ""},// pos:-9, col:0, row:0 --
            {0x18, ""},// pos:-8, col:0, row:0 --
            {0x19, ""},// pos:-7, col:0, row:0 --
            {0x1A, ""},// pos:-6, col:0, row:0 --
            {0x1B, ""},// pos:-5, col:0, row:0 --
            {0x1C, ""},// pos:-4, col:0, row:0 --
            {0x1D, ""},// pos:-3, col:0, row:0 --
            {0x1E, ""},// pos:-2, col:0, row:0 --
            {0x1F, ""},// pos:-1, col:0, row:0 --
            {0x20, " "},// pos:0, col:1, row:1 -- Start of font texture
            {0x21, "0"},// pos:1, col:2, row:1 --
            {0x22, "1"},// pos:2, col:3, row:1 --
            {0x23, "2"},// pos:3, col:4, row:1 --
            {0x24, "3"},// pos:4, col:5, row:1 --
            {0x25, "4"},// pos:5, col:6, row:1 --
            {0x26, "5"},// pos:6, col:7, row:1 --
            {0x27, "6"},// pos:7, col:8, row:1 --
            {0x28, "7"},// pos:8, col:9, row:1 --
            {0x29, "8"},// pos:9, col:10, row:1 --
            {0x2A, "9"},// pos:10, col:11, row:1 --
            {0x2B, "%"},// pos:11, col:12, row:2 --
            {0x2C, "/"},// pos:12, col:13, row:2 --
            {0x2D, ":"},// pos:13, col:14, row:2 --
            {0x2E, "!"},// pos:14, col:15, row:2 --
            {0x2F, "?"},// pos:15, col:16, row:2 --
            {0x30, "…"},// pos:16, col:17, row:2 --
            {0x31, "+"},// pos:17, col:18, row:2 --
            {0x32, "-"},// pos:18, col:19, row:2 --
            {0x33, "="},// pos:19, col:20, row:2 --
            {0x34, "*"},// pos:20, col:21, row:2 --
            {0x35, "&"},// pos:21, col:1, row:2 --
            {0x36, "「"},// pos:22, col:2, row:2 --
            {0x37, "」"},// pos:23, col:3, row:2 --
            {0x38, "("},// pos:24, col:4, row:2 --
            {0x39, ")"},// pos:25, col:5, row:2 --
            {0x3A, "·"},// pos:26, col:6, row:2 --
            {0x3B, "."},// pos:27, col:7, row:2 --
            {0x3C, ","},// pos:28, col:8, row:2 --
            {0x3D, "~"},// pos:29, col:9, row:2 --
            {0x3E, "“"},// pos:30, col:10, row:2 --
            {0x3F, "”"},// pos:31, col:11, row:2 --
            {0x40, "'"},// pos:32, col:12, row:3 --
            {0x41, "#"},// pos:33, col:13, row:3 --
            {0x42, "$"},// pos:34, col:14, row:3 --
            {0x43, "`"},// pos:35, col:15, row:3 --
            {0x44, "_"},// pos:36, col:16, row:3 --
            {0x45, "A"},// pos:37, col:17, row:3 --
            {0x46, "B"},// pos:38, col:18, row:3 --
            {0x47, "C"},// pos:39, col:19, row:3 --
            {0x48, "D"},// pos:40, col:20, row:3 --
            {0x49, "E"},// pos:41, col:21, row:3 --
            {0x4A, "F"},// pos:42, col:1, row:3 --
            {0x4B, "G"},// pos:43, col:2, row:3 --
            {0x4C, "H"},// pos:44, col:3, row:3 --
            {0x4D, "I"},// pos:45, col:4, row:3 --
            {0x4E, "J"},// pos:46, col:5, row:3 --
            {0x4F, "K"},// pos:47, col:6, row:3 --
            {0x50, "L"},// pos:48, col:7, row:3 --
            {0x51, "M"},// pos:49, col:8, row:3 --
            {0x52, "N"},// pos:50, col:9, row:3 --
            {0x53, "O"},// pos:51, col:10, row:3 --
            {0x54, "P"},// pos:52, col:11, row:3 --
            {0x55, "Q"},// pos:53, col:12, row:4 --
            {0x56, "R"},// pos:54, col:13, row:4 --
            {0x57, "S"},// pos:55, col:14, row:4 --
            {0x58, "T"},// pos:56, col:15, row:4 --
            {0x59, "U"},// pos:57, col:16, row:4 --
            {0x5A, "V"},// pos:58, col:17, row:4 --
            {0x5B, "W"},// pos:59, col:18, row:4 --
            {0x5C, "X"},// pos:60, col:19, row:4 --
            {0x5D, "Y"},// pos:61, col:20, row:4 --
            {0x5E, "Z"},// pos:62, col:21, row:4 --
            {0x5F, "a"},// pos:63, col:1, row:4 --
            {0x60, "b"},// pos:64, col:2, row:4 --
            {0x61, "c"},// pos:65, col:3, row:4 --
            {0x62, "d"},// pos:66, col:4, row:4 --
            {0x63, "e"},// pos:67, col:5, row:4 --
            {0x64, "f"},// pos:68, col:6, row:4 --
            {0x65, "g"},// pos:69, col:7, row:4 --
            {0x66, "h"},// pos:70, col:8, row:4 --
            {0x67, "i"},// pos:71, col:9, row:4 --
            {0x68, "j"},// pos:72, col:10, row:4 --
            {0x69, "k"},// pos:73, col:11, row:4 --
            {0x6A, "l"},// pos:74, col:12, row:5 --
            {0x6B, "m"},// pos:75, col:13, row:5 --
            {0x6C, "n"},// pos:76, col:14, row:5 --
            {0x6D, "o"},// pos:77, col:15, row:5 --
            {0x6E, "p"},// pos:78, col:16, row:5 --
            {0x6F, "q"},// pos:79, col:17, row:5 --
            {0x70, "r"},// pos:80, col:18, row:5 --
            {0x71, "s"},// pos:81, col:19, row:5 --
            {0x72, "t"},// pos:82, col:20, row:5 --
            {0x73, "u"},// pos:83, col:21, row:5 --
            {0x74, "v"},// pos:84, col:1, row:5 --
            {0x75, "w"},// pos:85, col:2, row:5 --
            {0x76, "x"},// pos:86, col:3, row:5 --
            {0x77, "y"},// pos:87, col:4, row:5 --
            {0x78, "z"},// pos:88, col:5, row:5 --
            {0x79, "À"},// pos:89, col:6, row:5 --
            {0x7A, "Á"},// pos:90, col:7, row:5 --
            {0x7B, "Â"},// pos:91, col:8, row:5 --
            {0x7C, "Ä"},// pos:92, col:9, row:5 --
            {0x7D, "Ç"},// pos:93, col:10, row:5 --
            {0x7E, "È"},// pos:94, col:11, row:5 --
            {0x7F, "É"},// pos:95, col:12, row:6 --
            {0x80, "Ê"},// pos:96, col:13, row:6 --
            {0x81, "Ë"},// pos:97, col:14, row:6 --
            {0x82, "Ì"},// pos:98, col:15, row:6 --
            {0x83, "Í"},// pos:99, col:16, row:6 --
            {0x84, "Î"},// pos:100, col:17, row:6 --
            {0x85, "Ï"},// pos:101, col:18, row:6 --
            {0x86, "Ñ"},// pos:102, col:19, row:6 --
            {0x87, "Ò"},// pos:103, col:20, row:6 --
            {0x88, "Ó"},// pos:104, col:21, row:6 --
            {0x89, "Ô"},// pos:105, col:1, row:6 --
            {0x8A, "Ö"},// pos:106, col:2, row:6 --
            {0x8B, "Ù"},// pos:107, col:3, row:6 --
            {0x8C, "Ú"},// pos:108, col:4, row:6 --
            {0x8D, "Û"},// pos:109, col:5, row:6 --
            {0x8E, "Ü"},// pos:110, col:6, row:6 --
            {0x8F, "Œ"},// pos:111, col:7, row:6 --
            {0x90, "Ʀ"},// pos:112, col:8, row:6 --
            {0x91, "à"},// pos:113, col:9, row:6 --
            {0x92, "á"},// pos:114, col:10, row:6 --
            {0x93, "â"},// pos:115, col:11, row:6 --
            {0x94, "ä"},// pos:116, col:12, row:7 --
            {0x95, "ç"},// pos:117, col:13, row:7 --
            {0x96, "è"},// pos:118, col:14, row:7 --
            {0x97, "é"},// pos:119, col:15, row:7 --
            {0x98, "ê"},// pos:120, col:16, row:7 --
            {0x99, "ë"},// pos:121, col:17, row:7 --
            {0x9A, "ì"},// pos:122, col:18, row:7 --
            {0x9B, "í"},// pos:123, col:19, row:7 --
            {0x9C, "î"},// pos:124, col:20, row:7 --
            {0x9D, "ï"},// pos:125, col:21, row:7 --
            {0x9E, "ñ"},// pos:126, col:1, row:7 --
            {0x9F, "ò"},// pos:127, col:2, row:7 --
            {0xA0, "ó"},// pos:128, col:3, row:7 --
            {0xA1, "ô"},// pos:129, col:4, row:7 --
            {0xA2, "ö"},// pos:130, col:5, row:7 --
            {0xA3, "ù"},// pos:131, col:6, row:7 --
            {0xA4, "ú"},// pos:132, col:7, row:7 --
            {0xA5, "û"},// pos:133, col:8, row:7 --
            {0xA6, "ü"},// pos:134, col:9, row:7 --
            {0xA7, "œ"},// pos:135, col:10, row:7 --
            {0xA8, "Ⅷ"},// pos:136, col:11, row:7 --
            {0xA9, "["},// pos:137, col:12, row:8 --
            {0xAA, "]"},// pos:138, col:13, row:8 --
            {0xAB, "⬛"},// pos:139, col:14, row:8 --
            {0xAC, "⦾"},// pos:140, col:15, row:8 --
            {0xAD, "◆"},// pos:141, col:16, row:8 --
            {0xAE, "【"},// pos:142, col:17, row:8 --
            {0xAF, "】"},// pos:143, col:18, row:8 --
            {0xB0, "⬜"},// pos:144, col:19, row:8 --
            {0xB1, "★"},// pos:145, col:20, row:8 --
            {0xB2, "『"},// pos:146, col:21, row:8 --
            {0xB3, "』"},// pos:147, col:1, row:8 --
            {0xB4, "▽"},// pos:148, col:2, row:8 --
            {0xB5, ";"},// pos:149, col:3, row:8 --
            {0xB6, "▼"},// pos:150, col:4, row:8 --
            {0xB7, "‾"},// pos:151, col:5, row:8 --
            {0xB8, "×"},// pos:152, col:6, row:8 --
            {0xB9, "☆"},// pos:153, col:7, row:8 --
            {0xBA, "’"},// pos:154, col:8, row:8 --
            {0xBB, "↓"},// pos:155, col:9, row:8 --
            {0xBC, "°"},// pos:156, col:10, row:8 --
            {0xBD, "¡"},// pos:157, col:11, row:8 --
            {0xBE, "¿"},// pos:158, col:12, row:9 --
            {0xBF, "—"},// pos:159, col:13, row:9 --
            {0xC0, "«"},// pos:160, col:14, row:9 --
            {0xC1, "»"},// pos:161, col:15, row:9 --
            {0xC2, "±"},// pos:162, col:16, row:9 --
            {0xC3, ""},// pos:163, col:17, row:9 --
            {0xC4, "♫"},// pos:164, col:18, row:9 --
            {0xC5, "↑"},// pos:165, col:19, row:9 --
            {0xC6, "VI"},// pos:166, col:20, row:9 --
            {0xC7, "II"},// pos:167, col:21, row:9 --
            {0xC8, "¡"},// pos:168, col:1, row:9 --
            {0xC9, "™"},// pos:169, col:2, row:9 --
            {0xCA, "<"},// pos:170, col:3, row:9 --
            {0xCB, ">"},// pos:171, col:4, row:9 --
            {0xCC, "GA"},// pos:172, col:5, row:9 --
            {0xCD, "ME"},// pos:173, col:6, row:9 --
            {0xCE, "FO"},// pos:174, col:7, row:9 --
            {0xCF, "LD"},// pos:175, col:8, row:9 --
            {0xD0, "ER"},// pos:176, col:9, row:9 --
            {0xD1, "Sl"},// pos:177, col:10, row:9 --
            {0xD2, "ot"},// pos:178, col:11, row:9 --
            {0xD3, "ing"},// pos:179, col:12, row:10 --
            {0xD4, "St"},// pos:180, col:13, row:10 --
            {0xD5, "ec"},// pos:181, col:14, row:10 --
            {0xD6, "kp"},// pos:182, col:15, row:10 --
            {0xD7, "la"},// pos:183, col:16, row:10 --
            {0xD8, ":z"},// pos:184, col:17, row:10 --
            {0xD9, "Fr"},// pos:185, col:18, row:10 --
            {0xDA, "nt"},// pos:186, col:19, row:10 --
            {0xDB, "elng"},// pos:187, col:20, row:10 --
            {0xDC, "re"},// pos:188, col:21, row:10 --
            {0xDD, "S:"},// pos:189, col:1, row:10 --
            {0xDE, "so"},// pos:190, col:2, row:10 --
            {0xDF, "Ra"},// pos:191, col:3, row:10 --
            {0xE0, "nu"},// pos:192, col:4, row:10 --
            {0xE1, "ra"},// pos:193, col:5, row:10 --
            {0xE2, "®"},// pos:194, col:6, row:10 -- End of font texture
            {0xE3, ""},// pos:195, col:0, row:0 --
            {0xE4, ""},// pos:196, col:0, row:0 --
            {0xE5, ""},// pos:197, col:0, row:0 --
            {0xE6, ""},// pos:198, col:0, row:0 --
            {0xE7, ""},// pos:199, col:0, row:0 --
            {0xE8, ""},// pos:200, col:0, row:0 --
            {0xE9, ""},// pos:201, col:0, row:0 --
            {0xEA, ""},// pos:202, col:0, row:0 --
            {0xEB, ""},// pos:203, col:0, row:0 --
            {0xEC, ""},// pos:204, col:0, row:0 --
            {0xED, ""},// pos:205, col:0, row:0 --
            {0xEE, ""},// pos:206, col:0, row:0 --
            {0xEF, ""},// pos:207, col:0, row:0 --
            {0xF0, ""},// pos:208, col:0, row:0 --
            {0xF1, ""},// pos:209, col:0, row:0 --
            {0xF2, ""},// pos:210, col:0, row:0 --
            {0xF3, ""},// pos:211, col:0, row:0 --
            {0xF4, ""},// pos:212, col:0, row:0 --
            {0xF5, ""},// pos:213, col:0, row:0 --
            {0xF6, ""},// pos:214, col:0, row:0 --
            {0xF7, ""},// pos:215, col:0, row:0 --
            {0xF8, ""},// pos:216, col:0, row:0 --
            {0xF9, ""},// pos:217, col:0, row:0 --
            {0xFA, ""},// pos:218, col:0, row:0 --
            {0xFB, ""},// pos:219, col:0, row:0 --
            {0xFC, ""},// pos:220, col:0, row:0 --
            {0xFD, ""},// pos:221, col:0, row:0 --
            {0xFE, ""},// pos:222, col:0, row:0 --
            {0xFF, ""},// pos:223, col:0, row:0 --

            //{0x00, "\0"}, // I think \0 is a new string. if you read in a msg file it a array of strings each ending with \0
            //{0x02, "\n"}, // changed \n to signal draw text to make a new line
            //{0x03, ""},
            //{0x04, "" }, //Probably
            //{0x0E, "" }, //Probably
            //{0x20, " "},
            //{0x21, "0"},
            //{0x22, "1"},
            //{0x23, "2"},
            //{0x24, "3"},
            //{0x25, "4"},
            //{0x26, "5"},
            //{0x27, "6"},
            //{0x28, "7"},
            //{0x29, "8"},
            //{0x2A, "9"},
            //{0x2B, "%"},
            //{0x2C, "/"},
            //{0x2D, ":"},
            //{0x2E, "!"},
            //{0x2F, "?"},
            //{0x30, "…"},
            //{0x31, "+"},
            //{0x32, "-"},
            //{0x33, "SPECIAL CHARACTER TODO"},
            //{0x34, "*"},
            //{0x35, "&"},
            //{0x36, "SPECIAL CHARACTER TODO" },
            //{0x37, "SPECIAL CHARACTER TODO" },
            //{0x38, "("},
            //{0x39, ")"},
            //{0x3A, "SPECIAL CHARACTER TODO"},
            //{0x3B, "."},
            //{0x3C, ","},
            //{0x3D, "~"},
            //{0x3E, "SPECIAL CHARACTER TODO"},
            //{0x3F, "SPECIAL CHARACTER TODO"},
            //{0x40, "'"},
            //{0x41, "#"},
            //{0x42, "$"},
            //{0x43, "`"},
            //{0x44, "_"},
            //{0x45, "A"},
            //{0x46, "B"},
            //{0x47, "C"},
            //{0x48, "D"},
            //{0x49, "E"},
            //{0x4A, "F"},
            //{0x4B, "G"},
            //{0x4C, "H"},
            //{0x4D, "I"},
            //{0x4E, "J"},
            //{0x4F, "K"},
            //{0x50, "L"},
            //{0x51, "M"},
            //{0x52, "N"},
            //{0x53, "O"},
            //{0x54, "P"},
            //{0x55, "Q"},
            //{0x56, "R"},
            //{0x57, "S"},
            //{0x58, "T"},
            //{0x59, "U"},
            //{0x5A, "V"},
            //{0x5B, "W"},
            //{0x5C, "X"},
            //{0x5D, "Y"},
            //{0x5E, "Z"},
            //{0x5F, "a"},
            //{0x60, "b"},
            //{0x61, "c"},
            //{0x62, "d"},
            //{0x63, "e"},
            //{0x64, "f"},
            //{0x65, "g"},
            //{0x66, "h"},
            //{0x67, "i"},
            //{0x68, "j"},
            //{0x69, "k"},
            //{0x6A, "l"},
            //{0x6B, "m"},
            //{0x6C, "n"},
            //{0x6D, "o"},
            //{0x6E, "p"},
            //{0x6F, "q"},
            //{0x70, "r"},
            //{0x71, "s"},
            //{0x72, "t"},
            //{0x73, "u"},
            //{0x74, "v"},
            //{0x75, "w"},
            //{0x76, "x"},
            //{0x77, "y"},
            //{0x78, "z"},
            //{0x79, "Ł"},
            //{0x7C, "Ä"},
            //{0x88, "Ó"},
            //{0x8A, "Ö"},
            //{0x8E, "Ü"},
            //{0x90, "ß"},
            //{0x94, "ä"},
            //{0xA0, "ó"},
            //{0xA2, "ö"},
            //{0xA6, "ü"},
            //{0xA8, "Ⅷ"},
            //{0xA9, "["},
            //{0xAA, "]"},
            //{0xAB, "[SQUARE]"},
            //{0xAC, "@"},
            //{0xAD, "[SSQUARE]"},
            //{0xAE, "{"},
            //{0xAF, "}"},
            //{0xC6, "Ⅵ"},
            //{0xC7, "Ⅱ"},
            //{0xC9, "™"},
            //{0xCA, "<"},
            //{0xCB, ">"},
            //{0xE8, "in"},
            //{0xE9, "e "},
            //{0xEA, "ne"},
            //{0xEB, "to"},
            //{0xEC, "re"},
            //{0xED, "HP"},
            //{0xEE, "l "},
            //{0xEF, "ll"},
            //{0xF0, "GF"},
            //{0xF1, "nt"},
            //{0xF2, "il"},
            //{0xF3, "o "},
            //{0xF4, "ef"},
            //{0xF5, "on"},
            //{0xF6, " w"},
            //{0xF7, " r"},
            //{0xF8, "wi"},
            //{0xF9, "fi"},
            //{0xFB, "s "},
            //{0xFC, "ar"},
            //{0xFE, " S"},
            //{0xFF, "ag"}
        };

        #endregion CharTable

        public enum ColorID
        {
            Dark_Gray, Grey, Yellow, Red, Green, Blue, Purple, White
        }

        public Font() => LoadFonts();

        internal void LoadFonts()
        {
            ArchiveWorker aw = new ArchiveWorker(Memory.Archives.A_MENU);
            string sysfntTdwFilepath = aw.GetListOfFiles().First(x => x.ToLower().Contains("sysfnt.tdw"));
            string sysfntFilepath = aw.GetListOfFiles().First(x => x.ToLower().Contains("sysfnt.tex"));
            TEX tex = new TEX(ArchiveWorker.GetBinaryFile(Memory.Archives.A_MENU, sysfntFilepath));
            sysfnt = tex.GetTexture((int)ColorID.White);
            sysfntbig = new TextureHandler("sysfld{0:00}.tex", tex, 2, 1, (int)ColorID.White);

            ReadTdw(ArchiveWorker.GetBinaryFile(Memory.Archives.A_MENU, sysfntTdwFilepath));
        }

        internal void ReadTdw(byte[] Tdw)
        {
            int widthPointer = BitConverter.ToInt32(Tdw, 0);
            int dataPointer = BitConverter.ToInt32(Tdw, 4);
            TIM2 tim = new TIM2(Tdw, (uint)dataPointer);
            menuFont = new Texture2D(Memory.graphics.GraphicsDevice, tim.GetWidth, tim.GetHeight);
            menuFont.SetData(tim.CreateImageBuffer(tim.GetClutColors(ColorID.White)));
        }

        public Rectangle CalcBasicTextArea(string buffer, Vector2 pos, Vector2 zoom, int whichFont = 0, int isMenu = 0, float Fade = 1.0f) => CalcBasicTextArea(buffer, (int)pos.X, (int)pos.Y, zoom.X, zoom.Y);

        public Rectangle CalcBasicTextArea(string buffer, Point pos, Vector2 zoom, int whichFont = 0, int isMenu = 0, float Fade = 1.0f) => CalcBasicTextArea(buffer, pos.X, pos.Y, zoom.X, zoom.Y);

        public Rectangle CalcBasicTextArea(string buffer, int x, int y, float zoomWidth = 1f, float zoomHeight = 1f, int whichFont = 0)
        {
            Rectangle ret = new Rectangle(x, y, 0, 0);
            Point real = new Point(x, y);
            int charCountWidth = whichFont == 0 ? 21 : 10;
            int charSize = whichFont == 0 ? 12 : 24;
            Vector2 zoom = new Vector2(zoomWidth, zoomHeight);
            Point size = (new Vector2(charSize, charSize) * zoom * Memory.Scale()).ToPoint();
            foreach (char c in buffer)
            {
                if (c == '\n')
                {
                    real.X = x;
                    real.Y += size.Y;
                    continue;
                }
                int verticalPosition = (char)(c - 32) / charCountWidth;
                //i.e. 1280 is 100%, 640 is 50% and therefore 2560 is 200% which means multiply by 0.5f or 2.0f
                real.X += size.X;
                int curWidth = real.X - x;
                if (curWidth > ret.Width)
                    ret.Width = curWidth;
            }
            ret.Height = size.Y + (real.Y - y);
            return ret;
        }

        public Rectangle RenderBasicText(string buffer, Vector2 pos, Vector2 zoom, int whichFont = 0, int isMenu = 0, float Fade = 1.0f) => RenderBasicText(buffer, (int)pos.X, (int)pos.Y, zoom.X, zoom.Y, whichFont, isMenu, Fade);

        public Rectangle RenderBasicText(string buffer, Point pos, Vector2 zoom, int whichFont = 0, int isMenu = 0, float Fade = 1.0f) => RenderBasicText(buffer, pos.X, pos.Y, zoom.X, zoom.Y, whichFont, isMenu, Fade);

        public Rectangle RenderBasicText(string buffer, int x, int y, float zoomWidth = 1f, float zoomHeight = 1f, int whichFont = 0, int isMenu = 0, float Fade = 1.0f)
        {
            Rectangle ret = new Rectangle(x, y, 0, 0);
            Point real = new Point(x, y);
            int charCountWidth = 21;
            int charSize = 12; //pixelhandler does the 2x scaling on the fly.
            Vector2 zoom = new Vector2(zoomWidth, zoomHeight);
            Point size = (new Vector2(charSize, charSize) * zoom * Memory.Scale()).ToPoint();
            foreach (char c in buffer)
            {
                char deltaChar = (char)(c - 32);
                int verticalPosition = deltaChar / charCountWidth;
                //i.e. 1280 is 100%, 640 is 50% and therefore 2560 is 200% which means multiply by 0.5f or 2.0f
                if (c == '\n')
                {
                    real.X = x;
                    real.Y += size.Y;
                    continue;
                }
                Rectangle destRect = new Rectangle(real, size);
                // if you use Memory.SpriteBatchStartAlpha(SamplerState.PointClamp); you won't need
                // to trim last pixel. but it doesn't look good on low res fonts.
                Rectangle sourceRect = new Rectangle((deltaChar - (verticalPosition * charCountWidth)) * charSize,
                    verticalPosition * charSize,
                    charSize,
                    charSize);

                if (whichFont == 0 || isMenu == 1)
                {
                    //trim pixels to remove texture filtering artifacts.
                    sourceRect.Width -= 1;
                    sourceRect.Height -= 1;
                    Memory.spriteBatch.Draw(isMenu == 1 ? menuFont : sysfnt,
                        destRect,
                        sourceRect,
                    Color.White * Fade);
                }
                else
                    sysfntbig.Draw(destRect, sourceRect, Color.White * Fade);

                real.X += size.X;
                int curWidth = real.X - x;
                if (curWidth > ret.Width)
                    ret.Width = curWidth;
            }
            ret.Height = size.Y + (real.Y - y);
            return ret;
        }

        //dirty, do not use for anything else than translating for your own purpouses. I'm just lazy
        public static string CipherDirty(string s)
        {
            string str = "";
            foreach (char n in s)
            {
                if (n == '\n') { str += n; continue; }
                foreach (KeyValuePair<byte, string> kvp in chartable)
                    if (kvp.Value.Length == 1)
                        if (kvp.Value[0] == n)
                            str += (char)(kvp.Key);
            }
            return str.Replace("\0", "");
        }

        /*
         * myst6re code
         *
         *
         *
         for(int i=0 ; i<size ; ++i) {
		caract = (quint8)ff8Text.at(i);
		if(caract==0) break;
		switch(caract) {
		case 0x1: // New Page
			if(height>maxH)	maxH = height;
			if(width>maxW)	maxW = width;
			width = 15;
			height = 28;
			pagesPos.append(i+1);
			break;

		case 0x2: // \n
			if(width>maxW)	maxW = width;
			++line;
			width = (ask_first<=line && ask_last>=line ? 79 : 15);//32+15+32 (padding for arrow) or 15
			height += 16;
			break;

		case 0x3: // Character names
			caract = (quint8)ff8Text.at(++i);
			if(caract>=0x30 && caract<=0x3a)
				width += namesWidth[caract-0x30];
			else if(caract==0x40)
				width += namesWidth[11];
			else if(caract==0x50)
				width += namesWidth[12];
			else if(caract==0x60)
				width += namesWidth[13];
			break;

		case 0x4: // Vars
			caract = (quint8)ff8Text.at(++i);
			if((caract>=0x20 && caract<=0x27) || (caract>=0x30 && caract<=0x37))
				width += font->charWidth(0, 1);// 0
			else if(caract>=0x40 && caract<=0x47)
				width += font->charWidth(0, 1)*8;// 00000000
			break;

		case 0x5: // Icons
			caract = (quint8)ff8Text.at(++i)-0x20;
            if(caract<96)
				width += iconWidth[caract]+iconPadding[caract];
			break;

		case 0xe: // Locations
			caract = (quint8)ff8Text.at(++i);
			if(caract>=0x20 && caract<=0x27)
				width += locationsWidth[caract-0x20];
			break;

		case 0x19: // Jap 1
			if(jp) {
				caract = (quint8)ff8Text.at(++i);
				if(caract>=0x20)
					width += font->charWidth(1, caract-0x20);
			}
			break;

		case 0x1a: // Jap 2
			if(jp) {
				caract = (quint8)ff8Text.at(++i);
				if(caract>=0x20)
					width += font->charWidth(2, caract-0x20);
			}
			break;

		case 0x1b: // Jap 3
			if(jp) {
				caract = (quint8)ff8Text.at(++i);
				if(caract>=0x20)
					width += font->charWidth(3, caract-0x20);
			}
			break;

		case 0x1c: // Jap 4
			if(tdwFile) {
				caract = (quint8)ff8Text.at(++i);
				if(caract>=0x20)
					width += tdwFile->charWidth(0, caract-0x20);
			}
			break;

		default:
			if(caract<0x20)
				++i;
			else if(jp) {
				width += font->charWidth(0, caract-0x20);
			} else {
				if(caract<232)
					width += font->charWidth(0, caract-0x20);
				else if(caract>=232)
					width += font->charWidth(0, (quint8)optimisedDuo[caract-232][0]) + font->charWidth(0, (quint8)optimisedDuo[caract-232][1]);
			}
			break;
		}
	}

	if(height>maxH)	maxH = height;
	if(width>maxW)	maxW = width;
	if(maxW>322)	maxW = 322;
	if(maxH>226)	maxH = 226;

update();
        */
    }
}