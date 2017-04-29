using System;

internal class Status
{
	public static readonly int SUCCESS;

	public static readonly int REPEATED = 1;

	public static readonly int ACCOUNT_INVALID = 2;

	public static readonly int SECURITY_ERROR = 3;

	public static readonly int DATA_FORMAT_ERROR = 4;

	public static readonly int CFG_ERROR = 5;

	public static readonly int GOLD_NOT_ENOUGH = 6;

	public static readonly int DIAMOND_NOT_ENOUGH = 7;

	public static readonly int NOT_FOUND = 8;

	public static readonly int NOT_BEGIN = 9;

	public static readonly int BEGAN = 10;

	public static readonly int NOT_PAUSE = 11;

	public static readonly int PAUSED = 12;

	public static readonly int INVALID_PARAMS = 13;

	public static readonly int SESSION_ID_REPEATED = 14;

	public static readonly int SCENE_ID_REPEATED = 15;

	public static readonly int WAIT_SCENE_CONN_CONFIRM = 16;

	public static readonly int INNER_ERROR = 17;

	public static readonly int FREEZE = 18;

	public static readonly int NOT_IN_IP_WHITE_LIST = 19;

	public static readonly int SDK_AUTHING = 20;

	public static readonly int SDK_AUTH_FAILED = 21;

	public static readonly int VERSION_NO_MATCH = 22;

	public static readonly int SDK_AUTH_NO_MATCH = 23;

	public static readonly int ILLEGAL_ACTION = 24;

	public static readonly int CLIENT_SN_ERROR = 25;

	public static readonly int NOT_CREATE_ROLE_STATE = 26;

	public static readonly int LOGIC_EXCEPTION = 27;

	public static readonly int IN_FIGHT_SERVER = 80;

	public static readonly int IN_FIGHT_SERVER_VALUE = 92;

	public static readonly int FIGHT_APP_ID_EXIST = 81;

	public static readonly int ALREADY_IN_FIGHT_SERVER = 82;

	public static readonly int NOT_IN_FIGHT_SERVER = 83;

	public static readonly int REPEATED_FIELD_ID = 84;

	public static readonly int FIGHT_FIELD_ID_EXIST = 85;

	public static readonly int SCENE_SERVER_CLOSE = 86;

	public static readonly int NOT_SUPPORT_FIGHT_TYPE = 87;

	public static readonly int FIGHT_SERVER_NOT_FOUND = 88;

	public static readonly int NOT_USE_FIGHT_SERVER = 89;

	public static readonly int SCENE_ROLE_STATE_SAME = 90;

	public static readonly int ROLE_NOT_IN_RECONNECTING = 91;

	public static readonly int DUPLICATE_AUTH = 100;

	public static readonly int RE_LOGIN = 101;

	public static readonly int ROLE_NOT_FOUND = 200;

	public static readonly int DUPLICATE_LOGIN = 201;

	public static readonly int DUPLICATE_REGISTERED = 202;

	public static readonly int ROLE_NOT_LOGIN = 203;

	public static readonly int TYPE_ID_ERROR = 204;

	public static readonly int LV_HAS_REACHED_MAX = 205;

	public static readonly int HAS_NOT_REACH_LV = 206;

	public static readonly int ILLEGALITY_NAME = 207;

	public static readonly int ROLE_OFFLINE = 208;

	public static readonly int ROLE_NAME_HAS_EXIST = 209;

	public static readonly int COULD_NOT_ENTER_SAME_MAP = 300;

	public static readonly int ROLE_NOT_IN_MAP = 301;

	public static readonly int SAME_MAP_POS = 302;

	public static readonly int WILL_SWITCH_MAP_NOT_EXIST = 303;

	public static readonly int MAP_POINT_NOT_WALKABLE = 304;

	public static readonly int ALREADY_IN_MAP = 305;

	public static readonly int MAP_ELEM_NOT_FOUND = 306;

	public static readonly int SAME_MAP_LAYER = 307;

	public static readonly int MAIN_CITY_NOT_OPEN = 308;

	public static readonly int ALREADY_IN_MAP_LINE = 309;

	public static readonly int NOT_IN_MAP_LINE = 310;

	public static readonly int TRANSPORT_NOT_FOUND = 311;

	public static readonly int ENERGY_NOT_ENOUGH = 400;

	public static readonly int ROLE_LEVEL_LIMIT = 401;

	public static readonly int DAILY_CHALLENGE_TIMES_LIMIT = 402;

	public static readonly int ROLE_FIGHTING_LIMIT = 403;

	public static readonly int RE_CHALLENGE_DUNGEON = 404;

	public static readonly int NOT_ENTER_DUNGEON = 405;

	public static readonly int CHALLENGE_TIME_EXPIRED = 406;

	public static readonly int ROLE_COUNT_LIMIT = 407;

	public static readonly int DUNGEON_NOT_EXIST = 408;

	public static readonly int PRE_DUNGEON_NOT_CLEARANCE = 409;

	public static readonly int NOT_SERVER_DUNGEON = 410;

	public static readonly int DUNGEON_CREATED = 411;

	public static readonly int DUNGEON_NOT_CREATE = 412;

	public static readonly int NOT_CHALLENGE_DUNGEON = 413;

	public static readonly int IN_CHALLENGING = 414;

	public static readonly int DUNGEON_NOT_CLEARANCE = 415;

	public static readonly int NOT_ALLOW_MOP_UP = 416;

	public static readonly int DUNGEON_RESET_TIMES_LIMIT = 417;

	public static readonly int DUNGEON_REMAINING_CHALLENGE_TIMES_NON_ZERO = 418;

	public static readonly int DUNGEON_SETTLED = 419;

	public static readonly int DUNGEON_NOT_SETTLE = 420;

	public static readonly int STAR_NOT_ENOUGH = 421;

	public static readonly int MOP_UP_TIME_NOT_ENOUGH = 422;

	public static readonly int DUNGEON_NOT_MOP_UP = 423;

	public static readonly int MOP_UP_STAR_NOT_ENOUGH = 424;

	public static readonly int MOP_UP_NOT_OPEN = 425;

	public static readonly int NOT_ALLOW_CHALLENGE = 426;

	public static readonly int NOT_HAD_CAN_ARENA = 427;

	public static readonly int ARENA_MOP_UP_FINISH = 428;

	public static readonly int CHALLENGE_TIMES_NOT_USED = 429;

	public static readonly int DUNGEON_NOT_OPEN = 430;

	public static readonly int DUNGEON_FIELD_TYPE_ERROR = 431;

	public static readonly int DUNGEON_WITHOUT_PET_FORMATION = 500;

	public static readonly int PET_ID_NOT_EXIST = 501;

	public static readonly int INVALID_INDEX = 502;

	public static readonly int INVALID_PET_FORMATION_TYPE = 503;

	public static readonly int PET_EXIST = 504;

	public static readonly int HAS_NOT_REACH_ACTIVE_LV = 505;

	public static readonly int HAS_NOT_ENOUGH_PET_FRAGMENT = 506;

	public static readonly int HAS_REACH_MAX_STAR = 507;

	public static readonly int HAS_REACH_MAX_QUALITY = 508;

	public static readonly int RUNE_NOT_EXIST = 509;

	public static readonly int RUNE_POS_IS_NOT_FULL = 510;

	public static readonly int RUNE_HAS_EMBED = 511;

	public static readonly int PET_BROKEN = 512;

	public static readonly int PET_FORMATION_PARAM_REPEAT = 513;

	public static readonly int PET_TALENT_NOT_EXIST = 514;

	public static readonly int HAS_REACH_MAX_TALENT_LV = 515;

	public static readonly int SKILL_POINT_NOT_ENOUGH = 516;

	public static readonly int SKILL_POINT_NOT_EMPTY = 517;

	public static readonly int INCORRE_SPONDENCE = 518;

	public static readonly int SOURCE_ITEM_NOT_ENOUGH = 519;

	public static readonly int PET_LV_CAN_NOT_UPPER_PLAYER_LV = 520;

	public static readonly int ITEM_NOT_CORRECT = 521;

	public static readonly int PET_NOT_REACH_LV = 522;

	public static readonly int INVALID_PET_FORMATION_ID = 523;

	public static readonly int LV_NOT_ENOUGH_TO_BATTLE = 524;

	public static readonly int NOT_ALLOW_PET_TYPE = 525;

	public static readonly int PET_LV_NOT_ENOUGH = 526;

	public static readonly int NOT_PET_TASK = 527;

	public static readonly int PET_TASK_PICK_UP = 528;

	public static readonly int PET_HAD_USING = 529;

	public static readonly int PET_TASK_REWARD_NONE = 530;

	public static readonly int PET_TASK_NOT_PET = 531;

	public static readonly int ITEM_NOT_EXIST = 600;

	public static readonly int ITEM_NOT_ENOUGH_COUNT = 601;

	public static readonly int BAG_IS_FULL = 602;

	public static readonly int BAG_NOT_ENOUGH_ROOM = 603;

	public static readonly int ITEM_COST_ERROR = 604;

	public static readonly int BAG_TYPE_NOT_EXIST = 605;

	public static readonly int DATA_NOT_ENOUGH = 606;

	public static readonly int DATA_NOT_CAN_COMPOSITE = 607;

	public static readonly int DATA_NOT_CAN_RECOVERY = 608;

	public static readonly int SKILL_NOT_FOUND = 700;

	public static readonly int SKILL_NOT_ACTIVE = 701;

	public static readonly int SKILL_IN_CD = 702;

	public static readonly int NOT_FOUND_TARGETS = 703;

	public static readonly int ACTION_POINT_NOT_ENOUGH = 704;

	public static readonly int EFFECT_ALREADY_USED = 705;

	public static readonly int EFFECT_CANCELLED = 706;

	public static readonly int EFFECT_INVALID = 707;

	public static readonly int EFFECT_UNIQUE_ID_REPEAT = 708;

	public static readonly int EFFECT_NOT_FOUND = 709;

	public static readonly int SOLDIER_DEAD = 710;

	public static readonly int SOLDIER_IN_FIT = 711;

	public static readonly int SOLDIER_NOT_IN_FIT = 712;

	public static readonly int BUFF_ALREADY_USED = 713;

	public static readonly int BUFF_EXIST = 714;

	public static readonly int BUFF_NOT_FOUND = 715;

	public static readonly int BUFF_STOPPED = 716;

	public static readonly int BUFF_INVALID = 717;

	public static readonly int SOLDIER_COULD_NOT_MOVE = 718;

	public static readonly int NOT_ROLE_TYPE_SOLDIER = 719;

	public static readonly int SOLDIER_NOT_FOUND = 720;

	public static readonly int NOT_EXIST_PET = 721;

	public static readonly int SOLDIER_BORNING = 722;

	public static readonly int SOLDIER_NOT_DEAD = 723;

	public static readonly int OWNER_SOLDIER_DEAD = 724;

	public static readonly int SOLDIER_ASSAULTING = 725;

	public static readonly int SOLDIER_KNOCKING = 726;

	public static readonly int SOLDIER_SUSPENDED = 727;

	public static readonly int SOLDIER_NOT_KNOCKING = 728;

	public static readonly int SOLDIER_NOT_KNOCK_MANAGER = 729;

	public static readonly int FIELD_ROLE_COUNT_LIMIT = 730;

	public static readonly int MASS_LIMIT = 731;

	public static readonly int COULD_NOT_FIND_MANAGER = 732;

	public static readonly int FILTER_NOT_FOUND = 733;

	public static readonly int SOLDIER_SKILL_MANAGING = 734;

	public static readonly int SOLDIER_NOT_SKILL_MANAGING = 735;

	public static readonly int SOLDIER_NOT_SKILL_MANAGE_MANAGER = 736;

	public static readonly int REPEAT_ADD_SKILL = 737;

	public static readonly int SOLDIER_FIGHTING = 738;

	public static readonly int SOLDIER_NOT_FIGHTING = 739;

	public static readonly int FIELD_NOT_FOUND = 740;

	public static readonly int SOLDIER_NOT_LOADING = 741;

	public static readonly int IN_FIGHTING = 742;

	public static readonly int SKILL_NOT_ALLOW_LONG_PRESS = 743;

	public static readonly int IN_SKILL_PRESSING = 744;

	public static readonly int NOT_IN_SKILL_PRESSING = 745;

	public static readonly int FIELD_PAUSED = 746;

	public static readonly int IN_LOADING = 747;

	public static readonly int PET_NOT_FITTABLE = 748;

	public static readonly int SKILL_JUDGE_NEED_ASSAULT = 749;

	public static readonly int SKILL_NOT_ALLOW_CANCEL = 750;

	public static readonly int SOLDIER_NOT_ALLOW_RELIVE = 751;

	public static readonly int SOLDIER_STUNNED = 752;

	public static readonly int SOLDIER_NOT_IN_STUN = 753;

	public static readonly int NOT_IN_CURRENT_SKILL = 754;

	public static readonly int INVALID_CASTER = 755;

	public static readonly int UID_NOT_FOUND = 756;

	public static readonly int ANI_PRIORITY = 757;

	public static readonly int EFFECT_MISS = 758;

	public static readonly int SOLIDER_KNOCKING = 759;

	public static readonly int MANAGE_REPLACE_MAX_LMT = 760;

	public static readonly int FAILED_CHECK_MANAGE_SN = 761;

	public static readonly int ARENA_ROLE_NOT_FOUND = 762;

	public static readonly int CLI_DRV_BATTLE_VERTIFY_FAIL = 763;

	public static readonly int BATTLE_FIELD_ALREADY_EXIST = 764;

	public static readonly int SOLDIER_WEAK = 780;

	public static readonly int SOLDIER_NOT_IN_WEAK = 781;

	public static readonly int EQUIP_IS_WEARING = 800;

	public static readonly int EQUIP_CAREER_NOT_MATCH = 801;

	public static readonly int EQUIP_TYPE_ERROR = 802;

	public static readonly int CAN_NOT_UPPER_PLAYER_LV = 803;

	public static readonly int INTENSIFY_MAX_LV = 804;

	public static readonly int EQUIP_NOT_EXIST = 805;

	public static readonly int LV_NOT_ENOUGH_TO_ADVANCED = 806;

	public static readonly int ADVANCED_MAX_LV = 807;

	public static readonly int WEAPON_SPACE_NOT_ENOUGH = 808;

	public static readonly int SHIRT_SPACE_NOT_ENOUGH = 809;

	public static readonly int PANT_SPACE_NOT_ENOUGH = 810;

	public static readonly int SHOE_SPACE_NOT_ENOUGH = 811;

	public static readonly int WAIST_SPACE_NOT_ENOUGH = 812;

	public static readonly int NECKLACE_SPACE_NOT_ENOUGH = 813;

	public static readonly int ATTR_CFG_ERROR = 814;

	public static readonly int NOT_MATCH_EQUIP_LV = 815;

	public static readonly int LESS_INTENSIFY_STONE = 816;

	public static readonly int CAN_NOT_REFINE = 817;

	public static readonly int ONLY_REFINE_ONE_POSITION = 818;

	public static readonly int LESS_REFINE_MATERIAL = 819;

	public static readonly int ILLEGAL_REFNIE_POSITION = 820;

	public static readonly int NOT_REFINE_OPERATION = 821;

	public static readonly int UP_TO_MAX_STAR = 822;

	public static readonly int NOT_EHCHANCE_STAR_MATERIAL = 823;

	public static readonly int LESS_ENCHANCE_STAR_MATERIAL = 824;

	public static readonly int LESS_LV_TO_ENCHANCE = 825;

	public static readonly int NOT_ENCHANCE_STAR = 826;

	public static readonly int LESS_RESET_MATERIAL = 827;

	public static readonly int CAN_NOT_ENCHANT = 828;

	public static readonly int LESS_ENCHANT_MATERIAL = 829;

	public static readonly int UN_SUIT_FOR_POSITION = 830;

	public static readonly int ERROR_ENCHANT_POSITION = 831;

	public static readonly int ENCHANT_SAME_ATTR = 832;

	public static readonly int NOT_ENCHANT_OPERATION = 833;

	public static readonly int DECOMPOSE_WEARING_EQUIP = 834;

	public static readonly int INTENSIFY_CFG_MAX_LV = 835;

	public static readonly int NOT_CAN_ENCHANT_STAR = 836;

	public static readonly int NOT_ENOUGH_STEP_LV = 837;

	public static readonly int DE_SMELT_WEARING_EQUIP = 838;

	public static readonly int SMELT_FUND_MAX = 839;

	public static readonly int EQUIP_QUALITY_NOT_ENOUGH = 840;

	public static readonly int ADVANCED_STEP_NOT_ENOUGH = 841;

	public static readonly int NOT_WEARING_EQUIP = 842;

	public static readonly int HAD_SUIT_ID = 843;

	public static readonly int NOT_CAN_FORGING_SUIT = 845;

	public static readonly int TASK_NOT_EXISTED = 900;

	public static readonly int INVALID_PARAM = 901;

	public static readonly int TASK_NOT_FINISHED = 902;

	public static readonly int ACTIVITY_NOT_ENOUGH = 903;

	public static readonly int HAS_ALREADY_GOT_ENGERY_PRIZE = 904;

	public static readonly int NOT_ON_TIME = 905;

	public static readonly int NO_SUCH_DAILY_TASK = 906;

	public static readonly int NOT_ENOUGH_COUNT = 907;

	public static readonly int OUT_OF_RANGE_OF_TIME = 908;

	public static readonly int TASK_HAS_GOT_PRIZE = 909;

	public static readonly int TASK_EXISTED = 910;

	public static readonly int MUST_DO_CHANGE_CAREER_TASK = 911;

	public static readonly int TASK_NOT_HAD_TIMES = 918;

	public static readonly int CAN_FIND_TIMES_USED = 912;

	public static readonly int NOT_ENOUGH_FIND_TIMES = 913;

	public static readonly int GOD_WEAPON_TASK_NOT_FINISH = 914;

	public static readonly int TASK_NOT_CAN_ACCEPT = 915;

	public static readonly int TASK_TIMES_NOT_ENOUGH = 916;

	public static readonly int REFRESH_TIME_USED = 917;

	public static readonly int ALL_TASK_RECEIVED = 918;

	public static readonly int TASK_GROUP_HAD_ACCEPT = 919;

	public static readonly int NOT_ENTER_PVP = 950;

	public static readonly int PVP_NOT_OPEN = 951;

	public static readonly int NOT_OWN_THIS_TITLE = 1000;

	public static readonly int HAD_EQUIP_THIS_TITLE = 1001;

	public static readonly int TITLE_HAS_EFFECTIVE = 1002;

	public static readonly int BUDDY_EXIST = 1100;

	public static readonly int BUDDY_IN_BLACK_LIST = 1101;

	public static readonly int IS_NOT_BUDDY = 1102;

	public static readonly int FORBID_JOIN_YOURSELF = 1103;

	public static readonly int BUDDY_REACH_MAX = 1104;

	public static readonly int SIDE_ROLE_BUDDY_REACH_MAX = 1105;

	public static readonly int YOU_ARE_IN_BLACK_LIST = 1106;

	public static readonly int SIDE_INVITE_REACH_MAX = 1107;

	public static readonly int TALK_TOO_QUICK = 1200;

	public static readonly int TALK_FORBIT = 1201;

	public static readonly int NOT_VIDEO = 1202;

	public static readonly int MAIL_DATA_ERROR = 13000;

	public static readonly int MAIL_NOT_FOUND = 13001;

	public static readonly int EMPTY_MAIL_ATTACH = 13002;

	public static readonly int MAIL_ATTACH_HAD_DRAW = 13003;

	public static readonly int REFINE_BODY_LEVEL_LIMIT = 1500;

	public static readonly int REFINE_BODY_EXP_NOT_ENGOUGH = 1501;

	public static readonly int REFINE_BODY_GOLD_COIN_NOT_ENGOUGH = 1502;

	public static readonly int REFINE_BODY_REN_WU_LIAN_TI_XI_TONG_CFG_NOT_EXIST = 1503;

	public static readonly int REFINE_BODY_DIAN_LIANG_SHUN_XU_CFG_NOT_EXIST = 1504;

	public static readonly int REFINE_BODY_DIAN_LIANG_BU_SHU_CFG_NOT_EXIST = 1505;

	public static readonly int REFINE_BODY_LIAN_TI_SHU_XING_CFG_NOT_EXIST = 1506;

	public static readonly int REFINE_BODY_ITEM_NO_EXP = 1507;

	public static readonly int NOT_REACH_ARENA_OPEN_LV = 1601;

	public static readonly int IN_CHALLENGE_CD = 1602;

	public static readonly int CAN_NOT_CANCEL_IN_FIGHTING = 1603;

	public static readonly int HAS_ALREADY_JOIN_IN_ARENA = 1604;

	public static readonly int ENERGY_FULL = 1700;

	public static readonly int ENERGY_BUY_TIMES_LIMIT = 1701;

	public static readonly int HAS_ALREADY_JOIN_IN_GANG_FIGHT = 1800;

	public static readonly int NOT_REACH_GANG_FIGHT_OPEN_LV = 1801;

	public static readonly int GANG_FIGHT_NOT_IN_OPEN_TIME = 1802;

	public static readonly int ALREADY_GANG_FIGHTING = 1803;

	public static readonly int WILD_BOSS_INFO_NOT_EXIST = 1851;

	public static readonly int WILD_BOSS_DROP_ITEM_NOT_EXIST = 1852;

	public static readonly int WILD_BOSS_ROLE_LEVEL_NOT_MATCH = 1853;

	public static readonly int WILD_BOSS_BEING_CHALLENGED = 1854;

	public static readonly int WILD_BOSS_ROLE_NOT_CHALLENGE = 1855;

	public static readonly int WILD_BOSS_CHALLENGE_APPLY_CD = 1856;

	public static readonly int WILD_BOSS_HAVE_NO_TEAM = 1857;

	public static readonly int WILD_BOSS_MIN_LV_LIMIT = 1858;

	public static readonly int WILD_BOSS_CHALLENGE_ROLE_LMT = 1859;

	public static readonly int WILD_BOSS_NOT_IN_JOIN_QUEUE = 1860;

	public static readonly int WILD_BOSS_ALREADY_IN_QUEUE = 1861;

	public static readonly int WILD_BOSS_REWARD_LMT = 1862;

	public static readonly int ALREADY_IN_GUILD = 1900;

	public static readonly int GUILD_REACH_MAX_LV = 1901;

	public static readonly int GUILD_NAME_ALREADY_EXIST = 1902;

	public static readonly int SEARCH_KEY_CAN_NOT_BE_EMPTY = 1903;

	public static readonly int NOT_JOIN_IN_AGUILD = 1904;

	public static readonly int CAN_NOT_OVER_AVERAGE_FIGHTING = 1905;

	public static readonly int ALREADY_APPLIED_FOR_THIS_GUILD = 1906;

	public static readonly int GUILD_NOT_EXIST = 1907;

	public static readonly int CD_REASON = 1908;

	public static readonly int GUILD_IS_FULL = 1909;

	public static readonly int GUILD_IS_UNAVAILABLE = 1910;

	public static readonly int ONLY_CHAIRMAN_COULD_BRING_SOMEBODY_IN = 1911;

	public static readonly int NOT_APPLIED_FOR_THIS_GUILD = 1912;

	public static readonly int MEMBER_NOT_IN_THIS_GUILD = 1913;

	public static readonly int ONLY_CHAIRMAN_COULD_KICK_OFF_AMEMBER = 1914;

	public static readonly int ONLY_CHAIRMAN_COULD_INVITE_SB = 1915;

	public static readonly int ONLY_CHAIRMAN_COULD_REFUSE_SOMEBODY = 1916;

	public static readonly int NOT_APPLY_FOR_THIS_GUILD = 1917;

	public static readonly int EXCEED_MAX_APPLICATIONS_FOR_GUILDS = 1918;

	public static readonly int ALREADY_INVITED_THIS_PLAYER = 1919;

	public static readonly int CAN_NOT_KICK_OFF_CHAIRMAN = 1920;

	public static readonly int PLAYER_NOT_ONLINE = 1921;

	public static readonly int CAN_NOT_INVITE_MEMBER_AGAIN = 1922;

	public static readonly int PLAYER_HAS_JOIN_OTHER_GUILD = 1923;

	public static readonly int GUILD_POWER_NOT_ENOUGH = 1924;

	public static readonly int GUILD_HAS_BE_PROCESS = 1925;

	public static readonly int GUILD_DISSOLVE_GUILD_CD = 1926;

	public static readonly int GUILD_EXIT_GUILD_CD = 1927;

	public static readonly int GUILD_NAME_INVALIDATE = 1928;

	public static readonly int GUILD_NOTICE_INVALIDATE = 1929;

	public static readonly int GUILD_FUND_NOT_ENOUGH = 1930;

	public static readonly int GUILD_NOT_FOUND_TITLE = 1931;

	public static readonly int GUILD_OTHER_MANAGER_DONE = 1932;

	public static readonly int GUILD_CLIENT_PARAM_INVALIDITY = 1933;

	public static readonly int GUILD_TITLE_HAS_FULL = 1934;

	public static readonly int GUILD_SYSTEM_DISSOLVE_CD = 1935;

	public static readonly int GUILD_KICK_OFF_CD = 1936;

	public static readonly int GUILD_REACH_MAX_BUILD_COUNT = 1937;

	public static readonly int GUILD_TASK_HAS_ACCEPT = 1938;

	public static readonly int GUILD_CALL_BOSS_TIME_REACH_MAX = 1939;

	public static readonly int GUILD_BOSS_STILL_LIVE = 1940;

	public static readonly int GUILD_CHALLENGE_BOSS_CD = 1941;

	public static readonly int GUILD_BOSS_ENDING = 1942;

	public static readonly int GUILD_REACH_MAX_CLEAN_COUNT = 1943;

	public static readonly int GUILD_BOSS_CLOSE = 1944;

	public static readonly int GUILD_ESSENCE_NOT_ENOUGH = 1945;

	public static readonly int GUILD_WAR_TIME = 1946;

	public static readonly int GUILD_REACH_MAX_TASK_COUNT = 1947;

	public static readonly int ROLE_NOT_GUILD = 1948;

	public static readonly int ROLE_NOT_ONLINE = 1949;

	public static readonly int NOT_ENOUGH_GOLD_BUY_TIMES = 2000;

	public static readonly int PVE_INVITE_COOLDOWN = 2100;

	public static readonly int PVE_TEAM_NOT_EXIST = 2101;

	public static readonly int PVE_EXCEPTION_REQ = 2102;

	public static readonly int PVE_BE_SURE_NOT_IN_TEAM = 2103;

	public static readonly int PVE_TEAM_HAS_EXIST = 2105;

	public static readonly int PVE_NO_ONE_CAN_INVITE = 2106;

	public static readonly int PVE_NO_REACH_WIN_MAX_COUNT = 2107;

	public static readonly int PVE_IS_NOT_OPEN_TIME = 2108;

	public static readonly int PVE_TEAM_FULL = 2109;

	public static readonly int PVE_REJECT_ENTER = 2110;

	public static readonly int PVE_TEAM_NOT_EXIST1 = 2111;

	public static readonly int PVE_AUTO_MATCH_CD = 2112;

	public static readonly int PVE_QUICK_ENTER_CD = 2113;

	public static readonly int PVE_LOAD_FAILED = 2114;

	public static readonly int PVE_ENTERING = 2115;

	public static readonly int TEAM_FORMING = 2200;

	public static readonly int TEAM_MEMBER_FULL = 2201;

	public static readonly int TEAM_QUICK_MATCH_FAILED = 2202;

	public static readonly int TEAM_HAS_EXIST = 2203;

	public static readonly int TEAM_NOT_FOUND = 2204;

	public static readonly int TEAM_HAS_THIS_MEMBER = 2205;

	public static readonly int TEAM_POWER_NOT_ENOUGH = 2206;

	public static readonly int TEAM_NOT_MEMBER = 2207;

	public static readonly int TEAM_SIDE_HAS_JOIN_OTHERS_TEAM = 2208;

	public static readonly int TEAM_SIDE_HAS_JOIN_TEAM = 2209;

	public static readonly int TEAM_LV_HAS_NOT_FIT = 2210;

	public static readonly int TEAM_INVITE_COOL_DOWN = 2211;

	public static readonly int TEAM_HAS_DISSOLVE = 2212;

	public static readonly int TEAM_LEADER_OFF_LINE = 2213;

	public static readonly int TEAM_NO_ONE_CAN_INVITED = 2214;

	public static readonly int TEAM_NOT_BE_INVITED = 2215;

	public static readonly int TEAM_HAS_FIGHTING = 2216;

	public static readonly int TEAM_ROLE_REQ_QUICK_ENTER = 2217;

	public static readonly int TEAM_INVALID_REQ = 2218;

	public static readonly int TEAM_MUST_EXIT_TEAM = 2219;

	public static readonly int TEAM_ROLE_TOO_LESS = 2220;

	public static readonly int TEAM_MEMBER_HAD_ANSWER = 2221;

	public static readonly int TEAM_ROLE_NOT_LEADER = 2222;

	public static readonly int TEAM_STATE_ERROR = 2223;

	public static readonly int TEAM_MEMBER_REFUSE = 2224;

	public static readonly int TEAM_MEMBER_IN_FIGHT = 2225;

	public static readonly int TEAM_QUICK_JOIN_CD = 2226;

	public static readonly int TEAM_QUICK_JOIN_FAILED = 2227;

	public static readonly int TEAM_TARGET_ERROR = 2228;

	public static readonly int REQUIRED_CONDITIONS_NOT_ENOUGH = 2300;

	public static readonly int ELEM_REACH_MAX_LV = 2301;

	public static readonly int NO_SUCH_ELEM = 2302;

	public static readonly int NO_EXIST_PRO = 2303;

	public static readonly int PRO_NOT_ENOUGH_COUNT = 2304;

	public static readonly int UPGRADE_CONDITION_NOT_ENOUGH = 2305;

	public static readonly int REPEATED_OPEN_VIP_EFFECT = 2404;

	public static readonly int VIP_EFFECT_NOT_FOUND = 2405;

	public static readonly int NOT_OPEN_BOX_TYPE_VIP_EFFECT = 2406;

	public static readonly int VIP_EFFECT_NOT_OPEN = 2407;

	public static readonly int RE_OPEN_BOX_TYPE_VIP_EFFECT = 2408;

	public static readonly int CARD_CAN_NOT_BUY = 2409;

	public static readonly int NO_HAD_THE_SHOP = 2501;

	public static readonly int SHOP_NOT_OPEN = 2502;

	public static readonly int SHOP_NO_ACTIVE_FRESH = 2503;

	public static readonly int SHOP_NO_FRESH = 2504;

	public static readonly int NOT_ENOUGH_REFRESH_PRICE = 2505;

	public static readonly int COMMODITY_CFG_ERROR = 2506;

	public static readonly int NOT_ENOUGH_MONEY = 2507;

	public static readonly int SHOP_VIP_LV_LIMIT = 2508;

	public static readonly int SHOP_REFRESH_TIME_LIMIT = 2509;

	public static readonly int GUILD_OFFER_NOT_ENOUGH = 2510;

	public static readonly int COMPETITIVE_CURRENCY_NOT_ENOUGH = 2511;

	public static readonly int PVP_LV_NOT_ENOUGH = 2512;

	public static readonly int COMMODITY_HAD_SOLD = 2513;

	public static readonly int NOT_HAD_COMMODITY = 2514;

	public static readonly int NUM_NOT_ENOUGH = 2515;

	public static readonly int GUILD_CONTRIBUTION_NOT_ENOUGH = 2516;

	public static readonly int NOT_ENOUGH_EXCHANGE_ITEMS = 2517;

	public static readonly int GOODS_SELLOUT = 2518;

	public static readonly int REPUTATION_NOT_ENOUGH = 2519;

	public static readonly int NOT_IN_GUILD_WAR_WAITING_ROOM = 2600;

	public static readonly int ALREADY_SIGNED_UP_GUILD_WAR = 2601;

	public static readonly int NOT_SIGN_UP_GUILD_WAR = 2602;

	public static readonly int WAITING_ROOM_NOT_OPEN = 2603;

	public static readonly int WAITING_ROOM_NOT_FOUND = 2604;

	public static readonly int BATTLE_ROOM_NOT_FOUND = 2605;

	public static readonly int BATTLE_FIELD_NOT_FOUND = 2606;

	public static readonly int IN_REVIVING_CD = 2607;

	public static readonly int NOT_EXIST_AWARD = 2700;

	public static readonly int NOT_CAN_RECEIVE = 2701;

	public static readonly int AWARD_IS_RECEIVED = 2702;

	public static readonly int AWARD_NOT_HAD_ITEMS = 2703;

	public static readonly int NOT_THIS_RULE_ID = 2704;

	public static readonly int RULE_ID_NULL = 2705;

	public static readonly int SCNOT_CHALLENGE_TIMES = 2800;

	public static readonly int SCCAN_NOT_CHALLENGE = 2801;

	public static readonly int OPEN_SERVER_REWARD_DOES_NOT_EXIT = 2900;

	public static readonly int UNABLE_TO_ACCEPT_THE_OPEN_SERVER_REWARD = 2901;

	public static readonly int HAS_ACCEPT_THE_OPEN_SERVER_REWARD = 2902;

	public static readonly int TODAY_IS_SIGN = 2903;

	public static readonly int REPAIR_SIGN_NU_MHAS_USED_UP = 2904;

	public static readonly int HAS_NO_THIS_SIGN_CONFIG = 2905;

	public static readonly int NOT_REACH_DAY = 2906;

	public static readonly int HAS_GET_PRIZE = 2907;

	public static readonly int NOT_HAD_THE_SKILL = 3000;

	public static readonly int SKILL_LV_MAX = 3001;

	public static readonly int SKILL_UPGRADE_CONDITIONAL_NOT_ENOUGH = 3002;

	public static readonly int SKILL_HAD_UN_LOCK = 3003;

	public static readonly int SKILL_CANT_UN_LOCK = 3004;

	public static readonly int ONE_SKILL = 3005;

	public static readonly int ROLE_LV_LIMIT = 3006;

	public static readonly int NOT_HAD_THE_PET = 3007;

	public static readonly int NOT_ENOUGH_VIP_LV = 3008;

	public static readonly int SKILL_CONFIG_HAD_UN_LOCK = 3009;

	public static readonly int NOT_ENOUGH_DATA = 3010;

	public static readonly int NOT_HAD_THIS_SKILL_CONFIG = 3011;

	public static readonly int ONLY_EXCELLENT_UP = 3012;

	public static readonly int GEM_HAS_NOT_REACH_LV = 3100;

	public static readonly int GEM_INPUT_PARAM_ERROR = 3101;

	public static readonly int GEM_EMBED_TYPE_ERROR = 3102;

	public static readonly int GEM_EMBED_TYPE_SAME_ERROR = 3103;

	public static readonly int GEM_NOT_FOUND = 3104;

	public static readonly int GEM_REACH_MAX_LV = 3105;

	public static readonly int GEM_HAS_NOT_ENOUGH = 3106;

	public static readonly int RUNE_STONE_NOT_OPEN = 3110;

	public static readonly int RUNE_STONE_REACH_MAX = 3111;

	public static readonly int RUNE_STONE_MNOT_ENOUGH = 3112;

	public static readonly int RUNE_STONE_GROUP_EMPTY = 3113;

	public static readonly int COUPON_NOT_ENOUGH = 3200;

	public static readonly int DRAW_LUCK_NOT_OPEN = 3201;

	public static readonly int DRAW_NOT_EXIST = 3202;

	public static readonly int DEF_NOT_OPEN_TIME = 3300;

	public static readonly int DEF_EXIST_CHALLENGE = 3301;

	public static readonly int DEF_HAS_NOT_CHALLENGE = 3302;

	public static readonly int DEF_HAS_USED_PROPS = 3303;

	public static readonly int DEF_UNSUITED_PROPS = 3304;

	public static readonly int DEF_HAS_REACH_MAX_BUY_TIMES = 3305;

	public static readonly int DEF_HAS_NO_CHALLENGE_TIMES = 3306;

	public static readonly int DEF_ENTERING_AREA = 3307;

	public static readonly int DEF_ILLEGAL_REQ = 3308;

	public static readonly int DEF_FACE_PLAYER_LEAVE = 3309;

	public static readonly int DEF_CREATE_FIELD_FAILED = 3310;

	public static readonly int DEF_ROBOT_NO_ENOUGH = 3311;

	public static readonly int NOT_PLATES = 3400;

	public static readonly int BLOCK_NOT_ACTIVATE = 3401;

	public static readonly int EXPLORE_ENERGY_USED_UP = 3402;

	public static readonly int PURCHASE_NUM_UP_TO_LIMITED = 3403;

	public static readonly int NOT_DEBRIS_CAN_ACCEPT = 3404;

	public static readonly int NOT_MINE_BLOCK = 3405;

	public static readonly int BLOCK_HAS_OCCUPY = 3406;

	public static readonly int PET_HAS_OCCUPY_MINE = 3407;

	public static readonly int NOT_THIS_PET = 3408;

	public static readonly int MINE_NOT_OCCUPY = 3409;

	public static readonly int NOT_CHALLENGE_BLOCK = 3410;

	public static readonly int BLOCK_HAS_CHALLENGED = 3411;

	public static readonly int SAME_BLOCK = 3412;

	public static readonly int NOT_HAD_THIS_TYPE = 3500;

	public static readonly int HAD_BUY = 3501;

	public static readonly int NOT_EXIST_ACTIVITY = 3600;

	public static readonly int NOT_FIRST_OPEN = 3601;

	public static readonly int PRIZE_CANNOT_GET = 3602;

	public static readonly int ACTIVITY_JOINED = 3603;

	public static readonly int ACTIVITY_OVER = 3604;

	public static readonly int HAS_BUY = 3704;

	public static readonly int HAS_NOT_BUY = 3705;

	public static readonly int NOT_CURRENT_ACHIEVEMENT = 3700;

	public static readonly int NOT_COMPLETE = 3701;

	public static readonly int ACHIEVEMENT_CFG_ERROR = 3702;

	public static readonly int ACHIEVEMENT_HAS_COMPLETED = 3703;

	public static readonly int BOUNTY_NOT_REACH_OPEN_TIME = 3800;

	public static readonly int BOUNTY_TEAM_HAS_EXIST = 3801;

	public static readonly int BOUNTY_TASK_CLOSE = 3802;

	public static readonly int BOUNTY_BOX_NOT_FOUND = 3803;

	public static readonly int BOUNTY_OPEN_BOX_NEED_MORE_STAR = 3804;

	public static readonly int BOUNTY_CANNOT_REF_URGENT_TASK = 3805;

	public static readonly int BOUNTY_UN_CHOOSE_TASK = 3806;

	public static readonly int BOUNTY_MATCH_FAILED = 3807;

	public static readonly int BOUNTY_HAS_TEAM = 3808;

	public static readonly int BOUNTY_HAS_GOT_STAR_BOX = 3809;

	public static readonly int BOUNTY_MATCH_FAILED_FOR_LV = 3810;

	public static readonly int BOUNTY_MATCH_FAILED_FOR_SCORE = 3811;

	public static readonly int BOUNTY_REACH_MAX_QUALITY = 3812;

	public static readonly int BOUNTY_REFRESH_COUNT_DOWN = 3813;

	public static readonly int BOUNTY_PRODUCT_TEAM_FULL = 3814;

	public static readonly int BOUNTY_TASK_NOT_FOUNT = 3815;

	public static readonly int BOUNTY_TASK_HAD_DONE = 3816;

	public static readonly int LESS_LV_TO_ENTER_SECRET_AREA = 3900;

	public static readonly int LESS_CHALLENGE_TIMES = 3901;

	public static readonly int NOT_CHALLENGE_SECRET_AREA = 3902;

	public static readonly int UPPER_LIMITED_BUY_TIMES = 3903;

	public static readonly int WING_HAS_EXIST = 4000;

	public static readonly int WING_HAS_NOT_EXIST = 4001;

	public static readonly int WING_REACH_MAX_LV = 4002;

	public static readonly int WING_TO_ACTIVITY = 4003;

	public static readonly int HAD_GOT_TASK = 4100;

	public static readonly int HAD_NOT_OPEN = 4101;

	public static readonly int HAD_NOT_CHANGE_CAREER = 4102;

	public static readonly int HAD_NOT_FINISH_TASK = 4103;

	public static readonly int HAD_NOT_GOT_ANY_TASK = 4104;

	public static readonly int UPDATE_AC_NOT_ACCEPT = 4200;

	public static readonly int AC_STATUS_FINISHED = 4201;

	public static readonly int AC_STATUS_READY = 4202;

	public static readonly int AC_STATUS_START = 4203;

	public static readonly int AC_STATUS_CLOSE = 4204;

	public static readonly int TALENT_HAS_EXIST = 4300;

	public static readonly int TALENT_HAS_NOT_EXIST = 4301;

	public static readonly int TALENT_REACH_MAX_LV = 4302;

	public static readonly int TALENT_CAREER_NOT_SUITED = 4303;

	public static readonly int TALENT_PRES_COND_NOT_SUITED = 4304;

	public static readonly int TALENT_POINT_NOT_ENOUGH = 4305;

	public static readonly int TALENT_NEVER_CHANGE_CAREER = 4306;

	public static readonly int TALENT_PRES_COND_LV_NOT_REACH = 4307;

	public static readonly int TLSALES_NOT_EXIST_GOODS = 4400;

	public static readonly int TLSALES_OUT_OF_DAYS = 4401;

	public static readonly int TLSALES_HAS_SELL_OUT = 4402;

	public static readonly int TLSALES_NO_DATA = 4403;

	public static readonly int TLSALES_NO_OPEN = 4404;

	public static readonly int AC_NOT_FINISH = 4500;

	public static readonly int AC_HAD_GOT_PRIZE = 4501;

	public static readonly int MEMORY_FLOP_CHALLENGING = 4600;

	public static readonly int MEMORY_FLOP_NOT_CHALLENGE = 4601;

	public static readonly int MEMORY_FLOP_VERIFY_FAILED = 4602;

	public static readonly int MEMORY_FLOP_EXTEND_TIMES_LMT = 4603;

	public static readonly int MEMORY_FLOP_STILL_HAVE_TIMES = 4604;

	public static readonly int MEMORY_FLOP_CHALLENGE_TIMES_LMT = 4605;

	public static readonly int EXPERIENCE_COPY_CHALLENGE_LIMIT = 4610;

	public static readonly int EXPERIENCE_COPY_EXTEND_TIMES_LIMIT = 4611;

	public static readonly int EXPERIENCE_COPY_BUF_ID_ERROR = 4612;

	public static readonly int NOT_HAD_TIMES = 4620;

	public static readonly int STOCK_NOT_ENOUGH = 4629;

	public static readonly int TIMES_USED = 4630;

	public static readonly int NOT_IN_GAME = 4631;

	public static readonly int GAME_OVER = 4632;

	public static readonly int SYSTEM_NOT_OPEN = 4633;

	public static readonly int ALREADY_IN_GUILD_FIELD = 4640;

	public static readonly int NOT_IN_GUILD_FIELD = 4641;

	public static readonly int GUILD_FIELD_NOT_OPEN = 4642;

	public static readonly int NOT_IN_VIP_AREA = 4643;

	public static readonly int VIP_AREA_POWER_LIMIT = 4644;

	public static readonly int NOT_VIP_AREA = 4645;

	public static readonly int COND_MAIN_CITY_TRANSPORT_ERR = 4646;

	public static readonly int ALREADY_IN_GUILD_WAR_MAP = 4647;

	public static readonly int OPEN_SERVER_NOT_OPEN = 4660;

	public static readonly int OPEN_SERVER_NOT_FINISH = 4661;

	public static readonly int OPEN_SERVER_HAD_GOT = 4662;

	public static readonly int NOT_HAD_THIS_CFG = 4670;

	public static readonly int HAD_TRACE = 4671;

	public static readonly int NOT_HAD_TRACE = 4672;

	public static readonly int ALREADY_IN_GUILD_BOSS = 4690;

	public static readonly int NOT_IN_GUILD_BOSS = 4691;

	public static readonly int ALREADY_HAS_GUILD_BOSS_ROOM = 4692;

	public static readonly int NOT_HAS_GUILD_BOSS_ROOM = 4693;

	public static readonly int FASHION_NOT_EXIST = 4710;

	public static readonly int FASHION_NOT_GET = 4711;

	public static readonly int CAREER_NOT_MATCH = 4712;

	public static readonly int FASHION_EXISTED = 4713;

	public static readonly int FASHION_CAN_NOT_BUY = 4714;

	public static readonly int WING_NOT_OPEN = 4715;

	public static readonly int GUILD_WAR_ERROR_PARAM = 4730;

	public static readonly int GUILD_WAR_NOT_IN_BATTLE = 4731;

	public static readonly int GUILD_WAR_DUNGEON_NOT_EXIST = 4732;

	public static readonly int GUILD_WAR_ROOM_EXIST = 4733;

	public static readonly int GUILD_WAR_NOT_IN_ROOM = 4734;

	public static readonly int GUILD_WAR_ROOM_NOT_EXIST = 4735;

	public static readonly int GUILD_WAR_SOLDIER_NUM_LMT = 4736;

	public static readonly int GUILD_WAR_HAS_GOT_DAILY_PRIZE = 4750;

	public static readonly int GUILD_WAR_NO_PRIZE_WEEK_DAY = 4751;

	public static readonly int GUILD_WAR_NOT_WEEK_CHAMPION = 4752;

	public static readonly int GUILD_WAR_DAILY_PRIZE_TIME_LIMIT = 4753;

	public static readonly int GUILD_WAR_MEMBER_TIME_LIMIT = 4754;

	public static readonly int GUILD_WAR_RELIVE_HAS_DUE = 4755;

	public static readonly int GUILD_WAR_ROLE_DEAD = 4756;

	public static readonly int GUILD_WAR_NO_THIS_GUILD = 4757;

	public static readonly int GUILD_WAR_NO_VS_GUILD = 4758;

	public static readonly int GUILD_WAR_JOIN_TIME_LIMIT = 4759;

	public static readonly int GUILD_WAR_NOT_CHAMPION_MEMBER = 4760;

	public static readonly int GUILD_WAR_HAS_NOT_RESULT = 4761;

	public static readonly int ELITE_DUNGEON_LIMIT = 4810;

	public static readonly int ELITE_TASK_LIMIT = 4811;

	public static readonly int ELITE_QUICK_ENTER_CD = 4812;

	public static readonly int ELITE_INQUIRED_CD = 4813;

	public static readonly int ELITE_ENTERING = 4814;

	public static readonly int ELITE_PRIZE_REACH_MAX = 4815;

	public static readonly int ELITE_NOT_FIRST_CHALLENGE = 4816;

	public static readonly int ELITE_DUNGEON_NOT_NONE = 4817;

	public static readonly int ELITE_DUNGEON_NOT_OPEN = 4818;

	public static readonly int FUND_HAD_BUY = 4900;

	public static readonly int LV_TOO_LOW = 4901;

	public static readonly int INDEX_INVALID = 4902;

	public static readonly int NOT_BUY_FUND = 4903;

	public static readonly int FUND_IS_OVERDUE = 4904;

	public static readonly int FUND_HAD_GET = 4905;

	public static readonly int FUND_NOT_SIGN = 4906;

	public static readonly int OFF_LINE_OVERFLOW = 4907;

	public static readonly int HOOK_ROOM_NOT_EXIST = 4911;

	public static readonly int HOOK_ROOM_ROLE_LMT = 4912;

	public static readonly int ALREADY_IN_HOOK_ROOM = 4913;

	public static readonly int ROLE_NOT_IN_HOOK_ROOM = 4914;

	public static readonly int HOOK_ROOM_CANT_PK = 4915;

	public static readonly int HOOK_NOT_LEGAL_OPERATION = 4916;

	public static readonly int REMAIN_HOOK_TIME_NOT_ENOUGH = 4917;

	public static readonly int BUY_TIMES_OVER = 4918;

	public static readonly int ACCUMULATE_LIMIT = 4919;

	public static readonly int HOOK_BAG_IS_FULL = 4920;

	public static readonly int ROLE_HOOK_BAG_INVALID = 4921;

	public static readonly int REFRESH_INTER_VAL = 4922;

	public static readonly int AC_OPEN_SERVER_NOT_OVER = 4950;

	public static readonly int AC_TASK_NOT_ACHIEVE = 4951;

	public static readonly int AC_NOT_OPEN = 4952;

	public static readonly int NOT_GUILD_SKILL = 4970;

	public static readonly int GUILD_SKILL_MAX = 4971;

	public static readonly int ON_MAX_GUILD_SKILL_LV = 4972;

	public static readonly int NOT_GUILD_SKILL_COST = 4973;

	public static readonly int GAME_STOCK_NOT_ENOUGH = 4990;

	public static readonly int ITEM_NUM_NOT_ENOUGH = 4991;

	public static readonly int NO_NEED_REPLACE = 4992;

	public static readonly int RED_PACKET_NOT_EXIST = 5000;

	public static readonly int YOU_NOT_LUCKY = 5001;

	public static readonly int MULTI_PVP_DAILY_REWARD_GET_ALREADY = 5010;

	public static readonly int MULTI_PVP_REWARD_CANT_GET = 5011;

	public static readonly int MATCH_IS_GOING = 5020;

	public static readonly int PROTECT_TIMES_USED = 5050;

	public static readonly int PROTECTING = 5051;

	public static readonly int MAP_ID_NOT_EXISTED = 5052;

	public static readonly int PROTECT_CLOSE = 5053;

	public static readonly int GRABBING = 5054;

	public static readonly int GRAB_OUT = 5055;

	public static readonly int GRAB_CD = 5056;

	public static readonly int GRAB_TIMES_USED = 5057;

	public static readonly int HELPER_MAX = 5058;

	public static readonly int FRIEND_HELP_PROTECT_TIMES_USED = 5059;

	public static readonly int FRIEND_HELP_INVITING = 5060;

	public static readonly int PROTECT_FIGHTING = 5061;

	public static readonly int REFRESH_ITEM_NOT_ENOUGH = 5062;

	public static readonly int TRAMCAR_MAX_QUALITY = 5063;

	public static readonly int LV_NOT_MATCH = 5064;

	public static readonly int REFRESH_CD = 5065;

	public static readonly int FIGHT_CLOSE = 5066;

	public static readonly int HAD_HELPER = 5067;

	public static readonly int GUILD_STORAGE_ITEM_YET_EXCHANGE = 5100;

	public static readonly int GUILD_STORAGE_IS_FULL = 5101;

	public static readonly int GUILD_STORAGE_NOT_EXIT = 5102;

	public static readonly int GUILD_STORAGE_NOT_DONATE = 5103;

	public static readonly int GUILD_STORAGE_EXHANGES_USE_UP = 5104;

	public static readonly int GUILD_STORAGE_POINTS_NOT_ENOUGH = 5105;

	public static readonly int UNKNOWN = -1;

	public static string GetStatusDesc(int status)
	{
		if (status == 0)
		{
			return "成功";
		}
		if (status == 1)
		{
			return "重复";
		}
		if (status == 2)
		{
			return "账号无效";
		}
		if (status == 3)
		{
			return "安全错误";
		}
		if (status == 4)
		{
			return "数据格式错误";
		}
		if (status == 5)
		{
			return "配置错误";
		}
		if (status == 6)
		{
			return "金币不足";
		}
		if (status == 7)
		{
			return "钻石不足";
		}
		if (status == 8)
		{
			return "未找到";
		}
		if (status == 9)
		{
			return "未开始";
		}
		if (status == 10)
		{
			return "已开始";
		}
		if (status == 11)
		{
			return "未暂停";
		}
		if (status == 12)
		{
			return "已暂停";
		}
		if (status == 13)
		{
			return "无效参数";
		}
		if (status == 14)
		{
			return "SessionId重复";
		}
		if (status == 15)
		{
			return "场景ID重复";
		}
		if (status == 16)
		{
			return "等待场景服确认连接";
		}
		if (status == 17)
		{
			return "内部错误";
		}
		if (status == 18)
		{
			return "此号被封";
		}
		if (status == 19)
		{
			return "服务器维护中";
		}
		if (status == 20)
		{
			return "sdk鉴权中";
		}
		if (status == 21)
		{
			return "sdk鉴权失败";
		}
		if (status == 22)
		{
			return "版本不一致，请更新客户端";
		}
		if (status == 23)
		{
			return "您进错服了";
		}
		if (status == 24)
		{
			return "非法请求";
		}
		if (status == 25)
		{
			return "客户端序号递减出错";
		}
		if (status == 26)
		{
			return "服务器爆满，请选择其它服务器";
		}
		if (status == 27)
		{
			return "逻辑处理异常，马上断开";
		}
		if (status == 80)
		{
			return "战斗服处理中，返回错误码";
		}
		if (status == 92)
		{
			return "战斗服处理中，返回错误值";
		}
		if (status == 81)
		{
			return "战斗服id已存在，注册失败";
		}
		if (status == 82)
		{
			return "已经在战斗服中";
		}
		if (status == 83)
		{
			return "不在战斗服中";
		}
		if (status == 84)
		{
			return "重复的战斗服id";
		}
		if (status == 85)
		{
			return "战斗服战场id已存在";
		}
		if (status == 86)
		{
			return "场景服关闭";
		}
		if (status == 87)
		{
			return "战斗服不支持的战斗类型";
		}
		if (status == 88)
		{
			return "战斗服未找到";
		}
		if (status == 89)
		{
			return "不使用战斗服";
		}
		if (status == 90)
		{
			return "战斗服中玩家状态和场景服中玩家状态一致，不需要同步。";
		}
		if (status == 91)
		{
			return "玩家不在重连状态";
		}
		if (status == 100)
		{
			return "重复执行登陆鉴权";
		}
		if (status == 101)
		{
			return "重连失败,请重新登录";
		}
		if (status == 200)
		{
			return "角色未找到";
		}
		if (status == 201)
		{
			return "重复登陆";
		}
		if (status == 202)
		{
			return "重复注册";
		}
		if (status == 203)
		{
			return "角色未登陆";
		}
		if (status == 204)
		{
			return "类型Id错误";
		}
		if (status == 205)
		{
			return "已经到达最大等级";
		}
		if (status == 206)
		{
			return "角色等级不足";
		}
		if (status == 207)
		{
			return "不合法的名字";
		}
		if (status == 208)
		{
			return "角色已下线";
		}
		if (status == 209)
		{
			return "角色名已存在";
		}
		if (status == 300)
		{
			return "不能重复进入同一个地图";
		}
		if (status == 301)
		{
			return "角色不在当前地图中";
		}
		if (status == 302)
		{
			return "相同的地图点";
		}
		if (status == 303)
		{
			return "将要切换的地图不存在";
		}
		if (status == 304)
		{
			return "地图点不可走";
		}
		if (status == 305)
		{
			return "你已经在该场景中";
		}
		if (status == 306)
		{
			return "地图元素未发现";
		}
		if (status == 307)
		{
			return "相同的地图层级";
		}
		if (status == 308)
		{
			return "主城未开启";
		}
		if (status == 309)
		{
			return "你已经在该地图分线中";
		}
		if (status == 310)
		{
			return "不在该地图分线中";
		}
		if (status == 311)
		{
			return "传送点未找到";
		}
		if (status == 400)
		{
			return "体力不足";
		}
		if (status == 401)
		{
			return "等级限制";
		}
		if (status == 402)
		{
			return "每日挑战次数限制";
		}
		if (status == 403)
		{
			return "角色战力限制";
		}
		if (status == 404)
		{
			return "重复进入副本";
		}
		if (status == 405)
		{
			return "未进入副本";
		}
		if (status == 406)
		{
			return "挑战超时";
		}
		if (status == 407)
		{
			return "进入人数限制";
		}
		if (status == 408)
		{
			return "副本不存在";
		}
		if (status == 409)
		{
			return "前置副本未通关";
		}
		if (status == 410)
		{
			return "非服务端副本";
		}
		if (status == 411)
		{
			return "副本已经创建";
		}
		if (status == 412)
		{
			return "副本未创建";
		}
		if (status == 413)
		{
			return "未挑战副本";
		}
		if (status == 414)
		{
			return "在挑战中";
		}
		if (status == 415)
		{
			return "副本未通关";
		}
		if (status == 416)
		{
			return "扫荡条件不满足,不允许挑战";
		}
		if (status == 417)
		{
			return "重置挑战次数到达上限";
		}
		if (status == 418)
		{
			return "剩余挑战次数不为0";
		}
		if (status == 419)
		{
			return "副本已经结算过";
		}
		if (status == 420)
		{
			return "副本未结算";
		}
		if (status == 421)
		{
			return "通关副本星数不足";
		}
		if (status == 422)
		{
			return "扫荡次数不足";
		}
		if (status == 423)
		{
			return "副本不允许扫荡";
		}
		if (status == 424)
		{
			return "扫荡副本所需通关星数不足";
		}
		if (status == 425)
		{
			return "扫荡功能未开启";
		}
		if (status == 426)
		{
			return "副本不允许挑战";
		}
		if (status == 427)
		{
			return "没有可收割的副本";
		}
		if (status == 428)
		{
			return "区域扫荡次数已用完";
		}
		if (status == 429)
		{
			return "挑战次数没用完";
		}
		if (status == 430)
		{
			return "副本待续开启";
		}
		if (status == 431)
		{
			return "副本战场类型错误";
		}
		if (status == 500)
		{
			return "该副本没有宠物出战队列";
		}
		if (status == 501)
		{
			return "宠物ID不存在";
		}
		if (status == 502)
		{
			return "无效的下标";
		}
		if (status == 503)
		{
			return "无效的宠物队列枚举类型";
		}
		if (status == 504)
		{
			return "宠物已存在";
		}
		if (status == 505)
		{
			return "未达到激活等级";
		}
		if (status == 506)
		{
			return "宠物碎片不足";
		}
		if (status == 507)
		{
			return "达到最大星级";
		}
		if (status == 508)
		{
			return "达到最大品质";
		}
		if (status == 509)
		{
			return "没有对应的符文";
		}
		if (status == 510)
		{
			return "符文格必段填满";
		}
		if (status == 511)
		{
			return "符文已经嵌入";
		}
		if (status == 512)
		{
			return "宠物打碎成碎片返回";
		}
		if (status == 513)
		{
			return "宠物队列宠物ID重复";
		}
		if (status == 514)
		{
			return "宠物天赋不存在";
		}
		if (status == 515)
		{
			return "达到最大宠物天赋等级";
		}
		if (status == 516)
		{
			return "技能点不足";
		}
		if (status == 517)
		{
			return "技能点不为空,不能购买";
		}
		if (status == 518)
		{
			return "id 和 num 不对应";
		}
		if (status == 519)
		{
			return "道具不足";
		}
		if (status == 520)
		{
			return "宠物等级不能超过人物等级";
		}
		if (status == 521)
		{
			return "不是宠物升级所需要的道具";
		}
		if (status == 522)
		{
			return "宠物等级不足";
		}
		if (status == 523)
		{
			return "无效的宠物上阵id参数";
		}
		if (status == 524)
		{
			return "角色等级不足，宠物不能上阵";
		}
		if (status == 525)
		{
			return "副本限制宠物类型上阵";
		}
		if (status == 526)
		{
			return "宠物等级不足";
		}
		if (status == 527)
		{
			return "宠物任务不存在";
		}
		if (status == 528)
		{
			return "宠物任务无法接取";
		}
		if (status == 529)
		{
			return "有宠物正在使用中";
		}
		if (status == 530)
		{
			return "奖励已领取";
		}
		if (status == 531)
		{
			return "请选择宠物";
		}
		if (status == 600)
		{
			return "物品不存在";
		}
		if (status == 601)
		{
			return "物品数量不足";
		}
		if (status == 602)
		{
			return "您的背包已满，请先整理您的背包";
		}
		if (status == 603)
		{
			return "您的背包空间不足，请先整理您的背包";
		}
		if (status == 604)
		{
			return "消耗的物品不对";
		}
		if (status == 605)
		{
			return "不存在此背包类型";
		}
		if (status == 606)
		{
			return "合成材料不足";
		}
		if (status == 607)
		{
			return "此道具不能合成";
		}
		if (status == 608)
		{
			return "此道具不能回收";
		}
		if (status == 700)
		{
			return "技能未发现";
		}
		if (status == 701)
		{
			return "技能不是主动技能";
		}
		if (status == 702)
		{
			return "技能在CD中";
		}
		if (status == 703)
		{
			return "未发现目标";
		}
		if (status == 704)
		{
			return "行动点不够";
		}
		if (status == 705)
		{
			return "效应已经使用,无法重复使用";
		}
		if (status == 706)
		{
			return "效应已经取消,无法继续使用";
		}
		if (status == 707)
		{
			return "效应无效";
		}
		if (status == 708)
		{
			return "效应唯一Id重复";
		}
		if (status == 709)
		{
			return "效应未发现";
		}
		if (status == 710)
		{
			return "士兵已经死亡";
		}
		if (status == 711)
		{
			return "在合体状态";
		}
		if (status == 712)
		{
			return "不在合体状态";
		}
		if (status == 713)
		{
			return "Buff已经使用";
		}
		if (status == 714)
		{
			return "Buff已经存在";
		}
		if (status == 715)
		{
			return "Buff未发现";
		}
		if (status == 716)
		{
			return "Buff已经停止";
		}
		if (status == 717)
		{
			return "Buff无效";
		}
		if (status == 718)
		{
			return "士兵不能移动";
		}
		if (status == 719)
		{
			return "不是角色类型士兵";
		}
		if (status == 720)
		{
			return "士兵未发现";
		}
		if (status == 721)
		{
			return "不存在宠物";
		}
		if (status == 722)
		{
			return "出生动画播放中";
		}
		if (status == 723)
		{
			return "士兵未死亡";
		}
		if (status == 724)
		{
			return "属主士兵死亡";
		}
		if (status == 725)
		{
			return "士兵冲锋中";
		}
		if (status == 726)
		{
			return "士兵在击飞中";
		}
		if (status == 727)
		{
			return "士兵已悬空";
		}
		if (status == 728)
		{
			return "士兵不在击飞中";
		}
		if (status == 729)
		{
			return "士兵不是击飞托管者";
		}
		if (status == 730)
		{
			return "战场人数限制";
		}
		if (status == 731)
		{
			return "战场怪物数量限制";
		}
		if (status == 732)
		{
			return "不能发现托管者";
		}
		if (status == 733)
		{
			return "滤镜未发现";
		}
		if (status == 734)
		{
			return "士兵在被技能管理中";
		}
		if (status == 735)
		{
			return "士兵未被技能管理";
		}
		if (status == 736)
		{
			return "士兵不是技能托管者";
		}
		if (status == 737)
		{
			return "重复添加技能";
		}
		if (status == 738)
		{
			return "士兵战斗中";
		}
		if (status == 739)
		{
			return "士兵未出战";
		}
		if (status == 740)
		{
			return "战场未发现";
		}
		if (status == 741)
		{
			return "士兵不在加载状态";
		}
		if (status == 742)
		{
			return "正在战斗中";
		}
		if (status == 743)
		{
			return "技能不允许长按";
		}
		if (status == 744)
		{
			return "在技能按压中";
		}
		if (status == 745)
		{
			return "不在技能按压中";
		}
		if (status == 746)
		{
			return "战场已经暂停";
		}
		if (status == 747)
		{
			return "战场加载中";
		}
		if (status == 748)
		{
			return "宠物不可召唤";
		}
		if (status == 749)
		{
			return "需要冲锋";
		}
		if (status == 750)
		{
			return "技能不允许取消";
		}
		if (status == 751)
		{
			return "不允许复活";
		}
		if (status == 752)
		{
			return "士兵在晕眩中";
		}
		if (status == 753)
		{
			return "士兵不在晕眩中";
		}
		if (status == 754)
		{
			return "不在当前技能";
		}
		if (status == 755)
		{
			return "非法投递者";
		}
		if (status == 756)
		{
			return "效应Uid未找到";
		}
		if (status == 757)
		{
			return "动作优先级";
		}
		if (status == 758)
		{
			return "效应miss";
		}
		if (status == 759)
		{
			return "受击状体中不能被客户端打断";
		}
		if (status == 760)
		{
			return "托管最大顶替次数";
		}
		if (status == 761)
		{
			return "托管序号";
		}
		if (status == 762)
		{
			return "竞技场角色未找到";
		}
		if (status == 763)
		{
			return "半端本结算";
		}
		if (status == 764)
		{
			return "战场已存在";
		}
		if (status == 780)
		{
			return "士兵在疲劳中";
		}
		if (status == 781)
		{
			return "士兵不在疲劳中";
		}
		if (status == 800)
		{
			return "装着的装备不能用于进阶消耗";
		}
		if (status == 801)
		{
			return "装备职业不匹配";
		}
		if (status == 802)
		{
			return "装备类型错误";
		}
		if (status == 803)
		{
			return "人物等级不足不可以强化";
		}
		if (status == 804)
		{
			return "已经强化到最大等级";
		}
		if (status == 805)
		{
			return "装备不存在";
		}
		if (status == 806)
		{
			return "等级不足不可以进阶";
		}
		if (status == 807)
		{
			return "装备已是最高阶";
		}
		if (status == 808)
		{
			return "武器库空间不足";
		}
		if (status == 809)
		{
			return "上衣库空间不足";
		}
		if (status == 810)
		{
			return "裤子库空间不足";
		}
		if (status == 811)
		{
			return "鞋子库空间不足";
		}
		if (status == 812)
		{
			return "腰坠库空间不足";
		}
		if (status == 813)
		{
			return "项链库空间不足";
		}
		if (status == 814)
		{
			return "进阶配置数据错误";
		}
		if (status == 815)
		{
			return "穿戴装备等级不足";
		}
		if (status == 816)
		{
			return "强化石数量不足";
		}
		if (status == 817)
		{
			return "当前装备不可以洗炼";
		}
		if (status == 818)
		{
			return "同一件装备只能对同一位置的属性进行洗炼";
		}
		if (status == 819)
		{
			return "洗炼材料不足";
		}
		if (status == 820)
		{
			return "洗炼属性的位置不合法";
		}
		if (status == 821)
		{
			return "当前没有进行洗炼操作";
		}
		if (status == 822)
		{
			return "已经达到最大星级";
		}
		if (status == 823)
		{
			return "不是升星所需的材料";
		}
		if (status == 824)
		{
			return "升星材料不足";
		}
		if (status == 825)
		{
			return "装备等级不足以升到该星级";
		}
		if (status == 826)
		{
			return "该装备没有升星";
		}
		if (status == 827)
		{
			return "装备星级重置材料不足";
		}
		if (status == 828)
		{
			return "当前装备不可以附魔";
		}
		if (status == 829)
		{
			return "装备附魔材料不足";
		}
		if (status == 830)
		{
			return "该部位不可以使用此附魔符";
		}
		if (status == 831)
		{
			return "装备附魔位置出错";
		}
		if (status == 832)
		{
			return "装备不同位置不可以附魔相同的属性";
		}
		if (status == 833)
		{
			return "当前没有附魔操作";
		}
		if (status == 834)
		{
			return "不能分解穿着的装备";
		}
		if (status == 835)
		{
			return "已达装备配置强化等级上限";
		}
		if (status == 836)
		{
			return "该装备不可升星";
		}
		if (status == 837)
		{
			return "装备阶级不足";
		}
		if (status == 838)
		{
			return "不能熔炼穿着的装备";
		}
		if (status == 839)
		{
			return "每日熔炼贡献资金达到上限";
		}
		if (status == 840)
		{
			return "装备品质不足";
		}
		if (status == 841)
		{
			return "转职后才能装备";
		}
		if (status == 842)
		{
			return "装备没有穿戴";
		}
		if (status == 843)
		{
			return "装备已是套装";
		}
		if (status == 845)
		{
			return "该装备不可锻造套装";
		}
		if (status == 900)
		{
			return "任务不存在";
		}
		if (status == 901)
		{
			return "无效的参数";
		}
		if (status == 902)
		{
			return "任务尚未完成";
		}
		if (status == 903)
		{
			return "活跃度不够";
		}
		if (status == 904)
		{
			return "已经领取过体力赠送了";
		}
		if (status == 905)
		{
			return "不在领取体力赠送时间范围内";
		}
		if (status == 906)
		{
			return "木有该日常任务";
		}
		if (status == 907)
		{
			return "日常任务次数未完成";
		}
		if (status == 908)
		{
			return "已经过了领取体力赠送的时候啦";
		}
		if (status == 909)
		{
			return "奖励已领取";
		}
		if (status == 910)
		{
			return "已接取了任务";
		}
		if (status == 911)
		{
			return "角色已达到该职业等级上限, 请先转职";
		}
		if (status == 918)
		{
			return "任务次数已用完";
		}
		if (status == 912)
		{
			return "可找回次数已用完";
		}
		if (status == 913)
		{
			return "可找回次数不足";
		}
		if (status == 914)
		{
			return "神器任务未完成";
		}
		if (status == 915)
		{
			return "任务不能接取";
		}
		if (status == 916)
		{
			return "任务完成次数不达标";
		}
		if (status == 917)
		{
			return "刷新次数已用完";
		}
		if (status == 918)
		{
			return "当前所有任务已被接取，无法刷新";
		}
		if (status == 919)
		{
			return "当前任务组已接取";
		}
		if (status == 950)
		{
			return "未进入PVP战场";
		}
		if (status == 951)
		{
			return "活动已关闭";
		}
		if (status == 1000)
		{
			return "没有获得这个称号";
		}
		if (status == 1001)
		{
			return "当前装备了该称号";
		}
		if (status == 1002)
		{
			return "称号还在有效期";
		}
		if (status == 1100)
		{
			return "已经是好友关系";
		}
		if (status == 1101)
		{
			return "存在于黑名单中";
		}
		if (status == 1102)
		{
			return "非好友关系";
		}
		if (status == 1103)
		{
			return "不能加自己为好友";
		}
		if (status == 1104)
		{
			return "你的好友已经达到上限";
		}
		if (status == 1105)
		{
			return "对方好友已经达到上限";
		}
		if (status == 1106)
		{
			return "你已被对方列为黑名单";
		}
		if (status == 1107)
		{
			return "对方申请列表已满，无法申请";
		}
		if (status == 1200)
		{
			return "发言过快,请休息一下";
		}
		if (status == 1201)
		{
			return "禁言中";
		}
		if (status == 1202)
		{
			return "时间过的太久已被删除";
		}
		if (status == 13000)
		{
			return "邮件数据异常";
		}
		if (status == 13001)
		{
			return "找不到此邮件";
		}
		if (status == 13002)
		{
			return "邮件附件为空";
		}
		if (status == 13003)
		{
			return "邮件附件已被领取";
		}
		if (status == 1500)
		{
			return "炼体等级不足";
		}
		if (status == 1501)
		{
			return "炼体值不足，请选择道具补充炼体值";
		}
		if (status == 1502)
		{
			return "炼体金币不足";
		}
		if (status == 1503)
		{
			return "人物炼体系统配置不存在";
		}
		if (status == 1504)
		{
			return "点亮顺序配置不存在";
		}
		if (status == 1505)
		{
			return "点亮步数配置不存在";
		}
		if (status == 1506)
		{
			return "炼体属性配置不存在";
		}
		if (status == 1507)
		{
			return "选择的道具无炼体值加成";
		}
		if (status == 1601)
		{
			return "未到竞技场开放等级";
		}
		if (status == 1602)
		{
			return "挑战CD中";
		}
		if (status == 1603)
		{
			return "战斗中无法取消";
		}
		if (status == 1604)
		{
			return "已经报名竞技场了";
		}
		if (status == 1700)
		{
			return "体力值已满";
		}
		if (status == 1701)
		{
			return "体力购买次数上限";
		}
		if (status == 1800)
		{
			return "已经报名大乱斗了";
		}
		if (status == 1801)
		{
			return "没达到大乱斗开放等级";
		}
		if (status == 1802)
		{
			return "没达开启时间";
		}
		if (status == 1803)
		{
			return "正在战斗中";
		}
		if (status == 1851)
		{
			return "野外boss不存在";
		}
		if (status == 1852)
		{
			return "该玩家没有掉落";
		}
		if (status == 1853)
		{
			return "玩家挑战等级不符合";
		}
		if (status == 1854)
		{
			return "boss被挑战中";
		}
		if (status == 1855)
		{
			return "角色没有挑战野外boss";
		}
		if (status == 1856)
		{
			return "邀请cd中";
		}
		if (status == 1857)
		{
			return "没有队伍";
		}
		if (status == 1858)
		{
			return "玩家最低等级限制";
		}
		if (status == 1859)
		{
			return "已达到挑战人数上限";
		}
		if (status == 1860)
		{
			return "不在排队队列";
		}
		if (status == 1861)
		{
			return "已经在排队队列中";
		}
		if (status == 1862)
		{
			return "今日击杀BOSS收益已达上限，可前往其他打宝地图";
		}
		if (status == 1900)
		{
			return "你已经拥有军团了";
		}
		if (status == 1901)
		{
			return "军团达最大等级";
		}
		if (status == 1902)
		{
			return "军团名称已经存在";
		}
		if (status == 1903)
		{
			return "搜索名称不能为空";
		}
		if (status == 1904)
		{
			return "还没有加入军团";
		}
		if (status == 1905)
		{
			return "不能超过军团平均战斗力";
		}
		if (status == 1906)
		{
			return "已经申请过该军团";
		}
		if (status == 1907)
		{
			return "军团不存在";
		}
		if (status == 1908)
		{
			return "刚退出军团还在CD中";
		}
		if (status == 1909)
		{
			return "该军团已满员";
		}
		if (status == 1910)
		{
			return "军团不招募";
		}
		if (status == 1911)
		{
			return "只有团长才能同意别人加入军团";
		}
		if (status == 1912)
		{
			return "该玩家没有申请该军团";
		}
		if (status == 1913)
		{
			return "成员不在此军团中";
		}
		if (status == 1914)
		{
			return "只有团长才能踢人";
		}
		if (status == 1915)
		{
			return "只有团长才能邀请别人";
		}
		if (status == 1916)
		{
			return "只有团长才能拒绝别人申请";
		}
		if (status == 1917)
		{
			return "还没有申请过该军团";
		}
		if (status == 1918)
		{
			return "最多申请5个军团";
		}
		if (status == 1919)
		{
			return "已经邀请过该玩家了";
		}
		if (status == 1920)
		{
			return "不能踢军团团长";
		}
		if (status == 1921)
		{
			return "玩家不在线";
		}
		if (status == 1922)
		{
			return "该玩家已经是军团成员了";
		}
		if (status == 1923)
		{
			return "该玩家已经加入其他军团";
		}
		if (status == 1924)
		{
			return "权限不足";
		}
		if (status == 1925)
		{
			return "该请求已经被处理";
		}
		if (status == 1926)
		{
			return "团长解散军团CD    (与客户端约定,不能改)";
		}
		if (status == 1927)
		{
			return "自己申请退出团长CD (与客户端约定,不能改)";
		}
		if (status == 1928)
		{
			return "军团名长度不合法";
		}
		if (status == 1929)
		{
			return "军团公告长度不合法";
		}
		if (status == 1930)
		{
			return "军团资金不足";
		}
		if (status == 1931)
		{
			return "没有此头衔";
		}
		if (status == 1932)
		{
			return "已由其他管理员处理";
		}
		if (status == 1933)
		{
			return "客户端输入参数非法";
		}
		if (status == 1934)
		{
			return "该职位人数已满";
		}
		if (status == 1935)
		{
			return "系统解散军团CD (与客户端约定,不能改)";
		}
		if (status == 1936)
		{
			return "被踢出军团CD (与客户端约定,不能改)";
		}
		if (status == 1937)
		{
			return "已达到今日建设次数上限";
		}
		if (status == 1938)
		{
			return "军团任务已接";
		}
		if (status == 1939)
		{
			return "已达今天召唤次数上限";
		}
		if (status == 1940)
		{
			return "BOSS存活";
		}
		if (status == 1941)
		{
			return "挑战BOSS cd 中";
		}
		if (status == 1942)
		{
			return "挑战BOSS 结束, 正在刷新";
		}
		if (status == 1943)
		{
			return "已达清CD上限";
		}
		if (status == 1944)
		{
			return "活动正在结算";
		}
		if (status == 1945)
		{
			return "军团装备精髓不足";
		}
		if (status == 1946)
		{
			return "军团战期间,不能解散军团";
		}
		if (status == 1947)
		{
			return "已达到今日做任务次数上限";
		}
		if (status == 1948)
		{
			return "该玩家还没有加入军团";
		}
		if (status == 1949)
		{
			return "信息获取失败，玩家已下线";
		}
		if (status == 2000)
		{
			return "当天金币购买次数已满,不能再继续购买";
		}
		if (status == 2100)
		{
			return "邀请时间正在冷却";
		}
		if (status == 2101)
		{
			return "队伍已经解散";
		}
		if (status == 2102)
		{
			return "非法请求";
		}
		if (status == 2103)
		{
			return "已加入队伍,请先离开";
		}
		if (status == 2105)
		{
			return "队伍已经存在";
		}
		if (status == 2106)
		{
			return "无人可邀请";
		}
		if (status == 2107)
		{
			return "剩余次数为0";
		}
		if (status == 2108)
		{
			return "没到开启时间";
		}
		if (status == 2109)
		{
			return "队伍已满员";
		}
		if (status == 2110)
		{
			return "该队伍正在战斗中, 不可加入";
		}
		if (status == 2111)
		{
			return "队伍已不存在, 不可加入";
		}
		if (status == 2112)
		{
			return "匹配CD中";
		}
		if (status == 2113)
		{
			return "快速进入CD";
		}
		if (status == 2114)
		{
			return "战场加载失败";
		}
		if (status == 2115)
		{
			return "加载中";
		}
		if (status == 2200)
		{
			return "正在进行组队";
		}
		if (status == 2201)
		{
			return "队伍已满";
		}
		if (status == 2202)
		{
			return "符合进入此副本的人数不足3人 --";
		}
		if (status == 2203)
		{
			return "当前存在队伍, 请先退出";
		}
		if (status == 2204)
		{
			return "队伍不存在";
		}
		if (status == 2205)
		{
			return "当前成员存在队伍中";
		}
		if (status == 2206)
		{
			return "权限不足";
		}
		if (status == 2207)
		{
			return "该玩家不是队列成员";
		}
		if (status == 2208)
		{
			return "对方已经加入其他队伍";
		}
		if (status == 2209)
		{
			return "对方已经加入队伍";
		}
		if (status == 2210)
		{
			return "等级不符合";
		}
		if (status == 2211)
		{
			return "邀请CD中";
		}
		if (status == 2212)
		{
			return "队伍已解散";
		}
		if (status == 2213)
		{
			return "队长已下线";
		}
		if (status == 2214)
		{
			return "无人可邀请";
		}
		if (status == 2215)
		{
			return "没被邀请";
		}
		if (status == 2216)
		{
			return "队伍已经开战";
		}
		if (status == 2217)
		{
			return "处于快速请求状态";
		}
		if (status == 2218)
		{
			return "无效操作";
		}
		if (status == 2219)
		{
			return "必须先退出队伍";
		}
		if (status == 2220)
		{
			return "队伍人数不足";
		}
		if (status == 2221)
		{
			return "已经回复过";
		}
		if (status == 2222)
		{
			return "不是队长";
		}
		if (status == 2223)
		{
			return "队伍状态不符合";
		}
		if (status == 2224)
		{
			return "队员拒绝挑战";
		}
		if (status == 2225)
		{
			return "有队员正在战斗中";
		}
		if (status == 2226)
		{
			return "快速入队CD中";
		}
		if (status == 2227)
		{
			return "当前没有合适队伍，您可创建队伍";
		}
		if (status == 2228)
		{
			return "组队的目标与此不一，请先退出";
		}
		if (status == 2300)
		{
			return "元素升级条件不足";
		}
		if (status == 2301)
		{
			return "元素到达最高等级";
		}
		if (status == 2302)
		{
			return "没有此元素";
		}
		if (status == 2303)
		{
			return "背包没有所需消耗的物品";
		}
		if (status == 2304)
		{
			return "所需消耗的物品数量不足";
		}
		if (status == 2305)
		{
			return "元素升级条件不足";
		}
		if (status == 2404)
		{
			return "重复开启Vip效果";
		}
		if (status == 2405)
		{
			return "Vip效果未找到";
		}
		if (status == 2406)
		{
			return "不是开启盒子类型Vip效果";
		}
		if (status == 2407)
		{
			return "Vip效果未开启";
		}
		if (status == 2408)
		{
			return "不允许重复打开礼盒类型Vip效果";
		}
		if (status == 2409)
		{
			return "未达到购买要求";
		}
		if (status == 2501)
		{
			return "没有此商店";
		}
		if (status == 2502)
		{
			return "商店没有开启";
		}
		if (status == 2503)
		{
			return "商店不支持消费刷新";
		}
		if (status == 2504)
		{
			return "商店不支持系统刷新";
		}
		if (status == 2505)
		{
			return "刷新费用不足";
		}
		if (status == 2506)
		{
			return "商品配置表不存在该商品";
		}
		if (status == 2507)
		{
			return "货币不足";
		}
		if (status == 2508)
		{
			return "商店Vip等级限制";
		}
		if (status == 2509)
		{
			return "商店刷新次数已用完";
		}
		if (status == 2510)
		{
			return "军团贡献不足";
		}
		if (status == 2511)
		{
			return "竞技币不足";
		}
		if (status == 2512)
		{
			return "竞技段位限制";
		}
		if (status == 2513)
		{
			return "商品已卖";
		}
		if (status == 2514)
		{
			return "没有此商品";
		}
		if (status == 2515)
		{
			return "商品数量不足";
		}
		if (status == 2516)
		{
			return "军团荣誉不足";
		}
		if (status == 2517)
		{
			return "所需交换的物品不足";
		}
		if (status == 2518)
		{
			return "商品售罄";
		}
		if (status == 2519)
		{
			return "声望不足";
		}
		if (status == 2600)
		{
			return "不在准备大厅";
		}
		if (status == 2601)
		{
			return "已经报名军团战了";
		}
		if (status == 2602)
		{
			return "没有报名军团战";
		}
		if (status == 2603)
		{
			return "准备大厅尚未开启";
		}
		if (status == 2604)
		{
			return "准备大厅没有找到";
		}
		if (status == 2605)
		{
			return "战场没有找到";
		}
		if (status == 2606)
		{
			return "战场没有找到";
		}
		if (status == 2607)
		{
			return "复活中";
		}
		if (status == 2700)
		{
			return "不存在此宝箱";
		}
		if (status == 2701)
		{
			return "未达到指定星数 宝箱未开启";
		}
		if (status == 2702)
		{
			return "宝箱已领取过了";
		}
		if (status == 2703)
		{
			return "宝箱没有物品";
		}
		if (status == 2704)
		{
			return "没有此掉落规则";
		}
		if (status == 2705)
		{
			return "掉落规则Id为空";
		}
		if (status == 2800)
		{
			return "剩余挑战次数不足";
		}
		if (status == 2801)
		{
			return "不能挑战该难度";
		}
		if (status == 2900)
		{
			return "该开服奖励不存在";
		}
		if (status == 2901)
		{
			return "签到次数不足,不能领取该奖励";
		}
		if (status == 2902)
		{
			return "已经领取过该奖励";
		}
		if (status == 2903)
		{
			return "当天已经签到过了";
		}
		if (status == 2904)
		{
			return "当月补签次数已用完";
		}
		if (status == 2905)
		{
			return "没有该签到配置数据";
		}
		if (status == 2906)
		{
			return "明天再来领取";
		}
		if (status == 2907)
		{
			return "已经领取过奖励了";
		}
		if (status == 3000)
		{
			return "没获取此技能";
		}
		if (status == 3001)
		{
			return "技能达到最大等级";
		}
		if (status == 3002)
		{
			return "技能升级条件不足";
		}
		if (status == 3003)
		{
			return "技能已解锁";
		}
		if (status == 3004)
		{
			return "技能不满足解锁需求";
		}
		if (status == 3005)
		{
			return "同一个技能";
		}
		if (status == 3006)
		{
			return "主角等级不足";
		}
		if (status == 3007)
		{
			return "尚未获得解锁所需宠物";
		}
		if (status == 3008)
		{
			return "VIP等级不足未开启";
		}
		if (status == 3009)
		{
			return "技能配置已解锁";
		}
		if (status == 3010)
		{
			return "所需消耗材料不足";
		}
		if (status == 3011)
		{
			return "没有获得此技能配置";
		}
		if (status == 3012)
		{
			return "只能装备卓越属性去升级";
		}
		if (status == 3100)
		{
			return "没达到收藏等级";
		}
		if (status == 3101)
		{
			return "客户端输入参数错误";
		}
		if (status == 3102)
		{
			return "该宝石不能装入此位置";
		}
		if (status == 3103)
		{
			return "同一装备部位上不能镶嵌同色宝石";
		}
		if (status == 3104)
		{
			return "宝石不存在";
		}
		if (status == 3105)
		{
			return "已达到最大等级";
		}
		if (status == 3106)
		{
			return "升级的宝石数量不足";
		}
		if (status == 3110)
		{
			return "符石孔没开启";
		}
		if (status == 3111)
		{
			return "已达最高等级";
		}
		if (status == 3112)
		{
			return "升级材料不足";
		}
		if (status == 3113)
		{
			return "符石组为空";
		}
		if (status == 3200)
		{
			return "您的奖卷不足";
		}
		if (status == 3201)
		{
			return "主角等级不足未开启";
		}
		if (status == 3202)
		{
			return "没有此抽奖";
		}
		if (status == 3300)
		{
			return "没到开放时间";
		}
		if (status == 3301)
		{
			return "上一个挑战没处理完";
		}
		if (status == 3302)
		{
			return "请先选择挑战的副本";
		}
		if (status == 3303)
		{
			return "已经使用过道具";
		}
		if (status == 3304)
		{
			return "不适合的道具";
		}
		if (status == 3305)
		{
			return "已经达到最大购买次数";
		}
		if (status == 3306)
		{
			return "挑战次数为0";
		}
		if (status == 3307)
		{
			return "已匹配成功，正在前往任务区域";
		}
		if (status == 3308)
		{
			return "不允许的操作";
		}
		if (status == 3309)
		{
			return "对方玩家退出";
		}
		if (status == 3310)
		{
			return "创建战场失败";
		}
		if (status == 3311)
		{
			return "敌对玩家数量不足";
		}
		if (status == 3400)
		{
			return "不是相邻板块";
		}
		if (status == 3401)
		{
			return "该板块未激活";
		}
		if (status == 3402)
		{
			return "探索能量已经用完";
		}
		if (status == 3403)
		{
			return "当天购买次数已经达到上限";
		}
		if (status == 3404)
		{
			return "没有碎片可以领取";
		}
		if (status == 3405)
		{
			return "当天没有该矿点";
		}
		if (status == 3406)
		{
			return "该矿点已经有宠物驻扎";
		}
		if (status == 3407)
		{
			return "该宠物已经驻扎在其他矿点";
		}
		if (status == 3408)
		{
			return "找不到这只宠物";
		}
		if (status == 3409)
		{
			return "该矿点还没有被占领";
		}
		if (status == 3410)
		{
			return "板块不是可挑战板块";
		}
		if (status == 3411)
		{
			return "此板块已经挑战过";
		}
		if (status == 3412)
		{
			return "没有移动到下一板块";
		}
		if (status == 3500)
		{
			return "没有此类型月卡";
		}
		if (status == 3501)
		{
			return "此类型月卡已购买，不可叠加";
		}
		if (status == 3600)
		{
			return "没有此活动";
		}
		if (status == 3601)
		{
			return "已经不是第一次打开";
		}
		if (status == 3602)
		{
			return "未达到奖励要求";
		}
		if (status == 3603)
		{
			return "活动已参加";
		}
		if (status == 3604)
		{
			return "活动已关闭";
		}
		if (status == 3704)
		{
			return "已经购买过此活动";
		}
		if (status == 3705)
		{
			return "未购买此活动";
		}
		if (status == 3700)
		{
			return "不是当前的成就";
		}
		if (status == 3701)
		{
			return "成就没有完成";
		}
		if (status == 3702)
		{
			return "成就配置数据错误";
		}
		if (status == 3703)
		{
			return "已经完成该类型的所有成就";
		}
		if (status == 3800)
		{
			return "没达开启时间";
		}
		if (status == 3801)
		{
			return "队伍已经存在";
		}
		if (status == 3802)
		{
			return "此任务关闭";
		}
		if (status == 3803)
		{
			return "不存在的宝箱";
		}
		if (status == 3804)
		{
			return "开箱子星星数不足";
		}
		if (status == 3805)
		{
			return "不能刷新紧急任务";
		}
		if (status == 3806)
		{
			return "没有可挑战的副本";
		}
		if (status == 3807)
		{
			return "没能匹配到玩家";
		}
		if (status == 3808)
		{
			return "正在前往执行任务中";
		}
		if (status == 3809)
		{
			return "此星级宝箱已经领取过";
		}
		if (status == 3810)
		{
			return "等级范围内没玩家";
		}
		if (status == 3811)
		{
			return "积分范围内没玩家";
		}
		if (status == 3812)
		{
			return "已达最大等级";
		}
		if (status == 3813)
		{
			return "时间冷却中";
		}
		if (status == 3814)
		{
			return "生产队列已满";
		}
		if (status == 3815)
		{
			return "没此任务";
		}
		if (status == 3816)
		{
			return "此任务已参与过,等下次开启吧";
		}
		if (status == 3900)
		{
			return "等级不足以挑战生存秘境";
		}
		if (status == 3901)
		{
			return "生存秘境挑战次数不足";
		}
		if (status == 3902)
		{
			return "之前没有进行生存秘境挑战";
		}
		if (status == 3903)
		{
			return "购买生存秘境挑战次数已达到上限";
		}
		if (status == 4000)
		{
			return "翅膀已经拥有";
		}
		if (status == 4001)
		{
			return "翅膀不存在";
		}
		if (status == 4002)
		{
			return "翅膀达到最大等级";
		}
		if (status == 4003)
		{
			return "激活翅膀才能查看";
		}
		if (status == 4100)
		{
			return "此转职任务已接取过";
		}
		if (status == 4101)
		{
			return "条件不足以开启转职系统";
		}
		if (status == 4102)
		{
			return "本职业不能转此职业";
		}
		if (status == 4103)
		{
			return "还没完成相应转职任务";
		}
		if (status == 4104)
		{
			return "没有接取到相关的转职任务";
		}
		if (status == 4200)
		{
			return "更新有礼活动未接取";
		}
		if (status == 4201)
		{
			return "活动已完成";
		}
		if (status == 4202)
		{
			return "活动未开始";
		}
		if (status == 4203)
		{
			return "活动进行中";
		}
		if (status == 4204)
		{
			return "活动已关闭";
		}
		if (status == 4300)
		{
			return "已经拥有该天赋";
		}
		if (status == 4301)
		{
			return "该天赋不存在";
		}
		if (status == 4302)
		{
			return "该天赋达到最大等级";
		}
		if (status == 4303)
		{
			return "职业不匹配";
		}
		if (status == 4304)
		{
			return "前置条件不足";
		}
		if (status == 4305)
		{
			return "天赋点不足";
		}
		if (status == 4306)
		{
			return "转职才能开放天赋系统";
		}
		if (status == 4307)
		{
			return "前置天赋等级不足";
		}
		if (status == 4400)
		{
			return "不存在的商品";
		}
		if (status == 4401)
		{
			return "商品已过期";
		}
		if (status == 4402)
		{
			return "已售完";
		}
		if (status == 4403)
		{
			return "没数据";
		}
		if (status == 4404)
		{
			return "活动结束";
		}
		if (status == 4500)
		{
			return "活动任务未完成";
		}
		if (status == 4501)
		{
			return "活动奖励已领取";
		}
		if (status == 4600)
		{
			return "翻牌游戏挑战中";
		}
		if (status == 4601)
		{
			return "翻牌游戏不在挑战中";
		}
		if (status == 4602)
		{
			return "翻牌游戏";
		}
		if (status == 4603)
		{
			return "今日拓展次数上限";
		}
		if (status == 4604)
		{
			return "还有次数，不能拓展";
		}
		if (status == 4605)
		{
			return "没有挑战次数";
		}
		if (status == 4610)
		{
			return "挑战次数达到上限";
		}
		if (status == 4611)
		{
			return "拓展次数上限";
		}
		if (status == 4612)
		{
			return "购买buf参数有误";
		}
		if (status == 4620)
		{
			return "购买次数已用完";
		}
		if (status == 4629)
		{
			return "库存不足";
		}
		if (status == 4630)
		{
			return "游戏次数已用完";
		}
		if (status == 4631)
		{
			return "没有参加游戏";
		}
		if (status == 4632)
		{
			return "游戏失败";
		}
		if (status == 4633)
		{
			return "系统未开启";
		}
		if (status == 4640)
		{
			return "已经在军团领地中";
		}
		if (status == 4641)
		{
			return "不在军团领地中";
		}
		if (status == 4642)
		{
			return "领地未开启";
		}
		if (status == 4643)
		{
			return "不在猎场中";
		}
		if (status == 4644)
		{
			return "vip权限不足";
		}
		if (status == 4645)
		{
			return "并非vip猎场";
		}
		if (status == 4646)
		{
			return "特殊领地传送错误";
		}
		if (status == 4647)
		{
			return "已经在帮会战领地中";
		}
		if (status == 4660)
		{
			return "活动没有开启";
		}
		if (status == 4661)
		{
			return "活动未完成";
		}
		if (status == 4662)
		{
			return "活动已领奖";
		}
		if (status == 4670)
		{
			return "没有此配置数据";
		}
		if (status == 4671)
		{
			return "已追踪";
		}
		if (status == 4672)
		{
			return "没有追踪";
		}
		if (status == 4690)
		{
			return "已经在军团boss副本中";
		}
		if (status == 4691)
		{
			return "不在军团boss挑战中";
		}
		if (status == 4692)
		{
			return "军团boss不能重复开启";
		}
		if (status == 4693)
		{
			return "军团boss没创建";
		}
		if (status == 4710)
		{
			return "时装不存在";
		}
		if (status == 4711)
		{
			return "时装未获得";
		}
		if (status == 4712)
		{
			return "职业不匹配";
		}
		if (status == 4713)
		{
			return "时装已拥有";
		}
		if (status == 4714)
		{
			return "暂时不可购买";
		}
		if (status == 4715)
		{
			return "激活翅膀后才可穿戴幻翼";
		}
		if (status == 4730)
		{
			return "军团战参数异常";
		}
		if (status == 4731)
		{
			return "不在军团战战场中";
		}
		if (status == 4732)
		{
			return "军团战战场不存在";
		}
		if (status == 4733)
		{
			return "军团战房间已创建";
		}
		if (status == 4734)
		{
			return "不在军团战领地中";
		}
		if (status == 4735)
		{
			return "当前没有对战";
		}
		if (status == 4736)
		{
			return "军队战战场人数限制";
		}
		if (status == 4750)
		{
			return "已经领取过奖励";
		}
		if (status == 4751)
		{
			return "未到领奖日";
		}
		if (status == 4752)
		{
			return "没有周冠军";
		}
		if (status == 4753)
		{
			return "不在领取时间";
		}
		if (status == 4754)
		{
			return "不在福利时间";
		}
		if (status == 4755)
		{
			return "复活时间已过期";
		}
		if (status == 4756)
		{
			return "在军团中死亡";
		}
		if (status == 4757)
		{
			return "该军团没在参赛中";
		}
		if (status == 4758)
		{
			return "您的军团因未匹配到对手，已成功晋级，此轮无需参战";
		}
		if (status == 4759)
		{
			return "加入军团时间限制";
		}
		if (status == 4760)
		{
			return "非第一军团成员";
		}
		if (status == 4761)
		{
			return "时间没到, 请静候结果";
		}
		if (status == 4810)
		{
			return "不能进行精英副本";
		}
		if (status == 4811)
		{
			return "任务未完成";
		}
		if (status == 4812)
		{
			return "快速进入CD中";
		}
		if (status == 4813)
		{
			return "询问CD";
		}
		if (status == 4814)
		{
			return "正在前往战场";
		}
		if (status == 4815)
		{
			return "可领奖次数达为0, 不能作为队长";
		}
		if (status == 4816)
		{
			return "非首次挑战";
		}
		if (status == 4817)
		{
			return "上个副本没结束";
		}
		if (status == 4818)
		{
			return "您的精英副本此难度未开启";
		}
		if (status == 4900)
		{
			return "不可重复购买";
		}
		if (status == 4901)
		{
			return "等级不足不能购买";
		}
		if (status == 4902)
		{
			return "奖励不存在";
		}
		if (status == 4903)
		{
			return "请先购买";
		}
		if (status == 4904)
		{
			return "已过期";
		}
		if (status == 4905)
		{
			return "奖励已领过";
		}
		if (status == 4906)
		{
			return "请明天再来领取";
		}
		if (status == 4907)
		{
			return "请先用完离线时间";
		}
		if (status == 4911)
		{
			return "挂机房间不存在";
		}
		if (status == 4912)
		{
			return "房间人数限制";
		}
		if (status == 4913)
		{
			return "已经在该房间中";
		}
		if (status == 4914)
		{
			return "玩家不在挂机房间中";
		}
		if (status == 4915)
		{
			return "该狩猎点不可切换战斗模式";
		}
		if (status == 4916)
		{
			return "不合法的操作";
		}
		if (status == 4917)
		{
			return "挂机时间不足";
		}
		if (status == 4918)
		{
			return "购买挂机次数已用完";
		}
		if (status == 4919)
		{
			return "挂机时间累积上限";
		}
		if (status == 4920)
		{
			return "挂机背包已满";
		}
		if (status == 4921)
		{
			return "挂机背包无法添加道具";
		}
		if (status == 4922)
		{
			return "刷新间隔 请稍候再试";
		}
		if (status == 4950)
		{
			return "活动结束后才能领取";
		}
		if (status == 4951)
		{
			return "任务未达成";
		}
		if (status == 4952)
		{
			return "活动已结束";
		}
		if (status == 4970)
		{
			return "没有此技能";
		}
		if (status == 4971)
		{
			return "已经达到最高级";
		}
		if (status == 4972)
		{
			return "提升公会等级才能继续升级";
		}
		if (status == 4973)
		{
			return "军团荣誉不足";
		}
		if (status == 4990)
		{
			return "游戏劵不足";
		}
		if (status == 4991)
		{
			return "商品数量不足";
		}
		if (status == 4992)
		{
			return "不需要更换商品";
		}
		if (status == 5000)
		{
			return "红包已过期";
		}
		if (status == 5001)
		{
			return "很遗憾,红包获取失败";
		}
		if (status == 5010)
		{
			return "奖励已领取";
		}
		if (status == 5011)
		{
			return "奖励未达到领取条件";
		}
		if (status == 5020)
		{
			return "有成员正在匹配中";
		}
		if (status == 5050)
		{
			return "今日护送次数已用完";
		}
		if (status == 5051)
		{
			return "正在护送中";
		}
		if (status == 5052)
		{
			return "地图不存在";
		}
		if (status == 5053)
		{
			return "此护送已结束";
		}
		if (status == 5054)
		{
			return "此矿车正在被抢夺";
		}
		if (status == 5055)
		{
			return "此矿车已被洗劫一空，没有可抢夺的资源";
		}
		if (status == 5056)
		{
			return "您处于抢夺CD中，请稍后再来";
		}
		if (status == 5057)
		{
			return "您本日可抢夺次数已用完，请明日再来";
		}
		if (status == 5058)
		{
			return "好友队伍已满";
		}
		if (status == 5059)
		{
			return "您的好友今日协助护送次数已用完";
		}
		if (status == 5060)
		{
			return "您的好友被邀请中";
		}
		if (status == 5061)
		{
			return "护送已开始";
		}
		if (status == 5062)
		{
			return "您当前刷新券道具不足";
		}
		if (status == 5063)
		{
			return "矿车已是极品";
		}
		if (status == 5064)
		{
			return "您邀请的好友的等级不匹配";
		}
		if (status == 5065)
		{
			return "刷新冷却";
		}
		if (status == 5066)
		{
			return "护矿接近尾声，无法抢夺";
		}
		if (status == 5067)
		{
			return "您的队伍好友已满";
		}
		if (status == 5100)
		{
			return "此商品已被其他成员兑换";
		}
		if (status == 5101)
		{
			return "军团仓库已满";
		}
		if (status == 5102)
		{
			return "军团仓库不存在";
		}
		if (status == 5103)
		{
			return "装备已经被绑定无法捐献";
		}
		if (status == 5104)
		{
			return "今日兑换次数已用完";
		}
		if (status == 5105)
		{
			return "仓库积分不足";
		}
		if (status == -1)
		{
			return "未知错误";
		}
		return "未知错误, status: " + status;
	}
}
