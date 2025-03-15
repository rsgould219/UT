using System.Xml;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcedemicController : MonoBehaviour
{
    public Course[] courses = new Course[953];
    public int[] electiveList = new int[19];
    public List<List<Course>> cseCousreMap= new List<List<Course>>();
    public static AcedemicController Instance;
    public Section clickedSection;
    public Zone selectedZ;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        loadCourses();
        //Test add section
        //courses[0].newSection();
    }

    // Update is called once per frame 
    void Update(){
    //Debug.Log(courses[0].sections.Count + " " + courses[0].sectionsS.Count);
    }

    public bool checkLabId(int id){
        if (id == 13 || id == 25 || id == 26 || id == 40 || id == 43 || id == 247)
            return true;
        if (id == 178 || id == 162 || id == 108 || id == 109)
            return true;
        else
            return false;
    }

    public bool checkSciId(int id){
        if(id == 507 || id == 9 || id == 10)
            return true;
        else
            return false;
    }
    //write sections as two elements, one for fall and one for spring
    public void writeXml(XmlWriter writer){
        foreach(Course c in courses){
            if(c != null){
                foreach(Section s in c.sections){
                    writer.WriteStartElement("Section");
                    s.writeXml(writer);
                }
                foreach(Section s in c.sectionsS){
                    writer.WriteStartElement("SectionS");
                    s.writeXml(writer);
                }
            }
        }
        //writer.WriteEndElement();
    }

    public void loadCourses(){
        courses[0] = new Course(1, "ENGR101", 2);
        courses[1] = new Course(2, "CSE101", 2);
        courses[2] = new Course(3, "ENG101", 3);
        courses[3] = new Course(4, "ENG102", 3);
        courses[4] = new Course(5, "MATH110", 3);
        courses[5] = new Course(6, "MATH120", 3);
        courses[6] = new Course(7, "MATH130", 3);
        courses[7] = new Course(8, "CSE102", 2);
        courses[8] = new Course(9, "CSE103", 2);
        courses[9] = new Course(10, "PHY110", 3);
        courses[10] = new Course(11, "CHM110", 3);
        courses[11] = new Course(12, "BIO101", 3);
        courses[12] = new Course(13, "CHM111", 1);
        courses[13] = new Course(14, "PHY112", 1);
        courses[14] = new Course(15, "MATH111", 1);
        courses[15] = new Course(16, "MATH121", 1);
        courses[16] = new Course(17, "MATH131", 1);
        courses[17] = new Course(18, "ECO101", 3);
        courses[18] = new Course(19, "MATH131", 1);
        courses[19] = new Course(20, "null", 3);
        courses[20] = new Course(21, "MEC101", 2); // 1st intro
        courses[21] = new Course(22, "MEC102", 2); // 2rd intro/design
        courses[22] = new Course(23, "MEC103", 2); //3nd design
        courses[23] = new Course(24, "CHM120", 3);//3rd
        courses[24] = new Course(25, "CHE101", 2);
        courses[25] = new Course(26, "CHM121", 1); //lab 120 3rd
        courses[26] = new Course(27, "CHM112", 1);//lab
        courses[27] = new Course(28, "ENGR120", 2);//3rdeng stats
        courses[28] = new Course(29, "MATH230", 3);
        courses[29] = new Course(30, "PHY120", 3);
        courses[30] = new Course(31, "PHY121", 1);
        courses[31] = new Course(32, "PHY122", 1);
        courses[32] = new Course(33, "MATH208", 3);

        courses[33] = new Course(34, "CHE150", 3);//5rd semester
        courses[34] = new Course(35, "CHE210", 3);//6th
        courses[35] = new Course(36, "CHE342", 3);//7th semester
        courses[36] = new Course(37, "CHE131", 3);//3rd semester
        courses[37] = new Course(38, "CHE205", 4);//5th
        courses[38] = new Course(39, "CHE244", 3);//6th semester
        courses[39] = new Course(40, "CHE140", 3);//4th fluid
        courses[40] = new Course(41, "CHE202", 2);//7th semester solo lab, lection at 178
        courses[41] = new Course(42, "CHE333", 3);//7th
        courses[42] = new Course(43, "CHE210", 4);//4th
        courses[43] = new Course(44, "CHE203", 2);//8th solo lab
        courses[44] = new Course(45, "CHE334", 3);//8th semester
        courses[45] = new Course(46, "CHE380", 3);//8th option for mat
        courses[46] = new Course(47, "CHE306", 3);//options
        courses[47] = new Course(48, "CHE321", 3);
        courses[48] = new Course(49, "CHE341", 3);
        courses[49] = new Course(50, "CHE342", 3);
        courses[50] = new Course(51, "CHE344", 3);
        courses[51] = new Course(52, "CHE396", 3);
        courses[52] = new Course(53, "CHE371", 3);
        courses[53] = new Course(54, "CHE339", 3);
        courses[54] = new Course(55, "CHE363", 3);
        courses[55] = new Course(56, "CHE365", 3);
        courses[56] = new Course(57, "CHE376", 3);
        courses[57] = new Course(58, "CHE377", 3);
        courses[58] = new Course(59, "CHE373", 3);
        courses[59] = new Course(60, "CHE375", 3);
        courses[60] = new Course(61, "CHE374", 3);
        courses[61] = new Course(62, "CHE398", 3);
        courses[62] = new Course(63, "CHE392", 3);
        courses[63] = new Course(64, "CHE391", 3);
        courses[64] = new Course(65, "CHE317", 3);
        courses[65] = new Course(66, "CHE394", 3);
        courses[66] = new Course(67, "CHE393", 3);

        courses[67] = new Course(68, "CIV140", 3); //lab 110 desing 3rd
        courses[68] = new Course(69, "CIV120", 2);//2rd semester survey/design
        courses[69] = new Course(70, "CIV129", 2);//3rdciv stats 2
        courses[70] = new Course(71, "CIV159", 3);//4th
        courses[71] = new Course(72, "CIV206", 2);//4th
        courses[72] = new Course(73, "CIV170", 4);//4th
        courses[73] = new Course(74, "CIV122", 3);//5th
        courses[74] = new Course(75, "CIV133", 3);//5th
        courses[75] = new Course(76, "CIV140", 3);//5th
        courses[76] = new Course(77, "CIV259", 4);//5th
        courses[77] = new Course(78, "CIV200", 3);//6th
        courses[78] = new Course(79, "CIV220", 3);//6th
        courses[79] = new Course(80, "CIV240", 3);//6th
        courses[80] = new Course(81, "CIV260", 3);//6th
        courses[81] = new Course(82, "CIV360", 3);//7th
        courses[82] = new Course(83, "CIV280", 3);//7th
        courses[83] = new Course(84, "CIV361", 2);//8th
        courses[84] = new Course(85, "CIV201", 3);//options
        courses[85] = new Course(86, "CIV216", 3);
        courses[86] = new Course(87, "CIV252", 3);
        courses[87] = new Course(88, "CIV254", 3);
        courses[88] = new Course(89, "CIV273", 3);
        courses[89] = new Course(90, "CIV275", 3);
        courses[90] = new Course(91, "CIV320", 3);
        courses[91] = new Course(92, "CIV322", 3);
        courses[92] = new Course(93, "CIV327", 3);
        courses[93] = new Course(94, "CIV347", 3);
        courses[94] = new Course(95, "CIV341", 3);
        courses[95] = new Course(96, "CIV342", 3);
        courses[96] = new Course(97, "CIV344", 3);
        courses[97] = new Course(98, "CIV345", 3);
        courses[98] = new Course(99, "CIV361", 3);
        courses[99] = new Course(100, "CIV363", 3);
        courses[100] = new Course(101, "CIV365", 3);
        courses[101] = new Course(102, "CIV370", 3);
        courses[102] = new Course(103, "CIV371", 3);
        courses[103] = new Course(104, "CIV378", 3);

        courses[104] = new Course(105, "ISE111", 2);//1st sem design
        courses[105] = new Course(106, "ISE130", 2);//2nd porp intro
        courses[106] = new Course(107, "ISE131", 2);//3rs intro
        courses[107] = new Course(108, "ISE160", 3);//4th
        courses[108] = new Course(109, "ISE161", 1);//4th 160 lab
        courses[109] = new Course(110, "ISE216", 1);//5th\215 lab
        courses[110] = new Course(111, "ISE230", 3);//5th
        courses[111] = new Course(112, "ISE272", 3);//5th
        courses[112] = new Course(113, "ISE215", 3);//5th
        courses[113] = new Course(114, "ISE240", 3);//6th
        courses[114] = new Course(115, "ISE224", 3);//6th
        courses[115] = new Course(116, "ISE305", 3);//6th
        courses[116] = new Course(117, "ISE226", 3);//6th
        courses[117] = new Course(118, "ISE350", 3);//7th
        courses[118] = new Course(119, "ISE345", 3);//8th
        courses[119] = new Course(120, "ISE316", 3);//Options
        courses[120] = new Course(121, "ISE319", 3);
        courses[121] = new Course(122, "ISE320", 3);
        courses[122] = new Course(123, "ISE324", 3);
        courses[123] = new Course(124, "ISE332", 3);
        courses[124] = new Course(125, "ISE334", 3);
        courses[125] = new Course(126, "ISE340", 3);
        courses[126] = new Course(127, "ISE356", 3);
        courses[127] = new Course(128, "ISE364", 3);
        courses[128] = new Course(129, "ISE365", 3);
        courses[129] = new Course(130, "ISE362", 3);
        courses[130] = new Course(131, "ISE372", 3);

        courses[131] = new Course(132, "null", 2);//3rdciv ool
        courses[132] = new Course(133, "CSE109", 4);//3rd 109
        courses[133] = new Course(134, "CSE160", 3);//4rdciv alg
        courses[134] = new Course(135, "CSE206", 4);//4th 
        courses[135] = new Course(136, "CSE240", 3);//4th data
        courses[136] = new Course(137, "CSE340", 3);//5th 
        courses[137] = new Course(138, "CSE290", 4);//5th
        courses[138] = new Course(139, "CSE209", 3);//6th 
        courses[139] = new Course(140, "CSE264", 4);//6th
        courses[140] = new Course(141, "CSE301", 3);//6th
        courses[141] = new Course(142, "CSE302", 3);//7th 
        courses[142] = new Course(143, "CSE309", 4);//7th
        courses[143] = new Course(144, "CSE318", 3);//8th
        courses[144] = new Course(145, "CSE252", 3);//options
        courses[145] = new Course(146, "CSE260", 3);
        courses[146] = new Course(147, "CSE271", 3);
        courses[147] = new Course(148, "CSE302", 3);
        courses[148] = new Course(149, "CSE308", 3);
        courses[149] = new Course(150, "CSE319", 3);
        courses[150] = new Course(151, "CSE326", 3);
        courses[151] = new Course(152, "CSE327", 3);
        courses[152] = new Course(153, "CSE331", 3);
        courses[153] = new Course(154, "CSE343", 3);
        courses[154] = new Course(155, "CSE336", 3);
        courses[155] = new Course(156, "CSE341", 3);
        courses[156] = new Course(157, "CSE347", 3);
        courses[157] = new Course(158, "CSE360", 3);
        courses[158] = new Course(159, "CSE298", 3);
        courses[159] = new Course(160, "CSE363", 3);
        courses[160] = new Course(161, "CSE375", 3);

        courses[161] = new Course(162, "MAT101", 3);//3rd sem
        courses[162] = new Course(163, "MAT102", 2);//3rd sem lab
        courses[163] = new Course(164, "MAT203", 4);//4rd sem
        courses[164] = new Course(165, "MAT204", 3);//4th
        courses[165] = new Course(166, "MAT205", 3);//4th sem
        courses[166] = new Course(167, "MAT218", 3);//4th
        courses[167] = new Course(168, "MAT120", 3);//5th
        courses[168] = new Course(169, "MAT201", 2);//5th 101
        courses[169] = new Course(170, "MAT261", 3);//5th
        courses[170] = new Course(171, "MAT266", 3);//6th
        courses[171] = new Course(172, "MAT214", 3);//6th sem
        courses[172] = new Course(173, "MAT202", 3);//6th 201
        courses[173] = new Course(174, "MAT211", 3);//6th
        courses[174] = new Course(175, "MAT352", 3);//7th
        courses[175] = new Course(176, "MAT312", 3);//7th
        courses[176] = new Course(177, "MAT399", 3);//7th
        courses[177] = new Course(178, "MAT328", 3);//8th
        courses[178] = new Course(179, "CHE203", 1);//CHE202 lecture for lab
        courses[179] = new Course(180, "MAT209", 3);//options
        courses[180] = new Course(181, "MAT314", 3);
        courses[181] = new Course(182, "MAT216", 3);
        courses[182] = new Course(183, "MAT317", 3);
        courses[183] = new Course(184, "MAT220", 3);
        courses[184] = new Course(185, "MAT324", 3);
        courses[185] = new Course(186, "MAT225", 3);
        courses[186] = new Course(187, "MAT226", 3);
        courses[187] = new Course(188, "MAT399", 3);
        courses[188] = new Course(189, "MAT333", 3);
        courses[189] = new Course(190, "MAT242", 3);
        courses[190] = new Course(191, "MAT345", 3);
        courses[191] = new Course(192, "MAT346", 3);
        courses[192] = new Course(193, "MAT255", 3);
        courses[193] = new Course(194, "MAT259", 3);
        courses[194] = new Course(195, "MAT263", 3);
        courses[195] = new Course(196, "MAT386", 3);
        courses[196] = new Course(197, "MAT292", 3);
        courses[197] = new Course(198, "MAT393", 3);

        courses[198] = new Course(199, "MEC170", 2);//3rd sem math
        courses[199] = new Course(200, "MEC144", 3);//4th
        courses[200] = new Course(201, "MEC112", 3);//4th
        courses[201] = new Course(202, "MEC121", 1);//5th
        courses[202] = new Course(203, "MEC231", 3);//5th
        courses[203] = new Course(204, "MEC192", 3);//5th
        courses[204] = new Course(205, "MEC215", 3);//5th
        courses[205] = new Course(206, "MEC113", 1);//6th
        courses[206] = new Course(207, "MEC240", 3);//6th
        courses[207] = new Course(208, "MEC252", 3);//6th
        courses[208] = new Course(209, "MEC242", 3);//6th
        courses[209] = new Course(210, "MEC311", 3);//7th
        courses[210] = new Course(211, "MEC312", 2);//8th
        courses[211] = new Course(212, "MEC321", 3);//8th
        courses[212] = new Course(213, "MEC302", 3);//options
        courses[213] = new Course(214, "MEC305", 3);
        courses[214] = new Course(215, "MEC304", 3);
        courses[215] = new Course(216, "MEC322", 3);
        courses[216] = new Course(217, "MEC331", 3);
        courses[217] = new Course(218, "MEC343", 3);
        courses[218] = new Course(219, "MEC309", 3);
        courses[219] = new Course(220, "MEC312", 3);
        courses[220] = new Course(221, "MEC314", 3);
        courses[221] = new Course(222, "MEC348", 3);
        courses[222] = new Course(223, "MEC341", 3);
        courses[223] = new Course(224, "MEC342", 3);
        courses[224] = new Course(225, "MEC333", 3);
        courses[225] = new Course(226, "MEC340", 3);
        courses[226] = new Course(227, "MEC354", 3);
        courses[227] = new Course(228, "MEC360", 3);
        courses[228] = new Course(229, "MEC364", 3);
        courses[229] = new Course(230, "MEC366", 3);
        courses[230] = new Course(231, "MEC368", 3);
        courses[231] = new Course(232, "MEC373", 3);
        courses[232] = new Course(233, "MEC385", 3);
        courses[233] = new Course(234, "MEC387", 3);

        courses[234] = new Course(235, "ELE101", 2); //1st desing
        courses[235] = new Course(236, "ELE102", 2);//2rd semester survey
        courses[236] = new Course(237, "ELE103", 4);
        courses[237] = new Course(238, "ELE104", 2); //3rd sem
        courses[238] = new Course(239, "ELE123", 3);//4th 
        courses[239] = new Course(240, "ELE121", 2);
        courses[240] = new Course(241, "ELE186", 3);//4th
        courses[241] = new Course(242, "ELE208", 3);//5th 
        courses[242] = new Course(243, "ELE202", 3);//5th
        courses[243] = new Course(244, "ELE180", 1);//5th
        courses[244] = new Course(245, "ELE225", 3);//6th 
        courses[245] = new Course(246, "ELE238", 3);//6th
        courses[246] = new Course(247, "ELE203", 3);//6th
        courses[247] = new Course(248, "ELE180", 1);//Mat 8th semester lab
        courses[248] = new Course(249, "ELE236", 3);//7th 
        courses[249] = new Course(250, "ELE357", 2);//7th
        courses[250] = new Course(251, "ELE358", 2);//8th
        courses[251] = new Course(252, "ELE308", 3);//options
        courses[252] = new Course(253, "ELE332", 3);
        courses[253] = new Course(254, "ELE333", 3);
        courses[254] = new Course(255, "ELE337", 3);
        courses[255] = new Course(256, "ELE355", 3);
        courses[256] = new Course(257, "ELE361", 3);
        courses[257] = new Course(258, "ELE313", 3);
        courses[258] = new Course(259, "ELE339", 3);
        courses[259] = new Course(260, "ELE341", 3);
        courses[260] = new Course(261, "ELE343", 3);
        courses[261] = new Course(262, "ELE387", 3);
        courses[262] = new Course(263, "ELE310", 3);
        courses[263] = new Course(264, "ELE325", 3);
        courses[264] = new Course(265, "ELE347", 3);
        courses[265] = new Course(266, "ELE348", 3);
        courses[266] = new Course(267, "ELE371", 3);
        courses[267] = new Course(268, "ELE372", 3);

        courses[268] = new Course(269, "ENG110", 4);
        courses[269] = new Course(270, "ENG223", 4);//basic lit
        courses[270] = new Course(271, "ENG224", 4);
        courses[271] = new Course(272, "ENG225", 4);
        courses[272] = new Course(273, "ENG226", 4);
        courses[273] = new Course(274, "ENG242", 4);//writing
        courses[274] = new Course(275, "ENG243", 4);
        courses[275] = new Course(276, "ENG244", 4);
        courses[276] = new Course(277, "ENG327", 4);//options
        courses[277] = new Course(278, "ENG328", 4);
        courses[278] = new Course(279, "ENG360", 4);
        courses[279] = new Course(280, "ENG362", 4);
        courses[280] = new Course(281, "ENG364", 4);
        courses[281] = new Course(282, "ENG331", 4);
        courses[282] = new Course(283, "ENG366", 4);
        courses[283] = new Course(284, "ENG367", 4);
        courses[284] = new Course(285, "ENG369", 4);
        courses[285] = new Course(286, "ENG371", 4);
        courses[286] = new Course(287, "ENG372", 4);
        courses[287] = new Course(288, "ENG374", 4);
        courses[288] = new Course(289, "ENG376", 4);
        courses[289] = new Course(290, "ENG377", 4);
        courses[290] = new Course(291, "ENG378", 4);
        courses[291] = new Course(292, "ENG379", 4);
        courses[292] = new Course(293, "ENG380", 4);
        courses[293] = new Course(294, "ENG383", 4);
        courses[294] = new Course(295, "ENG384", 4);
        courses[295] = new Course(296, "ENG385", 4);
        courses[296] = new Course(297, "ENG386", 4);
        courses[297] = new Course(298, "ENG387", 4);
        courses[298] = new Course(299, "ENG328", 4);
        courses[299] = new Course(300, "ENG342", 4);//adv writing
        courses[300] = new Course(301, "ENG343", 4);
        courses[301] = new Course(302, "ENG344", 4);
        electiveList[0] = 269;
        electiveList[1] = 270;
        electiveList[2] = 271;
        electiveList[3] = 272;
        electiveList[18] = 273;

        courses[302] = new Course(303, "HIST101", 4);
        courses[303] = new Course(304, "HIST141", 4);//NA
        courses[304] = new Course(305, "HIST142", 4);
        courses[305] = new Course(306, "HIST143", 4);// US hist 3
        courses[306] = new Course(307, "HIST245", 4);
        courses[307] = new Course(308, "HIST240", 4);
        courses[308] = new Course(309, "HIST247", 4);
        courses[309] = new Course(310, "HIST250", 4);
        courses[310] = new Course(311, "HIST244", 4);
        courses[311] = new Course(312, "HIST245", 4);
        courses[312] = new Course(313, "HIST246", 4);
        courses[313] = new Course(314, "HIST253", 4);
        courses[314] = new Course(315, "HIST255", 4);
        courses[315] = new Course(316, "HIST256", 4);
        courses[316] = new Course(317, "HIST259", 4);
        courses[317] = new Course(318, "HIST258", 4);
        courses[318] = new Course(319, "HIST348", 4);
        courses[319] = new Course(320, "HIST345", 4);
        courses[320] = new Course(321, "HIST349", 4);
        courses[321] = new Course(322, "HIST340", 4);
        courses[322] = new Course(323, "HIST343", 4);
        courses[323] = new Course(324, "HIST355", 4);
        courses[324] = new Course(325, "HIST358", 4);
        courses[325] = new Course(326, "HIST352", 4);
        courses[326] = new Course(327, "HIST356", 4);
        courses[327] = new Course(328, "HIST148", 4);
        courses[328] = new Course(329, "HIST350", 4);
        courses[329] = new Course(330, "HIST357", 4);
        courses[330] = new Course(331, "HIST111", 4);//EU 
        courses[331] = new Course(332, "HIST112", 4);
        courses[332] = new Course(333, "HIST115", 4);
        courses[333] = new Course(334, "HIST121", 4);//EU 
        courses[334] = new Course(335, "HIST122", 4);
        courses[335] = new Course(336, "HIST215", 4);
        courses[336] = new Course(337, "HIST214", 4);//EU 
        courses[337] = new Course(338, "HIST222", 4);
        courses[338] = new Course(339, "HIST223", 4);
        courses[339] = new Course(340, "HIST228", 4);//EU 
        courses[340] = new Course(341, "HIST225", 4);
        courses[341] = new Course(342, "HIST312", 4);
        courses[342] = new Course(343, "HIST314", 4);//EU 
        courses[343] = new Course(344, "HIST317", 4);
        courses[344] = new Course(345, "HIST318", 4);
        courses[345] = new Course(346, "HIST320", 4);//EU 
        courses[346] = new Course(347, "HIST324", 4);
        courses[347] = new Course(348, "HIST139", 4);//Latin
        courses[348] = new Course(349, "HIST137", 4);//inca/mayan/aztec
        courses[349] = new Course(350, "HIST130", 4);
        courses[350] = new Course(351, "HIST239", 4);
        courses[351] = new Course(352, "HIST235", 4);//Afro-latin
        courses[352] = new Course(353, "HIST338", 4);
        courses[353] = new Course(354, "HIST339", 4);//us latin relations
        courses[354] = new Course(355, "HIST165", 4);//Africa
        courses[355] = new Course(356, "HIST267", 4);
        courses[356] = new Course(357, "HIST260", 4);
        courses[357] = new Course(358, "HIST264", 4);
        courses[358] = new Course(359, "HIST269", 4);
        courses[359] = new Course(360, "HIST360", 4);
        courses[360] = new Course(361, "HIST361", 4);
        courses[361] = new Course(362, "HIST365", 4);
        courses[362] = new Course(363, "HIST161", 4);
        courses[363] = new Course(364, "HIST171", 4);//Asia
        courses[364] = new Course(365, "HIST175", 4);
        courses[365] = new Course(366, "HIST176", 4);
        courses[366] = new Course(367, "HIST179", 4);//assion imp
        courses[367] = new Course(368, "HIST172", 4);//asian political tension
        courses[368] = new Course(369, "HIST270", 4);
        courses[369] = new Course(370, "HIST279", 4);//india
        courses[370] = new Course(371, "HIST270", 4);//japan indust
        courses[371] = new Course(372, "HIST177", 4);
        courses[372] = new Course(373, "HIST275", 4);
        courses[373] = new Course(374, "HIST288", 4);//trans
        courses[374] = new Course(375, "HIST281", 4);
        courses[375] = new Course(376, "HIST187", 4);
        courses[376] = new Course(377, "HIST182", 4);
        courses[377] = new Course(378, "HIST289", 4);
        courses[378] = new Course(379, "HIST382", 4);
        courses[379] = new Course(380, "HIST384", 4);
        courses[380] = new Course(381, "HIST380", 4);//
        courses[381] = new Course(382, "HIST391", 4);//
        courses[382] = new Course(383, "HIST381", 4);
        courses[383] = new Course(384, "HIST388", 4);//
        courses[384] = new Course(385, "HIST392", 4);
        courses[385] = new Course(386, "HIST394", 4);
        electiveList[4] = 303;
        electiveList[5] = 304;
        electiveList[6] = 305;
        electiveList[7] = 330;
        electiveList[8]= 331;
        electiveList[9] = 332;
        electiveList[10] = 333;
        electiveList[11] = 334;
        electiveList[12] = 347;
        electiveList[13] = 348;
        electiveList[14] = 363;
        electiveList[15] = 367;
        electiveList[16] = 369;
        electiveList[17] = 370;

        courses[386] = new Course(387, "GER101", 4);
        courses[387] = new Course(388, "GER102", 4);

        courses[388] = new Course(389, "FRA101", 4);
        courses[389] = new Course(390, "FRA102", 4);

        courses[390] = new Course(391, "SPA101", 4);
        courses[391] = new Course(392, "SPA102", 4);

        courses[392] = new Course(393, "JPN101", 4);
        courses[393] = new Course(394, "JPN102", 4);

        courses[394] = new Course(395, "RUS101", 4);
        courses[395] = new Course(396, "RUS102", 4);

        courses[396] = new Course(397, "CHI101", 4);
        courses[397] = new Course(398, "CHI102", 4);

        courses[398] = new Course(399, "CHM180", 3);//4th semseter organic che
        courses[399] = new Course(400, "CHM181", 1);
        courses[400] = new Course(401, "CHM190", 3);//5th smesster org2 che
        courses[401] = new Course(402, "CHM191", 1);
        courses[402] = new Course(403, "CHE386", 3);
        courses[403] = new Course(404, "CHE387", 3);

        courses[404] = new Course(405, "PHY131", 3);
        courses[405] = new Course(406, "PHY129", 2);//intro addition
        courses[406] = new Course(407, "PHY212", 3);
        courses[407] = new Course(408, "PHY215", 4);
        courses[408] = new Course(409, "PHY220", 3);
        courses[409] = new Course(410, "PHY213", 3);
        courses[410] = new Course(411, "PHY262", 3);
        courses[411] = new Course(412, "PHY221", 2);
        courses[412] = new Course(413, "PHY240", 3);
        courses[413] = new Course(414, "PHY273", 3);
        courses[414] = new Course(415, "PHY364", 3);
        courses[415] = new Course(416, "PHY342", 3);
        courses[416] = new Course(417, "PHY369", 3);

        courses[417] = new Course(418, "PHY363", 3);//options
        courses[418] = new Course(419, "PHY352", 3);
        courses[419] = new Course(420, "PHY355", 3);
        courses[420] = new Course(421, "PHY348", 3);
        courses[421] = new Course(422, "PHY365", 3);
        courses[422] = new Course(423, "PHY380", 3);

        courses[423] = new Course(424, "CHM332", 3);
        courses[424] = new Course(425, "CHM307", 3);
        courses[425] = new Course(426, "CHM351", 3);
        courses[426] = new Course(427, "CHM334", 3);
        courses[427] = new Course(428, "CHM335", 3);
        courses[428] = new Course(429, "CHM341", 3);
        courses[429] = new Course(430, "CHM342", 3);
        courses[430] = new Course(431, "CHM343", 3);
        courses[431] = new Course(432, "CHM371", 3);
        courses[432] = new Course(433, "CHM375", 3);//null

        courses[433] = new Course(434, "CHM305", 3);//options
        courses[434] = new Course(435, "CHM323", 3);
        courses[435] = new Course(436, "CHM336", 3);
        courses[436] = new Course(437, "CHM337", 3);
        courses[437] = new Course(438, "CHM340", 3);
        courses[438] = new Course(439, "CHM350", 3);
        courses[439] = new Course(440, "CHM356", 3);
        courses[440] = new Course(441, "CHM357", 3);
        courses[441] = new Course(442, "CHM358", 3);
        courses[442] = new Course(443, "CHM362", 3);
        courses[443] = new Course(444, "CHM365", 3);
        courses[444] = new Course(445, "CHM372", 3);
        courses[445] = new Course(446, "CHM373", 3);
        courses[446] = new Course(447, "CHM376", 3);
        courses[447] = new Course(448, "CHM377", 3);
        courses[448] = new Course(449, "CHM392", 3);
        courses[449] = new Course(450, "CHM391", 3);
        courses[450] = new Course(451, "CHM393", 3);
        courses[451] = new Course(452, "CHM394", 3);

        courses[452] = new Course(453, "MEC245", 3);
        courses[453] = new Course(454, "MEC207", 3);

        courses[454] = new Course(455, "HIST116", 4);
        courses[455] = new Course(456, "HIST123", 4);
        courses[456] = new Course(457, "HIST253", 4);

        courses[457] = new Course(458, "MATH181", 3);//core bis
        courses[458] = new Course(459, "HOLD100", 3);
        courses[459] = new Course(460, "BIS101", 1);
        courses[460] = new Course(461, "BIS103", 2);
        courses[461] = new Course(462, "BIS144", 2);
        courses[462] = new Course(463, "ECO145", 3);
        courses[463] = new Course(464, "MAN143", 3);
        courses[464] = new Course(465, "ACT151", 3);
        courses[465] = new Course(466, "ACT152", 3);
        courses[466] = new Course(467, "BIS111", 3);
        courses[467] = new Course(468, "MKT111", 3);
        courses[468] = new Course(469, "MAN186", 3);
        courses[469] = new Course(470, "FIN125", 3);
        courses[470] = new Course(471, "ECO146", 3);
        courses[471] = new Course(472, "ECO147", 3);
        courses[472] = new Course(473, "LAW201", 3);
        courses[473] = new Course(474, "LAW202", 3);
        courses[474] = new Course(475, "BIS203", 3);
        courses[475] = new Course(476, "BIS204", 3);
        courses[476] = new Course(477, "MAN243", 3);
        courses[477] = new Course(478, "MAN301", 3);
        
        courses[478] = new Course(479, "ECO157", 3);
        //options
        courses[479] = new Course(480, "ECO209", 3);
        courses[480] = new Course(481, "ECO211", 3);
        courses[481] = new Course(482, "ECO229", 3);
        courses[482] = new Course(483, "ECO235", 3);
        courses[483] = new Course(484, "ECO203", 3);
        courses[484] = new Course(485, "ECO247", 3);
        courses[485] = new Course(486, "ECO224", 3); 
        courses[486] = new Course(487, "ECO273", 3);
        courses[487] = new Course(488, "ECO214", 3);
        courses[488] = new Course(489, "ECO228", 3);
        courses[489] = new Course(490, "ECO268", 3);

        courses[490] = new Course(491, "ECO312", 3);
        courses[491] = new Course(492, "ECO322", 3);
        courses[492] = new Course(493, "ECO338", 3);
        courses[493] = new Course(494, "ECO353", 3);
        courses[494] = new Course(495, "ECO358", 3);
        courses[495] = new Course(496, "ECO365", 3);
        courses[496] = new Course(497, "ECO301", 3);
        courses[497] = new Course(498, "ECO325", 3);
        courses[498] = new Course(499, "ECO333", 3);
        courses[499] = new Course(500, "ECO335", 3);
        courses[500] = new Course(501, "ECO336", 3);
        courses[501] = new Course(502, "ECO342", 3);
        courses[502] = new Course(503, "ECO345", 3);
        courses[503] = new Course(504, "ECO357", 3);
        courses[504] = new Course(505, "ECO360", 3);
        courses[505] = new Course(506, "ECO366", 3);
        courses[506] = new Course(507, "ECO367", 3);
;
        courses[508] = new Course(509, "BIO102", 1);
        courses[509] = new Course(510, "BIO103", 1);
        courses[507] = new Course(508, "PHY111", 1);

        courses[510] = new Course(511, "MATH163", 3);//core
        courses[511] = new Course(512, "MATH242", 3);
        courses[512] = new Course(513, "MATH243", 1);
        courses[513] = new Course(514, "MATH244", 3);
        courses[514] = new Course(515, "MATH252", 3);
        courses[515] = new Course(516, "MATH301", 3);
        courses[516] = new Course(517, "MATH192", 2);//core states
        courses[517] = new Course(518, "MATH264", 3);
        courses[518] = new Course(519, "MATH310", 3);
        courses[519] = new Course(520, "MATH333", 3);

        courses[520] = new Course(521, "MATH208", 3);//options
        courses[521] = new Course(522, "MATH229", 3);
        courses[522] = new Course(523, "MATH230", 3);
        courses[523] = new Course(524, "MATH234", 3);
        courses[524] = new Course(525, "MATH261", 3);
        courses[525] = new Course(526, "MATH203", 3);
        courses[526] = new Course(527, "MATH219", 3);

        courses[527] = new Course(528, "MATH302", 3);
        courses[528] = new Course(529, "MATH305", 3);
        courses[529] = new Course(530, "MATH307", 3);
        courses[530] = new Course(531, "MATH309", 3);
        courses[531] = new Course(532, "MATH311", 3);
        courses[532] = new Course(533, "MATH312", 3);
        courses[533] = new Course(534, "MATH316", 3);
        courses[534] = new Course(535, "MATH320", 3);
        courses[535] = new Course(536, "MATH322", 3);
        courses[536] = new Course(537, "MATH327", 3);
        courses[537] = new Course(538, "MATH331", 3);
        courses[538] = new Course(539, "MATH340", 3);
        courses[539] = new Course(540, "MATH341", 3);
        courses[540] = new Course(541, "MATH342", 3);
        courses[541] = new Course(542, "MATH343", 3);
        courses[542] = new Course(543, "MATH180", 3);
        courses[543] = new Course(544, "MATH194", 3);

        courses[543] = new Course(544, "MKT311", 3);//marketing
        courses[544] = new Course(545, "MKT312", 3);
        courses[545] = new Course(546, "MKT387", 3);

        courses[546] = new Course(547, "MKT313", 3);//options
        courses[547] = new Course(548, "MKT314", 3);
        courses[548] = new Course(549, "MKT319", 3);
        courses[549] = new Course(550, "MKT320", 3);
        courses[550] = new Course(551, "MKT325", 3);
        courses[551] = new Course(552, "MKT326", 3);
        courses[552] = new Course(553, "MKT327", 3);
        courses[553] = new Course(554, "MKT347", 3);
        courses[554] = new Course(555, "MKT330", 3);
        courses[555] = new Course(556, "MKT332", 3);
        courses[556] = new Course(557, "MKT366", 3);
        courses[557] = new Course(558, "MKT371", 3);
        courses[558] = new Course(559, "MKT372", 3);

        courses[559] = new Course(560, "ACT315", 3);//accounting
        courses[560] = new Course(561, "ACT316", 3);
        courses[561] = new Course(562, "ACT311", 3);
        courses[562] = new Course(563, "ACT324", 3);

        courses[563] = new Course(564, "ACT307", 3);//options
        courses[564] = new Course(565, "ACT320", 3);
        courses[565] = new Course(566, "ACT317", 3);
        courses[566] = new Course(567, "ACT318", 3);
        courses[567] = new Course(568, "ACT330", 3);

        courses[568] = new Course(569, "FIN323", 3);//finacne
        courses[569] = new Course(570, "FIN328", 3);

        courses[570] = new Course(571, "FIN324", 3);
        courses[571] = new Course(572, "FIN330", 3);
        courses[572] = new Course(573, "FIN333", 3);
        courses[573] = new Course(574, "FIN334", 3);
        courses[574] = new Course(575, "FIN335", 3);
        courses[575] = new Course(576, "FIN336", 3);
        courses[576] = new Course(577, "FIN377", 3);

        
        courses[577] = new Course(578, "CSE401", 3);
        courses[578] = new Course(579, "CSE403", 3);
        courses[579] = new Course(580, "CSE404", 3);
        courses[580] = new Course(581, "CSE405", 3);
        courses[581] = new Course(582, "CSE406", 3);
        courses[582] = new Course(583, "CSE407", 3);
        courses[583] = new Course(584, "CSE409", 3);
        courses[584] = new Course(585, "CSE411", 3);
        courses[585] = new Course(586, "CSE419", 3);
        courses[586] = new Course(587, "CSE425", 3);
        courses[587] = new Course(588, "CSE426", 3);
        courses[588] = new Course(589, "CSE428", 3);
        courses[589] = new Course(590, "CSE431", 3);
        courses[590] = new Course(591, "CSE434", 3);
        courses[591] = new Course(592, "CSE440", 3);
        courses[592] = new Course(593, "CSE443", 3);
        courses[593] = new Course(594, "CSE445", 3);
        courses[594] = new Course(595, "CSE447", 3);
        courses[595] = new Course(596, "CSE460", 3);
        courses[596] = new Course(597, "CSE471", 3);
        courses[597] = new Course(598, "CSE475", 3);

        courses[616] = new Course(617, "CHE400", 3);
        courses[617] = new Course(618, "CHE410", 3);
        courses[618] = new Course(619, "CHE415", 3);
        courses[619] = new Course(620, "CHE452", 3);
        
        courses[598] = new Course(599, "CHE401", 3);
        courses[599] = new Course(600, "CHE413", 3);
        courses[600] = new Course(601, "CHE415", 3);
        courses[601] = new Course(602, "CHE421", 3);
        courses[602] = new Course(603, "CHE428", 3);
        courses[603] = new Course(604, "CHE430", 3);
        courses[604] = new Course(605, "CHE433", 3);
        courses[605] = new Course(606, "CHE437", 3);
        courses[606] = new Course(607, "CHE440", 3);
        courses[607] = new Course(608, "CHE441", 3);
        courses[608] = new Course(609, "CHE447", 3);
        courses[609] = new Course(610, "CHE449", 3);
        courses[610] = new Course(611, "CHE463", 3);
        courses[611] = new Course(612, "CHE465", 3);
        courses[612] = new Course(613, "CHE473", 3);
        courses[613] = new Course(614, "CHE482", 3);
        courses[614] = new Course(615, "CHE485", 3);
        courses[615] = new Course(616, "CHE486", 3);

        courses[620] = new Course(621, "MEC401", 3);
        courses[621] = new Course(622, "MEC402", 3);
        courses[622] = new Course(623, "MEC411", 3);
        courses[623] = new Course(624, "MEC413", 3);
        courses[624] = new Course(625, "MEC415", 3);
        courses[625] = new Course(626, "MEC420", 3);
        courses[626] = new Course(627, "MEC423", 3);
        courses[627] = new Course(628, "MEC424", 3);
        courses[628] = new Course(629, "MEC426", 3);
        courses[629] = new Course(630, "MEC430", 3);
        courses[630] = new Course(631, "MEC431", 3);
        courses[631] = new Course(632, "MEC433", 3);
        courses[632] = new Course(633, "MEC437", 3);
        courses[633] = new Course(634, "MEC444", 3);
        courses[634] = new Course(635, "MEC446", 3);
        courses[635] = new Course(636, "MEC452", 3);
        courses[636] = new Course(637, "MEC454", 3);
        courses[637] = new Course(638, "MEC458", 3);
        courses[638] = new Course(639, "MEC464", 3);
        courses[639] = new Course(640, "MEC466", 3);
        courses[640] = new Course(641, "MEC468", 3);
        courses[641] = new Course(642, "MEC485", 3);

        courses[642] = new Course(643, "ELE402", 3);
        courses[643] = new Course(644, "ELE411", 3);
        courses[644] = new Course(645, "ELE413", 3);
        courses[645] = new Course(646, "ELE416", 3);
        courses[646] = new Course(647, "ELE420", 3);
        courses[647] = new Course(648, "ELE421", 3);
        courses[648] = new Course(649, "ELE422", 3);
        courses[649] = new Course(650, "ELE425", 3);
        courses[650] = new Course(651, "ELE432", 3);
        courses[651] = new Course(652, "ELE433", 3);
        courses[652] = new Course(653, "ELE434", 3);
        courses[653] = new Course(654, "ELE435", 3);
        courses[654] = new Course(655, "ELE437", 3);
        courses[655] = new Course(656, "ELE438", 3);
        courses[656] = new Course(657, "ELE441", 3);
        courses[657] = new Course(658, "ELE443", 3);
        courses[658] = new Course(659, "ELE448", 3);
        courses[659] = new Course(660, "ELE451", 3);
        courses[660] = new Course(661, "ELE455", 3);
        courses[661] = new Course(662, "ELE463", 3);
        courses[662] = new Course(663, "ELE468", 3);
        courses[663] = new Course(664, "ELE471", 3);
        courses[664] = new Course(665, "ELE472", 3);
        courses[665] = new Course(666, "ELE485", 3);
        courses[666] = new Course(667, "ELE493", 3);
        
        courses[667] = new Course(668, "CIV401", 3);
        courses[668] = new Course(669, "CIV404", 3);
        courses[669] = new Course(670, "CIV405", 3);
        courses[670] = new Course(671, "CIV406", 3);
        courses[671] = new Course(672, "CIV409", 3);
        courses[672] = new Course(673, "CIV412", 3);
        courses[673] = new Course(674, "CIV414", 3);
        courses[674] = new Course(675, "CIV415", 3);
        courses[675] = new Course(676, "CIV420", 3);
        courses[676] = new Course(677, "CIV424", 3);
        courses[677] = new Course(678, "CIV425", 3);
        courses[678] = new Course(679, "CIV427", 3);
        courses[679] = new Course(680, "CIV431", 3);
        courses[680] = new Course(681, "CIV432", 3);
        courses[681] = new Course(682, "CIV433", 3);
        courses[682] = new Course(683, "CIV441", 3);
        courses[683] = new Course(684, "CIV443", 3);
        courses[684] = new Course(685, "CIV445", 3);
        courses[685] = new Course(686, "CIV448", 3);
        courses[686] = new Course(687, "CIV452", 3);
        courses[687] = new Course(688, "CIV453", 3);
        courses[688] = new Course(689, "CIV454", 3);
        courses[689] = new Course(690, "CIV455", 3);
        courses[690] = new Course(691, "CIV456", 3);
        courses[691] = new Course(692, "CIV457", 3);
        courses[692] = new Course(693, "CIV458", 3);
        courses[693] = new Course(694, "CIV459", 3);
        courses[694] = new Course(695, "CIV461", 3);
        courses[695] = new Course(696, "CIV462", 3);
        courses[696] = new Course(697, "CIV463", 3);
        courses[697] = new Course(698, "CIV464", 3);
        courses[698] = new Course(699, "CIV465", 3);
        courses[699] = new Course(700, "CIV466", 3);
        courses[700] = new Course(701, "CIV468", 3);
        courses[701] = new Course(702, "CIV470", 3);
        courses[702] = new Course(703, "CIV471", 3);
        courses[703] = new Course(704, "CIV472", 3);
        courses[704] = new Course(705, "CIV473", 3);
        courses[705] = new Course(706, "CIV476", 3);
        courses[706] = new Course(707, "CIV477", 3);
        courses[707] = new Course(708, "CIV478", 3);
        
        courses[708] = new Course(709, "ISE401", 3);
        courses[709] = new Course(710, "ISE402", 3);
        courses[710] = new Course(711, "ISE404", 3);
        courses[711] = new Course(712, "ISE406", 3);
        courses[712] = new Course(713, "ISE407", 3);
        courses[713] = new Course(714, "ISE409", 3);
        courses[714] = new Course(715, "ISE410", 3);
        courses[715] = new Course(716, "ISE411", 3);
        courses[716] = new Course(717, "ISE412", 3);
        courses[717] = new Course(718, "ISE413", 3);
        courses[718] = new Course(719, "ISE416", 3);
        courses[719] = new Course(720, "ISE417", 3);
        courses[720] = new Course(721, "ISE418", 3);
        courses[721] = new Course(722, "ISE419", 3);
        courses[722] = new Course(723, "ISE420", 3);
        courses[723] = new Course(724, "ISE424", 3);
        courses[724] = new Course(725, "ISE425", 3);
        courses[725] = new Course(726, "ISE426", 3);
        courses[726] = new Course(727, "ISE429", 3);
        courses[727] = new Course(728, "ISE437", 3);
        courses[728] = new Course(729, "ISE438", 3);
        courses[729] = new Course(730, "ISE439", 3);
        courses[730] = new Course(731, "ISE441", 3);
        courses[731] = new Course(732, "ISE442", 3);
        courses[732] = new Course(733, "ISE443", 3);
        courses[733] = new Course(734, "ISE445", 3);
        courses[734] = new Course(735, "ISE447", 3);
        courses[735] = new Course(736, "ISE455", 3);
        courses[736] = new Course(737, "ISE456", 3);
        courses[737] = new Course(738, "ISE465", 3);
        courses[738] = new Course(739, "ISE467", 3);
        courses[739] = new Course(740, "ISE470", 3);
        courses[740] = new Course(741, "ISE471", 3);
        courses[741] = new Course(742, "ISE472", 3);
        courses[742] = new Course(743, "ISE473", 3);

        courses[743] = new Course(744, "MAT401", 3);
        courses[744] = new Course(745, "MAT402", 3);
        courses[745] = new Course(746, "MAT403", 3);
        courses[746] = new Course(747, "MAT406", 3);
        courses[747] = new Course(748, "MAT414", 3);
        courses[748] = new Course(749, "MAT415", 3);
        courses[749] = new Course(750, "MAT416", 3);
        courses[750] = new Course(751, "MAT423", 3);
        courses[751] = new Course(752, "MAT424", 3);
        courses[752] = new Course(753, "MAT425", 3);
        courses[753] = new Course(754, "MAT426", 3);
        courses[754] = new Course(755, "MAT427", 3);
        courses[755] = new Course(756, "MAT430", 3);
        courses[756] = new Course(757, "MAT431", 3);
        courses[757] = new Course(758, "MAT442", 3);
        courses[758] = new Course(759, "MAT443", 3);
        courses[759] = new Course(760, "MAT445", 3);
        courses[760] = new Course(761, "MAT455", 3);
        courses[761] = new Course(762, "MAT456", 3);
        courses[762] = new Course(763, "MAT459", 3);
        courses[763] = new Course(764, "MAT463", 3);
        courses[764] = new Course(765, "MAT482", 3);
        courses[765] = new Course(766, "MAT483", 3);
        courses[766] = new Course(767, "MAT485", 3);
        courses[767] = new Course(768, "MAT486", 3);
        courses[768] = new Course(769, "MAT487", 3);

        courses[769] = new Course(770, "PHY421", 3);
        courses[770] = new Course(771, "PHY424", 3);
        courses[771] = new Course(772, "PHY425", 3);
        courses[772] = new Course(773, "PHY428", 3);
        courses[773] = new Course(774, "PHY442", 3);

        
        courses[774] = new Course(775, "PHY420", 3);
        courses[775] = new Course(776, "PHY422", 3);
        courses[776] = new Course(777, "PHY429", 3);
        courses[777] = new Course(778, "PHY431", 3);
        courses[778] = new Course(779, "PHY443", 3);
        courses[779] = new Course(780, "PHY446", 3);
        courses[780] = new Course(781, "PHY455", 3);
        courses[781] = new Course(782, "PHY462", 3);
        courses[782] = new Course(783, "PHY475", 3);
        courses[783] = new Course(784, "PHY482", 3);
        courses[784] = new Course(785, "PHY487", 3);
        courses[785] = new Course(786, "PHY489", 3);
        courses[786] = new Course(787, "PHY492", 3);

        
        courses[787] = new Course(788, "CHM405", 3);
        courses[788] = new Course(789, "CHM407", 3);
        courses[789] = new Course(790, "CHM423", 3);
        courses[790] = new Course(791, "CHM424", 3);
        courses[791] = new Course(792, "CHM425", 3);
        courses[792] = new Course(793, "CHM426", 3);
        courses[793] = new Course(794, "CHM427", 3);
        courses[794] = new Course(795, "CHM430", 3);
        courses[795] = new Course(796, "CHM432", 3);
        courses[796] = new Course(797, "CHM434", 3);
        courses[797] = new Course(798, "CHM437", 3);
        courses[798] = new Course(799, "CHM438", 3);
        courses[799] = new Course(800, "CHM443", 3);
        courses[800] = new Course(801, "CHM444", 3);
        courses[801] = new Course(802, "CHM453", 3);
        courses[802] = new Course(803, "CHM455", 3);
        courses[803] = new Course(804, "CHM456", 3);
        courses[804] = new Course(805, "CHM457", 3);
        courses[805] = new Course(806, "CHM462", 3);
        courses[806] = new Course(807, "CHM465", 3);
        courses[807] = new Course(808, "CHM467", 3);
        courses[808] = new Course(809, "CHM468", 3);
        courses[809] = new Course(810, "CHM472", 3);
        courses[810] = new Course(811, "CHM479", 3);
        courses[811] = new Course(812, "CHM482", 3);
        courses[812] = new Course(813, "CHM483", 3);
        courses[813] = new Course(814, "CHM485", 3);
        courses[814] = new Course(815, "CHM487", 3);
        courses[815] = new Course(816, "CHM489", 3);
        courses[816] = new Course(817, "CHM494", 3);

        courses[817] = new Course(818, "CHM452", 3);
        courses[818] = new Course(819, "CHM473", 3);
        
        courses[819] = new Course(820, "MATH401", 3);
        courses[820] = new Course(821, "MATH405", 3);
        courses[821] = new Course(822, "MATH408", 3);
        courses[822] = new Course(823, "MATH423", 3);

        courses[823] = new Course(824, "MATH402", 3);
        courses[824] = new Course(825, "MATH404", 3);
        courses[825] = new Course(826, "MATH406", 3);
        courses[826] = new Course(827, "MATH409", 3);
        courses[827] = new Course(828, "MATH421", 3);
        courses[828] = new Course(829, "MATH424", 3);
        courses[829] = new Course(830, "MATH428", 3);
        courses[830] = new Course(831, "MATH430", 3);
        courses[831] = new Course(832, "MATH435", 3);
        courses[832] = new Course(833, "MATH441", 3);
        courses[833] = new Course(834, "MATH444", 3);
        courses[834] = new Course(835, "MATH455", 3);
        courses[835] = new Course(836, "MATH462", 3);
        courses[836] = new Course(837, "MATH463", 3);
        courses[837] = new Course(838, "MATH464", 3);
        courses[838] = new Course(839, "MATH467", 3);
        courses[839] = new Course(840, "MATH468", 3);
        courses[840] = new Course(841, "MATH470", 3);
        courses[841] = new Course(842, "MATH416", 3);

        courses[842] = new Course(843, "ENG400", 3);
        courses[843] = new Course(844, "ENG490", 3);
        
        courses[844] = new Course(845, "ENG433", 3);
        courses[845] = new Course(846, "ENG439", 3);
        courses[846] = new Course(847, "ENG441", 3);
        courses[847] = new Course(848, "ENG442", 3);
        courses[848] = new Course(849, "ENG443", 3);
        courses[849] = new Course(850, "ENG445", 3);
        courses[850] = new Course(851, "ENG447", 3);
        courses[851] = new Course(852, "ENG449", 3);
        courses[852] = new Course(853, "ENG451", 3);
        courses[853] = new Course(854, "ENG471", 3);
        courses[854] = new Course(855, "ENG473", 3);
        courses[855] = new Course(856, "ENG477", 3);
        courses[856] = new Course(857, "ENG478", 3);
        courses[857] = new Course(858, "ENG479", 3);
        courses[858] = new Course(859, "ENG480", 3);
        courses[859] = new Course(860, "ENG481", 3);
        courses[860] = new Course(861, "ENG482", 3);
        courses[861] = new Course(862, "ENG483", 3);
        
        courses[862] = new Course(863, "HIST404", 3);
        courses[863] = new Course(864, "HIST405", 3);
        courses[864] = new Course(865, "HIST412", 3);
        courses[865] = new Course(866, "HIST421", 3);
        courses[866] = new Course(867, "HIST426", 3);
        courses[867] = new Course(869, "HIST440", 3);
        courses[868] = new Course(870, "HIST441", 3);
        courses[869] = new Course(871, "HIST442", 3);
        courses[870] = new Course(872, "HIST443", 3);
        courses[871] = new Course(873, "HIST444", 3);
        courses[872] = new Course(874, "HIST445", 3);
        courses[873] = new Course(875, "HIST446", 3);
        courses[874] = new Course(876, "HIST447", 3);
        courses[875] = new Course(877, "HIST458", 3);

        
        courses[876] = new Course(877, "HIST438", 3);
        courses[877] = new Course(878, "HIST401", 3);//research
        courses[878] = new Course(879, "HIST200", 3);

        //mba
        courses[879] = new Course(880, "BIS451", 2);
        courses[880] = new Course(881, "BIS452", 2);
        courses[881] = new Course(882, "BIS453", 2);
        courses[882] = new Course(883, "BIS454", 2);
        courses[883] = new Course(884, "BIS455", 2);
        courses[884] = new Course(885, "BIS409", 3);

        courses[885] = new Course(886, "BIS456", 2);
        courses[886] = new Course(887, "BIS461", 2);
        courses[887] = new Course(888, "BIS462", 2);
        courses[888] = new Course(889, "BIS463", 2);
        courses[889] = new Course(890, "BIS464", 2);
        courses[890] = new Course(891, "BIS441", 1);

        courses[891] = new Course(892, "BIS440", 2);
        courses[892] = new Course(893, "BIS488", 3);

        courses[893] = new Course(894, "BIS401", 3);
        courses[894] = new Course(895, "BIS408", 3);
        courses[895] = new Course(896, "BIS409", 3);
        courses[896] = new Course(897, "BIS413", 3);
        courses[897] = new Course(898, "BIS414", 3);
        courses[898] = new Course(899, "BIS419", 3);
        courses[899] = new Course(900, "BIS420", 3);
        courses[900] = new Course(901, "BIS422", 3);
        courses[901] = new Course(902, "BIS424", 3);
        courses[902] = new Course(903, "BIS425", 3);
        courses[903] = new Course(904, "BIS431", 3);
        courses[904] = new Course(905, "BIS432", 3);
        courses[905] = new Course(906, "BIS437", 3);
        courses[906] = new Course(907, "BIS442", 3);
        courses[907] = new Course(908, "BIS447", 3);
        courses[908] = new Course(909, "BIS448", 3);
        courses[909] = new Course(910, "BIS450", 3);
        courses[910] = new Course(911, "BIS453", 3);
        courses[911] = new Course(912, "BIS460", 3);
        courses[912] = new Course(913, "BIS462", 3);
        courses[913] = new Course(914, "BIS464", 3);
        courses[914] = new Course(915, "BIS465", 3);
        courses[915] = new Course(916, "BIS467", 3);
        courses[916] = new Course(917, "BIS471", 3);
        courses[917] = new Course(918, "BIS472", 3);
        courses[918] = new Course(919, "BIS473", 3);
        courses[919] = new Course(920, "BIS475", 3);

        
        courses[920] = new Course(921, "ECO401", 3);
        courses[921] = new Course(922, "ECO403", 3);
        courses[922] = new Course(923, "ECO413", 3);
        courses[923] = new Course(924, "ECO415", 3);
        courses[924] = new Course(925, "ECO440", 3);
        courses[925] = new Course(926, "ECO417", 3);
        courses[926] = new Course(927, "ECO464", 3);
        courses[927] = new Course(928, "ECO402", 3);

        courses[928] = new Course(929, "ECO404", 3);
        courses[929] = new Course(930, "ECO409", 3);
        courses[930] = new Course(931, "ECO412", 3);
        courses[931] = new Course(932, "ECO416", 3);
        courses[932] = new Course(933, "ECO413", 3);
        courses[933] = new Course(934, "ECO423", 3);
        courses[934] = new Course(935, "ECO425", 3);
        courses[935] = new Course(936, "ECO427", 3);
        courses[936] = new Course(937, "ECO428", 3);
        courses[937] = new Course(938, "ECO429", 3);
        courses[938] = new Course(939, "ECO430", 3);
        courses[939] = new Course(940, "ECO431", 3);
        courses[940] = new Course(941, "ECO441", 3);
        courses[941] = new Course(942, "ECO447", 3);
        courses[942] = new Course(943, "ECO448", 3);
        courses[943] = new Course(944, "ECO454", 3);
        courses[944] = new Course(945, "ECO455", 3);
        courses[945] = new Course(946, "ECO456", 3);
        courses[946] = new Course(947, "ECO457", 3);
        courses[947] = new Course(948, "ECO460", 3);
        courses[948] = new Course(949, "ECO461", 3);
        courses[949] = new Course(950, "ECO465", 3);
        courses[950] = new Course(951, "ECO472", 3);
        courses[951] = new Course(952, "ECO473", 3);
        courses[952] = new Course(953, "ECO463", 3);
        }
}