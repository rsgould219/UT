using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class profListWriter : MonoBehaviour{
    public static profListWriter Instance;

    void Start(){
        Instance = this;
    }

    public List<int> sortCourses(List<int> li, int major){
    //add intro cse
    Debug.Log("Prof Major: " + major);
        switch(major){
            case 1:
                li = addCSE(li);
                break;
            case 2:
                li = addMEC(li);
                break;
            case 3:
                li = addCHE(li);
                break;
            case 4:
                li = addELE(li);
                break;
            case 5:
                li = addCIV(li);
                break;
            case 6:
                li = addISE(li); 
                break;
            case 7:
                li = addMAT(li);
                break;
            case 8:
                li = addPHY(li);
                break;
            case 9:
                li = addCHM(li);
                break;
            case 10:
                li = addENG(li);
                break;
            case 11:
                li = addHIST(li);
                break;
            case 12:
                li = addMATH(li);
                break;
            case 13:
                li = addECO(li);
                break;
            case 14:
                li = addMKT(li);
                break;
            case 15:
                li = addACT(li);
                break;
            case 16:
                li = addFIN(li);
                break;
            case 17:
                li = addBIS(li);
                break;
            case 18:
                li = addLAW(li);
                break;
        }
        return li;
    }

    //loads cse courses up to three times
    public List<int> addCSE(List<int> li){
        //Add intro courses to all profs
        li.Add(1);
        li.Add(7);
        li.Add(8);
        li.Add(581);
        li.Add(134);

        int ran = UnityEngine.Random.Range(1, 3);
        li = addCSEHelper(li, UnityEngine.Random.Range(1, 34));
        if(ran == 2){
            li = addCSEHelper(li, UnityEngine.Random.Range(1, 34));
        }
        if(ran == 3){
            li = addCSEHelper(li, UnityEngine.Random.Range(1, 34));
            li = addCSEHelper(li, UnityEngine.Random.Range(1, 34));
        }
        return li;
    }

    //loads che courses two times
    public List<int> addCHE(List<int> li){
        //Add intro courses to all profs
        li.Add(24);//intro
        li.Add(33);//analysis
        li.Add(34);//lab
        li.Add(35);

        int ran = UnityEngine.Random.Range(1, 2);
        if(ran == 1){
            li = addCHEHelper(li, UnityEngine.Random.Range(1, 23));
        }
        if(ran == 2){
            li = addCHEHelper(li, UnityEngine.Random.Range(1, 23));
        }
        return li;
    }

    //loads che courses once
    public List<int> addCIV(List<int> li){
        //Add intro courses to all profs
        li.Add(68);//intro
        li.Add(67);//mat
        li.Add(6);//analysis

        li = addCIVHelper(li, UnityEngine.Random.Range(1, 15));
 
        return li;
    }

    //loads mec courses up to two times
    public List<int> addMEC(List<int> li){
        //Add intro courses to all profs
        li.Add(19);//intro
        li.Add(20);
        li.Add(21);
        li.Add(204);
        li.Add(200);
        li.Add(615);

        int r = UnityEngine.Random.Range(1, 13);
        li = addMECHelper(li, r);
        if(r > 7){
            li = addMECHelper(li, UnityEngine.Random.Range(14, 26));
        }
        return li;
    }

     //loads mat courses up to two times
    public List<int> addMAT(List<int> li){
        //Add intro courses to all profs
        li.Add(161);//intro
        li.Add(162);
        li.Add(167);

         int ran = UnityEngine.Random.Range(1, 2);
        if(ran == 1){
            li = addMATHelper(li, UnityEngine.Random.Range(1, 23));
        }
        if(ran == 2){
            li = addMATHelper(li, UnityEngine.Random.Range(1, 23));
        }
        return li;
    }

     //loads ise courses up to two times
    public List<int> addISE(List<int> li){
        //Add intro courses to all profs
        li.Add(103);//intro
        li.Add(104);
        li.Add(105);
        li.Add(106);
        li.Add(107);

        int ran = UnityEngine.Random.Range(1, 17);
        if(ran < 8){
            li = addISEHelper(li, ran);
        }
        else{
            li = addISEHelper(li, UnityEngine.Random.Range(1, 7));
            li = addISEHelper(li, ran);
        }
        return li;
    }

      //loads ise courses up to two times
    public List<int> addELE(List<int> li){
        //Add intro courses to all profs
        li.Add(233);//intro
        li.Add(234);
        li.Add(235);
        li.Add(236);
        li.Add(237);
        li.Add(238);
        li.Add(242);
        li.Add(246);
        li.Add(248);
        li.Add(249);

        li = addELEHelper(li, UnityEngine.Random.Range(1, 18));
        return li;
    }

    public List<int> addPHY(List<int> li){
        li.Add(9);
        li.Add(507);
        li.Add(13);
        li.Add(29);
        li.Add(30);
        li.Add(31);
        li.Add(405);
        li.Add(408);
        li.Add(411);
        li.Add(413);
        li.Add(782);

        li = addPHYHelper(li, UnityEngine.Random.Range(1, 10));
        return li;
    }

    public List<int> addCHM(List<int> li){
        li.Add(10);
        li.Add(12);
        li.Add(26);
        li.Add(23);
        li.Add(25);
        li.Add(398);
        li.Add(399);
        li.Add(400);
        li.Add(401);
        li.Add(405);
        li.Add(425);

        li = addCHMHelperLab(li, UnityEngine.Random.Range(1, 2));
        li = addCHMHelper(li, UnityEngine.Random.Range(5, 13));
        li = addCHMHelper(li, UnityEngine.Random.Range(1, 4));
        return li;
    }

    public List<int> addBIS(List<int> li){
        li.Add(463);
        li.Add(468);
        li.Add(476);
        li.Add(477);
        li.Add(885);
        li.Add(886);
        li.Add(887);
        li.Add(888);
        li.Add(889);
        li.Add(890);
        li.Add(891);
        li.Add(892);
        li.Add(894);
        li.Add(895);
        li = addBISHelper(li, UnityEngine.Random.Range(1, 20));
        li = addBISHelper(li, UnityEngine.Random.Range(1, 20));
        return li;
    }

    public List<int> addLAW(List<int> li){
        li.Add(472);
        li.Add(473);
        return li;
    }

    public List<int> addBISHelper(List<int> li, int r){
        switch(r){
            case 1://financial analysis
                li.Add(893);
                li.Add(897);
                li.Add(903);
                break;
            case 2://speeking/negotiation
                li.Add(907);
                break;
            case 3://mang. accounting
                li.Add(896);
                break;
            case 4://investment
                li.Add(899);
                break;
            case 5://risk management
                li.Add(900);
                break;
            case 6://real estate
                li.Add(902);
                break;
            case 7://supply and demand
                li.Add(904);
                li.Add(909);
                break;
            case 8://financial management
                li.Add(898);
                li.Add(901);
                break;
            case 9://government
                li.Add(905);
                break;
            case 10://consulting
                li.Add(906);
                break;
            case 11://leadership
                li.Add(908);
                break;
            case 12://logistics
                li.Add(910);
                break;
            case 13://marketing strategy
                li.Add(911);
                li.Add(913);
                break;
            case 14://health
                li.Add(912);
                break;
            case 15://inovation
                li.Add(914);
                break;
            case 16://sales management
                li.Add(915);
                break;
            case 17://brand management
                li.Add(916);
                break;
            case 18://service marketing
                li.Add(917);
                break;
            case 19://internalal finace
                li.Add(918);
                break;
            case 20://global mareting
                li.Add(919);
                break;
        }
        return li;
    }

    public List<int> addECO(List<int> li){
        li.Add(17);
        li.Add(459);
        li.Add(460);
        li.Add(462);
        li.Add(470);
        li.Add(471);
        li.Add(478);
        li.Add(880);
        li.Add(882);
        li.Add(920);
        li.Add(921);
        li.Add(922);

        li = addECOHelper(li, UnityEngine.Random.Range(1, 15));
        li = addECOHelper(li, UnityEngine.Random.Range(1, 15));
        return li;
    }

    public List<int> addECOHelper(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(479);//coop
                break;
            case 2:
                li.Add(481);//bank/money/macro
                li.Add(929);
                li.Add(937);
                li.Add(925);
                break;
            case 3:
                li.Add(482);//labor/industrial
                li.Add(940);
                li.Add(924);
                li.Add(945);
                li.Add(927);
                break;
            case 4:
                li.Add(483);//poor
                break;
            case 5:
                li.Add(484);//sports states
                li.Add(485);
                break;
            case 6:
                li.Add(486);//gov
                li.Add(493);
                li.Add(495);
                li.Add(500);
                li.Add(938);
                break;
            case 7:
                li.Add(487);//energy/electricity
                li.Add(488);
                li.Add(935);
                break;
            case 8:
                li.Add(481);//envrionmental
                li.Add(943);
                break;
            case 9:
                li.Add(489);//health
                li.Add(944);
                li.Add(946);
                break;
            case 10:
                li.Add(490);//math
                li.Add(503);
                li.Add(504);
                li.Add(505);
                li.Add(926);
                li.Add(930);
                li.Add(923);
                li.Add(931);
                li.Add(949);
                break;
            case 11:
                li.Add(491);//analysis/game thero
                li.Add(494);
                li.Add(499);
                li.Add(934);
                li.Add(935);
                li.Add(939);
                li.Add(941);
                li.Add(947);
                li.Add(948);
                li.Add(952);
                break;
            case 12:
                li.Add(492);//international
                li.Add(501);
                li.Add(502);
                li.Add(950);
                li.Add(951);
                break;
            case 13://software
                li.Add(496);
                break;
            case 14://buisness decisions
                li.Add(497);
                li.Add(498);
                li.Add(942);
                break;
            case 15://micro
                li.Add(506);
                li.Add(928);
                li.Add(932);
                break;
            case 16://real
                li.Add(933);
                break;
        }
        return li;
    }

    public List<int> addMKT(List<int> li){
        li.Add(459);
        li.Add(460);
        li.Add(467);
        li.Add(543);
        li.Add(544);
        li.Add(545);
        li.Add(880);
        li.Add(882);
        li = addMKTHelper(li, UnityEngine.Random.Range(5, 11));
        return li;
    }

    //helper to load advanced chemistry labs
    public List<int> addMKTHelper(List<int> li, int r){
        switch(r){
            case 1://mkt campaigns
                li.Add(546);
                li.Add(548);
                li.Add(549);
                break;
            case 2://digital
                li.Add(547);
                li.Add(551);
                break;
            case 3://analysis
                li.Add(550);
                break;
            case 4://retail marketing
                li.Add(552);
                li.Add(556);
                break;
            case 5://brand management
                li.Add(553);
                li.Add(916);
                break;
            case 6://sales
                li.Add(554);
                li.Add(555);
                li.Add(915);
                break;
        }
        return li;
    }

    public List<int> addACT(List<int> li){
        li.Add(459);
        li.Add(460);
        li.Add(464);
        li.Add(465);
        li.Add(559);
        li.Add(560);
        li.Add(879);
        li.Add(882);
        li = addACTHelper(li, UnityEngine.Random.Range(5, 11));
        return li;
    }

    public List<int> addACTHelper(List<int> li, int r){
        switch(r){
            case 1://It
                li.Add(561);
                break;
            case 2://cost
                li.Add(562);
                break;
            case 3://tax
                li.Add(563);
                break;
            case 4://audit
                li.Add(564);
                break;
            case 5://accounting
                li.Add(565);
                break;
            case 6://financial statements
                li.Add(566);
                break;
            case 7://analytics
                li.Add(567);
                break;
        }
        return li;
    }

    public List<int> addFIN(List<int> li){
        li.Add(459);
        li.Add(460);
        li.Add(469);
        li.Add(569);
        li.Add(571);
        li.Add(574);
        li.Add(881);
        li.Add(882);
        li = addFINHelper(li, UnityEngine.Random.Range(1, 4));
        return li;
    }

    public List<int> addFINHelper(List<int> li, int r){
        switch(r){
            case 1://invest
                li.Add(568);
                li.Add(576);
                break;
            case 2://global
                li.Add(572);
                break;
            case 3://risk
                li.Add(573);
                break;
            case 4://real estatet
                li.Add(562);
                break;
        }
        return li;
    }

    //helper to load advanced chemistry labs
    public List<int> addCHMHelperLab(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(426);
                break;
            case 2:
                li.Add(427);
                break;
        }
        return li;
    }

    //helper to load chemistry courses
    public List<int> addCHMHelper(List<int> li, int r){
        switch(r){
            case 1://analytical
                li.Add(423);
                li.Add(439);
                li.Add(795);
                li.Add(798);
                break;
            case 2://advanced inorganic
                li.Add(424);
                li.Add(788);
                li.Add(799);
                li.Add(814);
                break;
            case 3://molecular
                li.Add(428);
                li.Add(430);
                li.Add(800);
                break;
            case 4://thermo
                li.Add(429);
                li.Add(449);
                li.Add(792);
                li.Add(793);
                break;
            case 5://bio chem
                li.Add(431);
                li.Add(444);
                li.Add(445);
                li.Add(789);
                li.Add(809);
                li.Add(819);
                break;
            case 6://metallic
                li.Add(433);
                li.Add(787);
                break;
            case 7://chem bio
                li.Add(434);
                li.Add(435);
                li.Add(794);
                li.Add(810);
                break;
            case 8://crystal
                li.Add(436);
                li.Add(437);
                break;
            case 9://advanced organic
                li.Add(440);
                li.Add(441);
                li.Add(787);
                li.Add(801);
                li.Add(802);
                li.Add(804);
                li.Add(806);
                li.Add(807);
                li.Add(808);
                li.Add(817);
                break;
            case 10://biophysics
                li.Add(442);
                li.Add(443);
                li.Add(816);
                break;
            case 11://polymer
                li.Add(448);
                li.Add(450);
                li.Add(451);
                li.Add(811);
                li.Add(812);
                li.Add(813);
                li.Add(815);
                break;
            case 12://med/pharma
                li.Add(790);
                li.Add(791);
                li.Add(797);
                break;
            case 13://spectral
                li.Add(805);
                li.Add(796);
                li.Add(803);
                break;
        }
        return li;
    }

    public List<int> addENG(List<int> li){
        li.Add(2);
        li.Add(3);
        li.Add(273);
        li.Add(274);
        li.Add(275);
        li.Add(269);
        li.Add(271);
        li.Add(842);
        li.Add(843);
        li.Add(859);
        li.Add(860);
        li.Add(861);

        li = addENGHelper1(li, UnityEngine.Random.Range(1, 3));
        int r = UnityEngine.Random.Range(1, 2);
        if(r == 1){
            li.Add(270);
            li = addENGHelper2(li, UnityEngine.Random.Range(1, 12));
            li = addENGHelper2(li, UnityEngine.Random.Range(1, 12));
            li = addENGHelper2(li, UnityEngine.Random.Range(1, 12));
        }
        else{
            li.Add(272);
            li = addENGHelper3(li, UnityEngine.Random.Range(1, 12));
            li = addENGHelper3(li, UnityEngine.Random.Range(1, 12));
            li = addENGHelper3(li, UnityEngine.Random.Range(1, 12));
        }

        return li;
    }
    public List<int> addHIST(List<int> li){
        li.Add(302);
        li.Add(878);
        li.Add(877);

        li = addHISTHelper1(li, UnityEngine.Random.Range(1, 11));
        li = addHISTHelper2(li, UnityEngine.Random.Range(1, 11));
        return li;
    }
    public List<int> addMATH(List<int> li){
        li.Add(4);
        li.Add(5);
        li.Add(6);
        li.Add(14);
        li.Add(15);
        li.Add(16);
        li.Add(28);
        li.Add(32);
        li.Add(510);
        li.Add(511);
        li.Add(512);
        li.Add(515);
        li.Add(523);
        li.Add(516);
        li.Add(840);

        li = addMATHHelper(li, UnityEngine.Random.Range(1, 9));
        return li;
    }

    public List<int> addMATHHelper(List<int> li, int r){
        switch(r){
            case 1://Stats
                li.Add(517);
                li.Add(518);
                li.Add(519);
                li.Add(532);
                li.Add(835);
                li.Add(836);
                li.Add(837);
                break;
            case 2://models
                li.Add(524);
                li.Add(538);
                li.Add(539);
                li.Add(541);
                li.Add(536);
                break;
            case 3: //processes&applications
                li.Add(518);
                li.Add(530);
                li.Add(532);
                li.Add(838);
                li.Add(839);
                break;
            case 4: //variables/differential
                li.Add(520);
                li.Add(526);
                li.Add(534);
                li.Add(820);
                li.Add(825);
                break;
            case 5: //Geometry
                li.Add(521);
                li.Add(523);
                li.Add(537);
                li.Add(822);
                li.Add(828);
                break;
            case 6: //Logic& number theory
                li.Add(525);
                li.Add(540);
                li.Add(841);
                li.Add(832);
                li.Add(834);
                break;
            case 7: //analysis& enumerative
                li.Add(527);
                li.Add(528);
                li.Add(533);
                li.Add(535);
                li.Add(819);
                li.Add(823);
                li.Add(830);
                li.Add(831);
                break;
            case 8: //graph/topogrophy
                li.Add(529);
                li.Add(531);
                li.Add(827);
                li.Add(829);
                break;
            case 9://algebraic topology
                li.Add(821);
                li.Add(833);
                break;
        }
        return li;
    }

    //add writing course
    public List<int> addENGHelper1(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(299);
                break;
            case 2:
                li.Add(300);
                break;
            case 3:
                li.Add(301);
                break;
        }
        return li;
    }

    public List<int> addENGHelper2(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(276);
                li.Add(844);
                break;
            case 2:
                li.Add(277);
                break;
            case 3:
                li.Add(278);
                break;
            case 4:
                li.Add(279);
                li.Add(845);
                li.Add(846);
                break;
            case 5:
                li.Add(280);
                li.Add(845);
                li.Add(846);
                break;
            case 6:
                li.Add(281);
                li.Add(845);
                li.Add(846);
                break;
            case 7:
                li.Add(282);
                li.Add(847);
                break;
            case 8:
                li.Add(283);
                li.Add(848);
                break;
            case 9:
                li.Add(284);
                li.Add(849);
                break;
            case 10:
                li.Add(285);
                li.Add(850);
                break;
            case 11:
                li.Add(286);
                li.Add(850);
                break;
            case 12://modern grad
                li.Add(851);
                li.Add(852);
                break;
        }
        return li;
    }

    //options
    public List<int> addENGHelper3(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(287);
                break;
            case 2:
                li.Add(288);
                break;
            case 3:
                li.Add(289);
                li.Add(853);
                li.Add(854);
                break;
            case 4:
                li.Add(290);
                li.Add(853);
                li.Add(854);
                break;
            case 5:
                li.Add(291);
                li.Add(853);
                li.Add(854);
                li.Add(855);
                break;
            case 6:
                li.Add(292);
                li.Add(856);
                break;
            case 7:
                li.Add(293);
                li.Add(855);
                break;
            case 8:
                li.Add(294);
                li.Add(856);
                li.Add(857);
                break;
            case 9:
                li.Add(295);
                break;
            case 10:
                li.Add(296);
                break;
            case 11:
                li.Add(297);
                break;
            case 12:
                li.Add(298);
                break;
        }
        return li;
    }

    public List<int> addHISTHelper1(List<int> li, int r){
        switch (r) {
            case 1:
                li.Add(303); //early us
                li.Add(314);
                li.Add(309);
                li.Add(320);
                li.Add(321);
                li.Add(325);
                li.Add(862);
                li.Add(865);
                li.Add(864);
                li.Add(866);
                li.Add(868);
                break;
            case 2:
                li.Add(304); //mid us
                li.Add(315);
                li.Add(317);
                li.Add(329);
                li.Add(863);
                li.Add(866);
                li.Add(869);
                break;
            case 3:
                li.Add(305); //late us
                li.Add(318);
                li.Add(326);
                li.Add(353);
                li.Add(866);
                li.Add(870);
                break;
            case 4: //early europe
                li.Add(330);
                li.Add(334);
                li.Add(335);
                li.Add(341);
                break;
            case 5: //later europe
                li.Add(331);
                li.Add(455);
                li.Add(336);
                li.Add(337);
                li.Add(342);
                li.Add(346);
                li.Add(862);
                li.Add(865);
                li.Add(875);
                break;
            case 6: //UK
                li.Add(332);
                li.Add(454);
                li.Add(344);
                li.Add(871);
                break;
            case 7: //France
                li.Add(338);
                li.Add(339);
                li.Add(343);
                li.Add(345);
                li.Add(456);
                break;
            case 8: //latin
                li.Add(347);
                li.Add(348);
                li.Add(349);
                li.Add(352);
                li.Add(353);
                li.Add(872);
                break;
            case 9: //Africa
                li.Add(354);
                li.Add(355);
                li.Add(357);
                li.Add(358);
                li.Add(360);
                break;
            case 10: //Asia
                li.Add(363);
                li.Add(364);
                li.Add(365);
                li.Add(366);
                li.Add(367);
                li.Add(368);
                break;
            case 11: //India
                li.Add(369);
                break;
            }
        return li;
        }

    public List<int> addHISTHelper2(List<int> li, int r) {
        switch (r) {
            case 2:
                li.Add(307); // millitary
                li.Add(384);
                break;
            case 3: //women
                li.Add(308);
                li.Add(310);
                li.Add(311);
                li.Add(312);
                li.Add(323);
                li.Add(355);
                li.Add(877);
                break;
            case 4: //aas
                li.Add(313);
                li.Add(316);
                li.Add(356);
                li.Add(359);
                li.Add(382);
                break;
            case 5://religion
                li.Add(318);
                break;
            case 6://industry/tech
                li.Add(319);
                li.Add(327);
                li.Add(370);
                li.Add(375);
                li.Add(873);
                li.Add(874);
                break;
            case 7://media/culture
                li.Add(306);
                li.Add(322);
                li.Add(324);
                li.Add(876);
                break;
            case 8://law/medicine
                li.Add(328);
                li.Add(373);
                break;
            case 9: //globalisation
                li.Add(374);
                break;
            case 10: //ideology
                li.Add(376);
                li.Add(386);
                break;
            }
        return li;
        }

    //helper to load ele courses
    public List<int> addELEHelper(List<int> li, int r){
        switch(r){
            case 1://semiconductor
                li.Add(239);
                li.Add(659);
                li.Add(660);
                li.Add(665);
                break;
            case 2://signals
                li.Add(240);
                li.Add(254);
                break;
            case 3://magnets
                li.Add(241);
                break;
            case 4://adv. circuits
                li.Add(243);
                li.Add(250);
                li.Add(646);
                li.Add(666);
                break;
            case 5://electro mag.
                li.Add(245);
                li.Add(642);
                break;
            case 6://electro mech.
                li.Add(247);
                li.Add(661);
                break;
            case 7://linear electronic
                li.Add(251);
                break;
            case 8://med elect.
                li.Add(252);
                li.Add(662);
                break;
            case 9://micro/nano
                li.Add(253);
                li.Add(655);
                break;
            case 10://vlsi
                li.Add(255);
                break;
            case 11://control 
                li.Add(256);
                li.Add(260);
                li.Add(651);
                li.Add(652);
                li.Add(654);
                break;
            case 12://signal processing
                li.Add(259);
                li.Add(645);
                li.Add(650);
                break;
            case 13://communications
                li.Add(258);
                li.Add(653);
                li.Add(658);
                break;
            case 14://wireless
                li.Add(261);
                li.Add(656);
                li.Add(657);
                break;
            case 15://lasers
                li.Add(262);
                li.Add(649);
                break;
            case 16://optics
                li.Add(263);
                li.Add(264);
                li.Add(265);
                li.Add(266);
                li.Add(663);
                li.Add(664);
                break;
            case 17://info therory
                li.Add(643);
                li.Add(653);
                break;
            case 18://power electroncis
                li.Add(257);
                li.Add(644);
                li.Add(647);
                li.Add(648);
                break;
        }
        return li;
        }

    //helper to load ise courses
    public List<int> addISEHelper(List<int> li, int r){
        switch(r){
            case 1://stochastic model + algorithms
                li.Add(111);
                li.Add(110);
                li.Add(715);
                li.Add(718);
                li.Add(726);
                li.Add(735);
                break;
            case 2://manufacturing
                li.Add(112);
                li.Add(109);
                li.Add(721);
                li.Add(731);
                break;
            case 3://optimisation models
                li.Add(113);
                li.Add(709);
                li.Add(711);
                li.Add(712);
                li.Add(719);
                li.Add(725);
                li.Add(736);
                break;
            case 4://it analysis
                li.Add(114);
                li.Add(708);
                li.Add(713);
                li.Add(720);
                li.Add(727);
                break;
            case 5://sim
                li.Add(115);
                li.Add(710);
                break;
            case 6://econ of eng.
                li.Add(116);
                li.Add(118);
                li.Add(717);
                li.Add(730);
                li.Add(734);
                break;
            case 7://prod and inventory control
                li.Add(117);
                li.Add(120);
                li.Add(724);
                li.Add(733);
                break;
            case 8://service eng.
                li.Add(121);
                li.Add(722);
                break;
            case 9://product quality
                li.Add(122);
                break;
            case 10://roboics and automation
                li.Add(123);
                li.Add(723);
                li.Add(732);
                break;
            case 11://organization planning
                li.Add(124);
                li.Add(720);
                li.Add(729);
                break;
            case 12://production eng.
                li.Add(125);
                li.Add(130);
                break;
            case 13://systems eng.
                li.Add(126);
                li.Add(714);
                li.Add(728);
                break;
            case 14://machine learning.
                li.Add(127);
                break;
            case 15://data mining
                li.Add(128);
                li.Add(737);
                li.Add(738);
                break;
            case 16://logistics/supply
                li.Add(129);
                li.Add(716);
                break;
            case 17://health
                li.Add(740);
                li.Add(741);
                li.Add(742);
                li.Add(743);
                break;
        }
        return li;
    }

    //helper to load mat courses
    public List<int> addMATHelper(List<int> li, int r){
        switch(r){
            case 1://mat atomic structure
                li.Add(162);
                li.Add(171);
                li.Add(745);
                break;
            case 2://polymer
                li.Add(163);
                li.Add(194);
                li.Add(195);
                li.Add(196);
                li.Add(764);
                li.Add(765);
                li.Add(766);
                li.Add(767);
                break;
            case 3://thermo
                li.Add(164);
                li.Add(743);
                break;
            case 4://macro/nano
                li.Add(165);
                li.Add(191);
                li.Add(760);
                li.Add(761);
                break;
            case 5://comp methods
                li.Add(166);
                li.Add(182);
                li.Add(193);
                li.Add(763);
                break;
            case 6://transformation
                li.Add(168);
                li.Add(746);
                break;
            case 7://metals
                li.Add(169);
                li.Add(179);
                li.Add(747);
                break;
            case 8://ceramics
                li.Add(170);
                li.Add(748);
                break;
            case 9://final design
                li.Add(172);
                li.Add(174);
                break;
            case 10://electrical prop
                li.Add(173);
                break;
            case 11://failure analysis
                li.Add(176);
                break;
            case 12://unit ops
                li.Add(44);
                break;
            case 13://optics
                li.Add(180);
                li.Add(749);
                break;
            case 14://composites
                li.Add(178);
                break;
            case 15://crystals
                li.Add(181);
                li.Add(187);
                break;
            case 16://biomaterials
                li.Add(183);
                li.Add(184);
                li.Add(185);
                li.Add(751);
                li.Add(752);
                li.Add(753);
                break;
            case 17://Glasses
                li.Add(188);
                li.Add(755);
                li.Add(757);
                break;
            case 18://3d print
                li.Add(189);
                li.Add(759);
                break;
            case 19://Welding
                li.Add(190);
                li.Add(768);
                break;
            case 20://thin film
                li.Add(192);
                li.Add(762);
                break;
            case 21://manufaceing
                li.Add(196);
                break;
            case 22://electron microscopy
                li.Add(750);
                li.Add(754);
                break;
            case 23://solid states
                li.Add(756);
                li.Add(758);
                break;
        }
        return li;
    }

    //helper to load mec courses
    public List<int> addMECHelper(List<int> li, int r){
        switch(r){
            case 1://numeric
                li.Add(197);
                li.Add(26);
                li.Add(623);
                li.Add(635);
                 break;
            case 2://thermo
                li.Add(198);
                li.Add(213);
                li.Add(625);
                break;
            case 3://materials
                li.Add(199);
                li.Add(212);
                break;
            case 4://fluid
                li.Add(201);
                li.Add(215);
                li.Add(622);
                li.Add(624);
                li.Add(629);
                break;
            case 5://dynamic
                li.Add(202);
                li.Add(637);
                break;
            case 6://reliablity
                li.Add(203);
                li.Add(211);
                li.Add(634);
                break;
            case 7://manufacture
                li.Add(205);
                li.Add(621);
                break;
            case 8://mec elecments
                li.Add(206);
                li.Add(224);
                break;
            case 9://mec engineering systems
                li.Add(206);
                li.Add(222);
                break;
            case 10://design
                li.Add(208);
                li.Add(209);
                li.Add(220);
                li.Add(633);
                break;
            case 11://heat transfer
                li.Add(210);
                li.Add(626);
                li.Add(628);
                break;
            case 12://materials +
                li.Add(199);
                li.Add(212);
                li.Add(217);
                break;
            case 13://control mec
                li.Add(216);
                li.Add(221);
                li.Add(631);
                li.Add(632);
                break;
            case 14://gas
                li.Add(214);
                li.Add(630);
                break;
            case 15://mechanisms
                li.Add(218);
                break;
            case 16://metal forming
                li.Add(219);
                break;
            case 17://propulsion
                li.Add(219);
                li.Add(225);
                break;
            case 18://metal forming
                li.Add(226);
                break;
            case 19://renewable energy
                li.Add(227);
                li.Add(638);
                break;
            case 20://coal
                li.Add(228);
                break;
            case 21://energy efficiency
                li.Add(229);
                li.Add(640);
                break;
            case 22://mechatronic
                li.Add(230);
                break;
            case 23://manufacture+
                li.Add(205);
                li.Add(231);
                li.Add(621);
                li.Add(641);
                break;
            case 24://control mec+
                li.Add(216);
                li.Add(221);
                li.Add(232);
                break;
            case 25://auro
                li.Add(636);
                break;
            case 26://acoustics
                li.Add(639);
                break;
        }
        return li;
    }

    //helper to load cse courses
    public List<int> addCSEHelper(List<int> li, int r){
        switch(r){
            case 1:
                li.Add(132);
                li.Add(577);
                break;
            case 2://109 track
                li.Add(132);
                li.Add(138);
                li.Add(142);
                li.Add(577);
                li.Add(587);
                break;
            case 3:
                li.Add(136);
                li.Add(133);
                break;
            case 4:
                li.Add(143);
                li.Add(133);
                break;
            case 5://algorithms tracks
                li.Add(136);
                li.Add(143);
                li.Add(133);
                li.Add(591);
                break;
            case 6://prog lang.
                li.Add(135);
                li.Add(580);
                break;
            case 7://security
                li.Add(153);
                li.Add(592);
                li.Add(590);
                break;
            case 8://software
                li.Add(137);
                li.Add(139);
                break;
            case 9:
                li.Add(137);
                li.Add(135);
                break;
            case 10:
                li.Add(137);
                li.Add(144);
                break;
            case 11:
                li.Add(137);
                li.Add(158);
                break;
            case 12:
                li.Add(137);
                li.Add(140);
                li.Add(141);
                break;
            case 13://algorithm/data tracks
                li.Add(135);
                li.Add(143);
                li.Add(133);
                li.Add(136);
                li.Add(591);
                break;
            case 14:
                li.Add(144);
                li.Add(139);
                break;
            case 15:
                li.Add(132);
                li.Add(146);
                break;
            case 16:
                li.Add(136);
                li.Add(144);
                break;
            case 17:
                li.Add(132);
                li.Add(146);
                break;
            case 18:
                li.Add(147);
                break;
            case 19:
                li.Add(148);
                li.Add(582);
                break;
            case 20:
                li.Add(149);
                li.Add(585);
                break;
            case 21:
                li.Add(150);
                break;
            case 22:
                li.Add(151);
                li.Add(589);
                break;
            case 23:
                li.Add(152);
                break;
            case 24:
                li.Add(135);
                li.Add(154);
                break;
            case 25:
                li.Add(132);
                li.Add(152);
                break;
            case 26:
                li.Add(155);
                break;
            case 27:
                li.Add(156);
                li.Add(594);
                break;
            case 28:
                li.Add(157);
                break;
            case 29:
                li.Add(158);
                li.Add(595);
                break;
            case 30:
                li.Add(159);
                li.Add(596);
                break;
            case 31:
                li.Add(160);
                li.Add(597);
                break;
            case 32://network
                li.Add(579);
                break;
            case 33://language processing
                li.Add(586);
                break;
            case 34://web
                li.Add(593);
                li.Add(588);
                break;
        }
        
        return li;
    }

    public List<int> addCHEHelper(List<int> li, int r){
        switch(r){
            case 1://fluid
                li.Add(36);
                break;
            case 2://thermo
                li.Add(39);
                li.Add(616);
                li.Add(615);
                break;
            case 3://mass trasfer
                li.Add(38);
                li.Add(602);
                li.Add(603);
                li.Add(618);
                break;
            case 4://reacter
                li.Add(42);
                li.Add(617);
                break;
            case 5://design
                li.Add(40);
                li.Add(43);
                break;
            case 6://control
                li.Add(41);
                li.Add(271);
                li.Add(612);
                li.Add(604);
                break;
            case 7://control
                li.Add(41);
                li.Add(272);
                li.Add(612);
                li.Add(604);
                break;
            case 8://biotech
                li.Add(47);
                li.Add(48);
                li.Add(49);
                li.Add(607);
                break;
            case 9://design 2
                li.Add(40);
                li.Add(43);
                break;
            case 10://biochem
                li.Add(46);
                li.Add(51);
                break;
            case 11://models
                li.Add(52);
                li.Add(53);
                break;
            case 12://molecular models
                li.Add(54);
                li.Add(611);
                li.Add(608);
                li.Add(601);
                break;
            case 13://energy
                li.Add(55);
                li.Add(600);
                break;
            case 14://electriochem
                li.Add(56);
                break;
            case 15://fluid/thermo
                li.Add(36);
                li.Add(39);
                break;
            case 16://environment
                li.Add(57);
                li.Add(58);
                break;
            case 17://electriochem
                li.Add(58);
                break;
            case 18://mat
                li.Add(60);
                break;
            case 19://organic
                li.Add(61);
                li.Add(64);
                li.Add(609);
                li.Add(606);
                break;
            case 20://physical
                li.Add(61);
                li.Add(65);
                li.Add(599);
                break;
            case 21://polymers
                li.Add(62);
                li.Add(613);
                li.Add(614);
                li.Add(615);
                break;
            case 22://mat
                li.Add(63);
                break;
            case 23://analysis
                li.Add(37);
                li.Add(610);
                li.Add(605);
                break;
        }
        return li;
    }

    public List<int> addCIVHelper(List<int> li, int r){
        switch(r){
            case 1://Numerical
                li.Add(70);
                li.Add(86);
                li.Add(669);
                li.Add(687);
                li.Add(699);
                break;
            case 2://strength of materials
                li.Add(69);
                li.Add(73);
                break;
            case 3://Environment
                li.Add(71);
                li.Add(100);
                li.Add(87);
                li.Add(88);
                li.Add(667);
                li.Add(702);
                li.Add(704);
                li.Add(705);
                li.Add(706);
                break;
            case 4://Fluid
                li.Add(72);
                li.Add(675);
                //kinetic
                li.Add(101);
                li.Add(682);
                li.Add(692);
                li.Add(700);
                li.Add(701);
                break;
            case 5://Soil
                li.Add(74);
                li.Add(95);
                li.Add(683);
                li.Add(685);
                break;
            case 6://structural
                li.Add(75);
                li.Add(85);
                li.Add(668);
                li.Add(670);
                li.Add(671);
                li.Add(672);
                li.Add(679);
                li.Add(681);
                li.Add(686);
                li.Add(689);
                break;
            case 7://Economics
                li.Add(76);
                li.Add(688);
                break;
            case 8://water engineering
                li.Add(77);
                li.Add(84);
                li.Add(89);
                li.Add(90);
                li.Add(91);
                li.Add(676);
                li.Add(677);
                break;
            case 9://geotechnical
                li.Add(78);
                li.Add(94);
                li.Add(96);
                break;
            case 10://steel
                li.Add(79);
                li.Add(673);
                li.Add(674);
                break;
            case 11://concrete/plastci
                li.Add(81);
                li.Add(99);
                li.Add(693);
                li.Add(696);
                break;
            case 12://design
                li.Add(80);
                li.Add(82);
                li.Add(97);
                li.Add(98);
                li.Add(694);
                break;
            case 13://waste/contamination
                li.Add(102);
                li.Add(678);
                li.Add(703);
                li.Add(707);
                break;
            case 14://risk/hazerd
                li.Add(680);
                li.Add(690);
                li.Add(691);
                li.Add(695);
                li.Add(697);
                li.Add(698);
                break;
            case 15://foundation
                li.Add(93);
                li.Add(684);
                break;
        }
        return li;
    }

    public List<int> addPHYHelper(List<int> li, int r){
        switch(r){
            case 1://quantum
                li.Add(404);
                li.Add(416);
                li.Add(770);
                li.Add(771);
                break;
             case 2://electricity & magnets
                li.Add(406);
                li.Add(409);
                li.Add(769);
                li.Add(775);
                break;
            case 3://classic mechanics
                li.Add(407);
                li.Add(774);
                break;
            case 4://structure
                li.Add(410);
                break;
            case 5://thermal
                li.Add(412);
                li.Add(784);
                break;
            case 6://particle
                li.Add(414);
                li.Add(779);
                li.Add(781);
                break;
            case 7://relativity
                li.Add(415);
                li.Add(785);
                break;
            case 8://solids
                li.Add(417);
                li.Add(777);
                break;
            case 9://optics
                li.Add(418);
                li.Add(419);
                li.Add(780);
                li.Add(783);
                break;
            case 10://plasma
                li.Add(420);
                li.Add(780);
                break;
            case 11://fluids
                li.Add(421);
                li.Add(786);
                break;
            case 12://comp. fluids
                li.Add(422);
                break;
            case 13://mathmatical physics
                li.Add(772);
                li.Add(776);
                li.Add(773);
                li.Add(778);
                break;
        }
        return li;
    }
}