public class SaveParser : Form, InterfaceWindowDebug
{
    // Fields
    private TextWriter logWriter;
    private Stopwatch stopwatch;
    private Window_Debug windowDebug;
    private Window_Table windowTable;
    private ParserFunctions parserFunctions;
    private Dictionary<string, string> dic_properties;
    private Dictionary<string, string> dic_ModShortPackFile;
    private string saveFileName;
    private int gameTag = -1;
    private string mod;
    private bool debugging;
    private bool usePackFiles;
    private string config_AppDataPath = @"Data\";
    private int savegameFileType;
    private string selectedFactionArrayIndex = "0";
    private List<string> buffer_EditFactions = new List<string>();
    private List<string> buffer_EditRegions = new List<string>();
    private List<string> buffer_EditProvinces = new List<string>();
    private int clickedRowIndex = -1;
    private string[,,] columnAttrib;
    private ToolStripMenuItem[] tsmi_Mods;
    private IContainer components;
    private MenuStrip ms_Main;
    private ToolStripMenuItem mi_File;
    private ToolStripMenuItem tsmi_OpenFile;
    private OpenFileDialog openFileDialog_Savegame;
    private Panel p_Status;
    private Label l_Status_Label;
    private ToolStripMenuItem mi_Game;
    private ToolStripMenuItem tsmi_Rome2;
    private ToolStripMenuItem tsmi_Shogun2;
    private ToolStripMenuItem tsmi_Napoleon;
    private ToolStripMenuItem tsmi_Empire;
    private ToolTip toolTip;
    private ToolStripMenuItem tsmi_SaveFile;
    private ToolStripMenuItem mi_Info;
    private ToolStripMenuItem tsmi_Version;
    private Label l_Status_GameTag;
    private ToolStripMenuItem mi_Tools;
    private ToolStripMenuItem tsmi_EditSF;
    private ToolStripMenuItem mi_Admin;
    private ToolStripMenuItem tsmi_DebugWindow;
    private ToolStripMenuItem tsmi_SaveFileAs;
    private SaveFileDialog saveFileDialog_Savegame;
    private ToolStripMenuItem tsmi_OpenFile_Data;
    private ToolStripMenuItem tsmi_OpenFile_Names;
    private ToolStripMenuItem tsmi_PFM;
    private ToolStripMenuItem tsmi_OpenFile_CustomFile;
    private ContextMenuStrip cms_Table;
    private ToolStripMenuItem tsmi_JumpToEditSF;
    private Label l_Status_Text;
    private ToolStripMenuItem tsmi_ExportTable;
    private ToolStripSeparator tss_File;
    private ToolStripMenuItem tsmi_FileType;
    private ToolStripSeparator tss_Tools_1;
    private ToolStripMenuItem mi_Options;
    private ToolStripSeparator tss_Tools_2;
    private ToolStripMenuItem tsmi_OpenBatchController;
    private ToolStripMenuItem tsmi_Attila;
    private Button b_Reuse;
    private TabPage tabPage_Edit;
    private GroupBox gb_Region;
    private Button b_OpenTableDialog_Regions;
    private Button b_ConstructionComplete;
    private GroupBox gb_Character;
    private Button b_BirthYears;
    private NumericUpDown nup_Age;
    private GroupBox gb_Movement;
    private Button b_ResetAgentAction;
    private Button b_MovementPoints;
    private Label l_MovementPoints;
    private GroupBox gb_Research;
    private Button b_ResearchAddAllProjects;
    private Button b_ResearchComplete;
    private Label l_ResearchComplete;
    private GroupBox gb_ArmyStrength;
    private Button b_Replenish;
    private Button b_Reduce;
    private Label l_UnitChange;
    private GroupBox gb_Parameter;
    private Button b_OpenTableDialog_Factions;
    private GroupBox gb_Global_MoneyDistribution;
    private CheckBox cb_Global_MoneyDistribution_Rebels;
    private Button b_Global_MoneyDistribution;
    private Label l_MoneyDistributionLimit;
    private Label l_MoneyDistribution;
    private NumericUpDown nup_Global_MoneyDistributionLimit;
    private NumericUpDown nup_Global_MoneyDistribution;
    private TabPage tabPage_Parser;
    private DataGridView dgv_ParserResult;
    private Panel p_Control;
    private Button b_FactionArrayIndex;
    private TextBox tb_ParseFaction;
    private ComboBox cb_Factions;
    private Label l_Filter;
    private ComboBox cb_Filter;
    private ComboBox cb_DisplayTable;
    private Label l_DisplayTable;
    private Button b_Parse;
    private TabControl tabControl_Main;
    private ToolStripMenuItem tsmi_SaveViewer;
    private ToolStripMenuItem tsmi_EditSFDialog;
    private ToolStripMenuItem tsmi_ReadMe;
    private TabPage tabPage_Edit_Global;
    private GroupBox gb_Global_GameSettings;
    private Button b_Global_Climate;
    private NumericUpDown nup_Global_Climate;
    private ComboBox cb_Global_MaxUnits;
    private Button b_Global_RemoveFamousBattleMarker;
    private Button b_Global_TPY;
    private NumericUpDown nup_Global_TPY;
    private Button b_Global_EnablePolitics;
    private GroupBox gb_Province;
    private Button b_OpenTableDialog_Provinces;
    private Button b_ProvinceSetHappiness;
    private NumericUpDown nup_HappinessValue;
    private Label l_HappinessValue;
    private NumericUpDown nup_MovementPoints;
    private Button b_ResetMovementPoints;
    private TextBox tb_Global_CampaignTag;
    private Label l_CampaignTag;
    private GroupBox gb_Global_GameInfo;
    private ComboBox cb_EditFactions;
    private ComboBox cb_EditProvinces;
    private ComboBox cb_EditRegions;
    private Label l_Age;
    private Button b_RemoveAlertTraits;
    private ToolStripMenuItem tsmi_SaveParserIni;
    private ToolStripMenuItem tsmi_EditConfigFile;
    private ToolStripMenuItem tsmi_RestartConfig;
    private ToolStripMenuItem tsmi_SavegameFiles;
    private ToolStripMenuItem tsmi_Multiplayer;
    private ToolStripMenuItem internalDBToolStripMenuItem;
    private ToolStripMenuItem tsmi_ExportInternalDBTables;
    private ToolStripMenuItem tsmi_InternalDBViewer;
    private ToolStripMenuItem tsmi_FactionArrayIndex;
    private ToolStripMenuItem tsmi_Options_ActiveFactions;
    private ToolStripMenuItem tsmi_Options_RememberFactionValue;
    private ToolStripMenuItem tsmi_ExportSavegame;
    private ToolStripMenuItem tsmi_Warhammer;
    private Button b_RecruitmentComplete;
    private NumericUpDown nup_EditRank;
    private Label l_Rank;
    private GroupBox gb_Recruitment;
    private Button b_Map;
    private Button b_Corruption_Clear;
    private GroupBox gb_Global_Army;
    private ComboBox cb_Global_UnitSizeScale;
    private Label l_Global_UnitSizeScale;
    private Button b_Global_SetUnitSizeToScale;
    private Button b_Global_MaxUnits;
    private ToolStripMenuItem tsmi_Warhammer2;
    private ToolStripMenuItem tsmi_Britannia;
    private ToolStripMenuItem mi_Donation;
    private ToolStripMenuItem tsmi_PayPal;
    private Button b_ResearchAddSelectedProjects;
    private ToolStripMenuItem tsmi_ThreeKingdoms;

    // Methods
    public SaveParser()
    {
        this.InitializeComponent();
        string[] commandLineArgs = Environment.GetCommandLineArgs();
        if (commandLineArgs.Length != 0)
        {
            foreach (string str in commandLineArgs)
            {
                Console.WriteLine("CommandLineArg: " + str);
                if (str == "admin")
                {
                    this.mi_Admin.Visible = true;
                }
                if (str == "debug")
                {
                    this.tsmi_DebugWindow_Click(null, null);
                }
            }
        }
        this.readINIFile();
        this.cb_DisplayTable.Items.Add("character");
        this.cb_DisplayTable.Items.Add("faction");
        this.cb_DisplayTable.Items.Add("region");
        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        this.tsmi_Version.Text = " Version " + versionInfo.ProductVersion + " 'Celso'";
    }

    private void addComboBoxBuffers()
    {
        string text = this.cb_EditFactions.Text;
        if (!this.buffer_EditFactions.Contains(text))
        {
            this.buffer_EditFactions.Add(text);
            this.cb_EditFactions.DataSource = null;
            this.cb_EditFactions.DataSource = this.buffer_EditFactions;
            this.cb_EditFactions.SelectedItem = text;
        }
        text = this.cb_EditRegions.Text;
        if (!this.buffer_EditRegions.Contains(text))
        {
            this.buffer_EditRegions.Add(text);
            this.cb_EditRegions.DataSource = null;
            this.cb_EditRegions.DataSource = this.buffer_EditRegions;
            this.cb_EditRegions.SelectedItem = text;
        }
        text = this.cb_EditProvinces.Text;
        if (!this.buffer_EditProvinces.Contains(text))
        {
            this.buffer_EditProvinces.Add(text);
            this.cb_EditProvinces.DataSource = null;
            this.cb_EditProvinces.DataSource = this.buffer_EditProvinces;
            this.cb_EditProvinces.SelectedItem = text;
        }
    }

    private void b_BirthYears_Click(object sender, EventArgs e)
    {
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, true);
        if (list != null)
        {
            this.addComboBoxBuffers();
            this.setButtonsStatus(false);
            int age = Convert.ToInt32(this.nup_Age.Value);
            foreach (int num2 in list)
            {
                this.l_Status_Text.Text = "Setting all characters birth year of faction " + num2.ToString() + "...";
                this.l_Status_Text.Refresh();
                Util_Character character = new Util_Character(this);
                for (int i = 0; i < character.getFactionCharacterSize(num2); i++)
                {
                    character.setBirthYear(num2, i, -1, age);
                }
            }
            this.setButtonsStatus(true);
            this.l_Status_Text.Text = "Birth years set.";
        }
    }

    private void b_ConstructionComplete_Click(object sender, EventArgs e)
    {
        object obj1;
        this.addComboBoxBuffers();
        this.setButtonsStatus(false);
        if (this.cb_EditRegions.Text.Trim() == "")
        {
            obj1 = null;
        }
        else
        {
            char[] separator = new char[] { ';' };
            obj1 = this.cb_EditRegions.Text.Trim().Split(separator);
        }
        string[] strArray = (string[]) obj1;
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, false);
        this.debug("b_ConstructionFinish_Click:", false);
        this.debug("- regions  entries: " + ((strArray != null) ? strArray.Length.ToString() : "NULL"), false);
        this.debug("- factions entries: " + ((list != null) ? list.Count.ToString() : "NULL"), false);
        Util_Region region = new Util_Region(this);
        if (strArray == null)
        {
            this.debug("-> full region array", false);
            int num2 = region.getRegionSize();
            strArray = new string[num2];
            for (int i = 0; i < num2; i++)
            {
                strArray[i] = i.ToString();
            }
        }
        int[] source = null;
        if (list != null)
        {
            this.debug("-> factionIndex into factionID", false);
            source = new int[list.Count];
            int num4 = 0;
            while (true)
            {
                if (num4 >= source.Length)
                {
                    if ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))
                    {
                        Util_Army army = new Util_Army(this);
                        this.debug("- complete horde armies:", false);
                        foreach (int num5 in list)
                        {
                            int num6 = army.getFactionArmySize(num5);
                            int armyIndex = 0;
                            while (armyIndex < num6)
                            {
                                Table_Data_Army army2 = army.createArmy(num5, armyIndex, false);
                                int num8 = 0;
                                while (true)
                                {
                                    if (num8 >= army2.buildingConstructionItem_Needed.Length)
                                    {
                                        armyIndex++;
                                        break;
                                    }
                                    if (army2.buildingConstructionItem_Needed[num8] > -1)
                                    {
                                        string[] textArray1 = new string[] { "- complete construction for army ", armyIndex.ToString(), " of faction ", num5.ToString(), " , slot ", num8.ToString(), ". buildingConstructionItem_Needed = ", army2.buildingConstructionItem_Needed[num8].ToString() };
                                        this.debug(string.Concat(textArray1), false);
                                        army.setBuildingTurnsDone(num5, armyIndex, num8, 0x3e7);
                                    }
                                    num8++;
                                }
                            }
                        }
                    }
                    break;
                }
                source[num4] = DataBase.dic_FactionIndexId[list[num4]];
                this.debug("   - " + list[num4].ToString() + " -> " + source[num4].ToString(), false);
                num4++;
            }
        }
        string[] strArray2 = strArray;
        int index = 0;
        while (true)
        {
            int num9;
            while (true)
            {
                if (index < strArray2.Length)
                {
                    string str = strArray2[index];
                    this.debug("- checking for region " + str, false);
                    num9 = -1;
                    try
                    {
                        num9 = Convert.ToInt32(str);
                        if (num9 >= 0)
                        {
                            break;
                        }
                        throw new Exception();
                    }
                    catch (Exception)
                    {
                        this.l_Status_Text.Text = "invalid input at region";
                        this.setButtonsStatus(true);
                    }
                    return;
                }
                else
                {
                    this.setButtonsStatus(true);
                    this.l_Status_Text.Text = "Constructions complete.";
                    return;
                }
                break;
            }
            this.l_Status_Text.Text = "Complete all constructions of region " + num9.ToString() + "...";
            this.l_Status_Text.Refresh();
            bool flag = false;
            if (source == null)
            {
                flag = true;
            }
            else if (source.Contains<int>(region.createRegion(num9, true).controllerId))
            {
                flag = true;
            }
            if (flag)
            {
                int num10 = region.getRegionSlotsCount(num9);
                for (int i = 0; i < num10; i++)
                {
                    string[] textArray2 = new string[] { "- complete construction for region ", DataBase.dic_RegionIndexName[num9], " (arrayIndex: ", num9.ToString(), "), slot ", i.ToString(), "." };
                    this.debug(string.Concat(textArray2), false);
                    region.setBuildingTurnsDone(num9, i, 0x3e7);
                }
            }
            index++;
        }
    }

    private void b_Corruption_Clear_Click(object sender, EventArgs e)
    {
        this.debug("b_Corruption_Clear_Click:", false);
        this.addComboBoxBuffers();
        this.setButtonsStatus(false);
        Util_Region region = new Util_Region(this);
        foreach (int num in this.getRegionIndices())
        {
            Table_Data_Region region2 = region.createRegion(num, false);
            Dictionary<string, decimal> religion = region2.religion;
            this.debug("region: " + num.ToString(), false);
            foreach (string str in region2.religion.Keys.ToList<string>())
            {
                this.debug("- " + str, false);
                if (str.Contains("chaos"))
                {
                    region2.religion[str] = 0.0M;
                }
                if (str.Contains("undeath"))
                {
                    region2.religion[str] = 0.0M;
                }
                this.debug("set", false);
            }
            region.setReligion(num, region2.religion);
        }
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = "Clearing corruption complete.";
    }

    private void b_FactionArrayIndex_Click(object sender, EventArgs e)
    {
        this.b_FactionArrayIndex.Visible = false;
        this.tb_ParseFaction.Visible = false;
        this.cb_Factions.Visible = true;
    }

    private void b_Global_Climate_Click(object sender, EventArgs e)
    {
        int tpy = Convert.ToInt32(this.nup_Global_Climate.Value);
        this.parserFunctions.setClimate(tpy);
        this.l_Status_Text.Text = "Climate value set.";
    }

    private void b_Global_EnablePolitics_Click(object sender, EventArgs e)
    {
        new Util_Faction(this).setEnablePolitics(0);
        this.l_Status_Text.Text = "Politics for player faction enabled.";
    }

    private void b_Global_MaxUnits_Click(object sender, EventArgs e)
    {
        this.addComboBoxBuffers();
        int unitSize = Convert.ToInt32(this.cb_Global_MaxUnits.Text);
        this.parserFunctions.setMaxUnits(unitSize);
        this.l_Status_Text.Text = "Max units value set.";
    }

    private void b_Global_MoneyDistribution_Click(object sender, EventArgs e)
    {
        this.debug("b_MoneyDistribution_Click:", false);
        this.l_Status_Text.Text = "Distribute money...";
        this.l_Status_Text.Refresh();
        bool flag = this.cb_Global_MoneyDistribution_Rebels.Checked;
        this.setButtonsStatus(false);
        Util_Faction faction = new Util_Faction(this);
        int num = faction.getFactionSize();
        int num2 = Convert.ToInt32(this.nup_Global_MoneyDistribution.Value);
        bool flag2 = num2 == 0;
        int num3 = 0;
        int money = Convert.ToInt32(this.nup_Global_MoneyDistributionLimit.Value);
        for (int i = 0; i < num; i++)
        {
            Table_Data_Faction faction2 = faction.createFaction(i, false);
            if (faction2.characters > 0)
            {
                num3++;
            }
            else if ((flag2 && (flag || ((faction2.name != "rom_slave") && (faction2.name.IndexOf("_civil_war") <= 0)))) && (faction2.money > money))
            {
                int num7 = faction2.money - money;
                if (num7 > 0)
                {
                    num2 += num7;
                    string[] textArray1 = new string[] { faction2.name, ": ", faction2.money.ToString(), " -> ", money.ToString(), ", to Pool:", num7.ToString() };
                    this.debug(string.Concat(textArray1), false);
                    faction.setMoney(i, money);
                }
            }
        }
        this.debug("moneyPool(1) = " + num2.ToString(), false);
        if (flag2)
        {
            this.debug("Destroyed factions:", false);
            for (int k = 0; k < num; k++)
            {
                Table_Data_Faction faction3 = faction.createFaction(k, false);
                if ((faction3.characters == 0) && (faction3.money < money))
                {
                    int num10 = Math.Abs((int) (faction3.money - money));
                    if (num2 >= num10)
                    {
                        num2 -= num10;
                        string[] textArray2 = new string[] { faction3.name, "(+): ", faction3.money.ToString(), " -> ", money.ToString() };
                        this.debug(string.Concat(textArray2), false);
                        faction.setMoney(k, money);
                    }
                }
            }
        }
        this.debug("moneyPool(2) = " + num2.ToString(), false);
        int num5 = Convert.ToInt32((int) (num2 / num3));
        this.debug("moneyShare   = " + num5.ToString(), false);
        this.debug("Active factions:", false);
        for (int j = 0; j < num; j++)
        {
            Table_Data_Faction faction4 = faction.createFaction(j, false);
            if ((flag || (faction4.name != "rom_slave")) && (faction4.characters > 0))
            {
                int num12 = faction4.money + num5;
                string[] textArray3 = new string[] { faction4.name, ": ", faction4.money.ToString(), " -> ", num12.ToString() };
                this.debug(string.Concat(textArray3), false);
                faction.setMoney(j, num12);
            }
        }
        this.l_Status_Text.Text = "Setting done.";
        this.setButtonsStatus(true);
    }

    private void b_Global_RemoveFamousBattleMarker_Click(object sender, EventArgs e)
    {
        this.parserFunctions.removeFamousBattleMarkers();
        this.l_Status_Text.Text = "All markers removed.";
    }

    private void b_Global_SetUnitSizeToScale_Click(object sender, EventArgs e)
    {
        this.addComboBoxBuffers();
        this.l_Status_Text.Text = "Scaling armies of all factions...";
        this.setButtonsStatus(false);
        this.parserFunctions.setScaleToAllUnits((string) this.cb_Global_UnitSizeScale.SelectedItem);
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = "Scaling armies of all factions done.";
    }

    private void b_Global_TPY_Click(object sender, EventArgs e)
    {
        this.setButtonsStatus(false);
        this.parserFunctions.setTPY(Convert.ToInt32(this.nup_Global_TPY.Value));
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = "TPY set.";
    }

    private void b_Map_Click(object sender, EventArgs e)
    {
        this.l_Status_Text.Text = "Loading map data... (Can last up to one minute if loading the first time in a session)";
        this.l_Status_Text.Refresh();
        this.debug("b_Map_Click:", false);
        Window_Map map = new Window_Map(this, GlobalData.campaignMap, GlobalData.campaignTag);
        this.debug("- gathering data:", false);
        this.debug("-- gathering data: diplomacy", false);
        this.stopwatch = Stopwatch.StartNew();
        Dictionary<string, List<Tuple<string, byte>>> dictionary = new Dictionary<string, List<Tuple<string, byte>>>();
        Util_Diplomacy diplomacy1 = new Util_Diplomacy(this);
        for (int i = 1; i < DataBase.dic_FactionIndexNameActive.Count; i++)
        {
            KeyValuePair<int, string> pair = DataBase.dic_FactionIndexNameActive.ElementAt<KeyValuePair<int, string>>(i);
            int key = pair.Key;
            List<Tuple<string, byte>> list2 = new List<Tuple<string, byte>>();
            foreach (Table_Data_Diplomacy diplomacy in this.parserFunctions.loadDiplomacies(key, true))
            {
                byte num3 = 0;
                if (diplomacy.stance == "war")
                {
                    num3 = 1;
                }
                else if (diplomacy.stance == "military_allies")
                {
                    num3 = 2;
                }
                else if (diplomacy.stance == "defensive_allies")
                {
                    num3 = 2;
                }
                else if (diplomacy.stance == "vassal")
                {
                    num3 = 2;
                }
                else if (diplomacy.stance == "master")
                {
                    num3 = 2;
                }
                list2.Add(new Tuple<string, byte>(diplomacy.name, num3));
            }
            dictionary.Add(DataBase.dic_FactionIndexNameActive[key], list2);
        }
        this.stopwatch.Stop();
        this.debug("-- " + this.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s"), false);
        this.debug("-- gathering data: mapElements", false);
        this.stopwatch = Stopwatch.StartNew();
        this.stopwatch.Stop();
        this.debug("-- " + this.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s"), false);
        this.l_Status_Text.Text = "";
        map.setData(this.parserFunctions.getMapElements(), dictionary);
        map.ShowDialog();
        if (map.DialogResult == DialogResult.OK)
        {
            List<Data_MapElement> list = map.getData();
            this.parserFunctions.saveMapResult(list);
        }
        map.Dispose();
        GC.Collect();
        this.debug("Memory: " + $"{GC.GetTotalMemory(false):0,0}" + " / " + $"{GC.GetTotalMemory(false):0,0}", false);
    }

    private void b_MovementPoints_Click(object sender, EventArgs e)
    {
        this.addComboBoxBuffers();
        this.debug("b_MovementPoints_Click:", false);
        bool flag = true;
        int movementPoints = -1;
        if (sender == this.b_MovementPoints)
        {
            movementPoints = Convert.ToInt32(this.nup_MovementPoints.Value);
            flag = false;
        }
        this.debug("- performingReset: " + flag.ToString(), false);
        this.debug("- arg value      : " + movementPoints.ToString(), false);
        this.setButtonsStatus(false);
        int factionIndex = 0;
        this.l_Status_Text.Text = "Setting all characters movement points of player's faction ...";
        this.l_Status_Text.Refresh();
        this.debug("- modifying characters:", false);
        Util_Character character = new Util_Character(this);
        for (int i = 0; i < character.getFactionCharacterSize(factionIndex); i++)
        {
            string[] textArray1 = new string[] { "-- characterIndex ", i.ToString(), " of factionIndex ", factionIndex.ToString(), ": new value = ", movementPoints.ToString() };
            this.debug(string.Concat(textArray1), false);
            character.setMovementPoints(factionIndex, i, movementPoints, -1);
        }
        this.debug("- modifying armies:", false);
        Util_Army army = new Util_Army(this);
        for (int j = 0; j < army.getFactionArmySize(factionIndex); j++)
        {
            int movementPointsMax = -1;
            if (flag)
            {
                Table_Data_Army army2 = army.createArmy(factionIndex, j, false);
                if (DataBase.dic_CharacterIdIndex.ContainsKey(army2.commanderID))
                {
                    movementPointsMax = character.createCharacter(factionIndex, DataBase.dic_CharacterIdIndex[army2.commanderID], false).movementPointsMax;
                }
                this.debug("  army.commanderID " + army2.commanderID.ToString() + " -> valueCharacter: " + movementPointsMax.ToString(), false);
            }
            int num6 = flag ? movementPointsMax : movementPoints;
            if (!flag || (movementPointsMax != -1))
            {
                for (int k = 0; k < army.getFactionArmyUnitSize(factionIndex, j); k++)
                {
                    string[] textArray2 = new string[] { "-- armyIndex ", j.ToString(), ", unitIndex ", k.ToString(), " of factionIndex ", factionIndex.ToString(), ": new value = ", num6.ToString() };
                    this.debug(string.Concat(textArray2), false);
                    army.setMovementPoints(factionIndex, j, k, num6);
                }
            }
        }
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = "Movement points set.";
    }

    private void b_OpenTableDialog_Factions_Click(object sender, EventArgs e)
    {
        if ((this.windowTable == null) || this.windowTable.IsDisposed)
        {
            List<Table_Data_Info> list = new List<Table_Data_Info>();
            foreach (KeyValuePair<int, string> pair in DataBase.dic_FactionIndexName)
            {
                Table_Data_Info item = new Table_Data_Info {
                    arrayIndex = pair.Key,
                    name = pair.Value
                };
                list.Add(item);
            }
            IList iList = list;
            this.windowTable = new Window_Table("Factions", true);
            this.windowTable.Show();
            this.windowTable.setTableData<Table_Data_Info>(iList, Array.Empty<string>());
        }
    }

    private void b_OpenTableDialog_Provinces_Click(object sender, EventArgs e)
    {
        if ((this.windowTable == null) || this.windowTable.IsDisposed)
        {
            List<Table_Data_Info> list = new List<Table_Data_Info>();
            foreach (KeyValuePair<int, string> pair in DataBase.dic_ProvinceIndexName)
            {
                Table_Data_Info item = new Table_Data_Info {
                    arrayIndex = pair.Key,
                    name = pair.Value
                };
                list.Add(item);
            }
            IList iList = list;
            this.windowTable = new Window_Table("Provinces", true);
            this.windowTable.Show();
            this.windowTable.setTableData<Table_Data_Info>(iList, Array.Empty<string>());
        }
    }

    private void b_OpenTableDialog_Regions_Click(object sender, EventArgs e)
    {
        if ((this.windowTable == null) || this.windowTable.IsDisposed)
        {
            List<Table_Data_Info> list = new List<Table_Data_Info>();
            foreach (KeyValuePair<int, string> pair in DataBase.dic_RegionIndexName)
            {
                Table_Data_Info item = new Table_Data_Info {
                    arrayIndex = pair.Key,
                    name = pair.Value
                };
                list.Add(item);
            }
            IList iList = list;
            this.windowTable = new Window_Table("Regions", true);
            this.windowTable.Show();
            this.windowTable.setTableData<Table_Data_Info>(iList, Array.Empty<string>());
        }
    }

    private void b_Parse_Click(object sender, EventArgs e)
    {
        this.parsing(-1);
    }

    private void b_ProvinceSetHappiness_Click(object sender, EventArgs e)
    {
        object obj1;
        this.addComboBoxBuffers();
        this.setButtonsStatus(false);
        if (this.cb_EditProvinces.Text.Trim() == "")
        {
            obj1 = null;
        }
        else
        {
            char[] separator = new char[] { ';' };
            obj1 = this.cb_EditProvinces.Text.Trim().Split(separator);
        }
        string[] strArray = (string[]) obj1;
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, false);
        this.debug("b_ProvinceSetHappiness_Click:", false);
        this.debug("- provinces entries: " + ((strArray != null) ? strArray.Length.ToString() : "NULL"), false);
        this.debug("- factions  entries: " + ((list != null) ? list.Count.ToString() : "NULL"), false);
        Util_Province province = new Util_Province(this);
        if (strArray == null)
        {
            this.debug("-> full province array", false);
            int num2 = province.getProvinceSize();
            strArray = new string[num2];
            for (int i = 0; i < num2; i++)
            {
                strArray[i] = i.ToString();
            }
        }
        int[] source = null;
        if (list != null)
        {
            this.debug("-> factionIndex into factionID", false);
            source = new int[list.Count];
            for (int i = 0; i < source.Length; i++)
            {
                source[i] = DataBase.dic_FactionIndexId[list[i]];
                this.debug("   - " + list[i].ToString() + " -> " + source[i].ToString(), false);
            }
        }
        string[] strArray2 = strArray;
        int index = 0;
        while (true)
        {
            int num5;
            while (true)
            {
                if (index < strArray2.Length)
                {
                    string str = strArray2[index];
                    this.debug("- checking for province " + str, false);
                    num5 = -1;
                    try
                    {
                        num5 = Convert.ToInt32(str);
                        if (num5 >= 0)
                        {
                            break;
                        }
                        throw new Exception();
                    }
                    catch (Exception)
                    {
                        this.l_Status_Text.Text = "invalid input at province";
                        this.setButtonsStatus(true);
                    }
                    return;
                }
                else
                {
                    this.setButtonsStatus(true);
                    this.l_Status_Text.Text = "Setting happiness complete.";
                    return;
                }
                break;
            }
            Table_Data_Province province2 = province.createProvince(num5);
            this.l_Status_Text.Text = "Set happiness of province " + num5.ToString() + "...";
            this.l_Status_Text.Refresh();
            int num6 = 0;
            while (true)
            {
                if (num6 >= province2.factionProvinceManager_factionID.Length)
                {
                    index++;
                    break;
                }
                int num7 = province2.factionProvinceManager_factionID[num6];
                bool flag = false;
                if (num7 != -1)
                {
                    if (source == null)
                    {
                        flag = true;
                    }
                    if ((source != null) && source.Contains<int>(num7))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    int happiness = Convert.ToInt32(this.nup_HappinessValue.Value);
                    string[] textArray1 = new string[9];
                    textArray1[0] = "-> set happiness ";
                    textArray1[1] = happiness.ToString();
                    textArray1[2] = " for province '";
                    textArray1[3] = province2.name;
                    textArray1[4] = "' (arrayIndex: ";
                    textArray1[5] = num5.ToString();
                    textArray1[6] = "), slot ";
                    textArray1[7] = num6.ToString();
                    textArray1[8] = ".";
                    this.debug(string.Concat(textArray1), false);
                    province.setHappiness(num5, num6, happiness);
                }
                num6++;
            }
        }
    }

    private void b_RecruitmentComplete_Click(object sender, EventArgs e)
    {
        object obj1;
        this.addComboBoxBuffers();
        this.setButtonsStatus(false);
        if (this.cb_EditProvinces.Text.Trim() == "")
        {
            obj1 = null;
        }
        else
        {
            char[] separator = new char[] { ';' };
            obj1 = this.cb_EditProvinces.Text.Trim().Split(separator);
        }
        string[] strArray = (string[]) obj1;
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, false);
        this.debug("b_RecruitmentComplete_Click:", false);
        this.debug("- provinces entries: " + ((strArray != null) ? strArray.Length.ToString() : "NULL"), false);
        this.debug("- factions  entries: " + ((list != null) ? list.Count.ToString() : "NULL"), false);
        Util_Province province = new Util_Province(this);
        int rank = Convert.ToInt32(this.nup_EditRank.Value);
        int[] source = null;
        if (list != null)
        {
            this.debug("-> factionIndex into factionID", false);
            source = new int[list.Count];
            int num3 = 0;
            while (true)
            {
                if (num3 >= source.Length)
                {
                    if ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))
                    {
                        Util_Army army = new Util_Army(this);
                        this.debug("- complete horde armies:", false);
                        foreach (int num4 in list)
                        {
                            int num5 = army.getFactionArmySize(num4);
                            for (int i = 0; i < num5; i++)
                            {
                                army.completeRecruitment(num4, i, rank);
                            }
                        }
                    }
                    if ((this.gameTag == 5) || (this.gameTag == 6))
                    {
                        Util_Faction faction = new Util_Faction(this);
                        this.debug("- complete horde armies:", false);
                        foreach (int num7 in list)
                        {
                            faction.completeRecruitment(num7);
                        }
                    }
                    break;
                }
                source[num3] = DataBase.dic_FactionIndexId[list[num3]];
                this.debug("   - " + list[num3].ToString() + " -> " + source[num3].ToString(), false);
                num3++;
            }
        }
        if (strArray == null)
        {
            this.debug("-> full province array", false);
            int num8 = province.getProvinceSize();
            strArray = new string[num8];
            for (int i = 0; i < num8; i++)
            {
                strArray[i] = i.ToString();
            }
        }
        string[] strArray2 = strArray;
        int index = 0;
        while (true)
        {
            int num10;
            while (true)
            {
                if (index < strArray2.Length)
                {
                    string str = strArray2[index];
                    this.debug("- checking for province " + str, false);
                    num10 = -1;
                    try
                    {
                        num10 = Convert.ToInt32(str);
                        if (num10 >= 0)
                        {
                            break;
                        }
                        throw new Exception();
                    }
                    catch (Exception)
                    {
                        this.l_Status_Text.Text = "invalid input at province";
                        this.setButtonsStatus(true);
                    }
                    return;
                }
                else
                {
                    Util_SeaRegion region = new Util_SeaRegion(this);
                    foreach (Table_Data_Province province3 in this.parserFunctions.loadSeaRegions())
                    {
                        this.debug("- checking for seaRegion " + province3.name, false);
                        for (int i = 0; i < province3.factionProvinceManager_factionID.Length; i++)
                        {
                            int num14 = province3.factionProvinceManager_factionID[i];
                            bool flag2 = false;
                            if (num14 != -1)
                            {
                                if (source == null)
                                {
                                    flag2 = true;
                                }
                                if ((source != null) && source.Contains<int>(num14))
                                {
                                    flag2 = true;
                                }
                            }
                            if (flag2)
                            {
                                string[] textArray2 = new string[9];
                                textArray2[0] = "-> complete recruitment with rank ";
                                textArray2[1] = rank.ToString();
                                textArray2[2] = " for seaRegion '";
                                textArray2[3] = province3.name;
                                textArray2[4] = "' (arrayIndex: ";
                                textArray2[5] = province3.arrayIndex.ToString();
                                textArray2[6] = "), slot ";
                                textArray2[7] = i.ToString();
                                textArray2[8] = ".";
                                this.debug(string.Concat(textArray2), false);
                                region.completeRecruitment(province3.arrayIndex, i, rank);
                            }
                        }
                    }
                    this.setButtonsStatus(true);
                    this.l_Status_Text.Text = "Recruitment complete.";
                    return;
                }
                break;
            }
            Table_Data_Province province2 = province.createProvince(num10);
            this.l_Status_Text.Text = "Complete recruitment of province " + num10.ToString() + "...";
            this.l_Status_Text.Refresh();
            int num11 = 0;
            while (true)
            {
                if (num11 >= province2.factionProvinceManager_factionID.Length)
                {
                    index++;
                    break;
                }
                int num12 = province2.factionProvinceManager_factionID[num11];
                bool flag = false;
                if (num12 != -1)
                {
                    if (source == null)
                    {
                        flag = true;
                    }
                    if ((source != null) && source.Contains<int>(num12))
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    string[] textArray1 = new string[9];
                    textArray1[0] = "-> complete recruitment with rank ";
                    textArray1[1] = rank.ToString();
                    textArray1[2] = " for province '";
                    textArray1[3] = province2.name;
                    textArray1[4] = "' (arrayIndex: ";
                    textArray1[5] = num10.ToString();
                    textArray1[6] = "), slot ";
                    textArray1[7] = num11.ToString();
                    textArray1[8] = ".";
                    this.debug(string.Concat(textArray1), false);
                    province.completeRecruitment(num10, num11, rank);
                }
                num11++;
            }
        }
    }

    private void b_Reduce_Click(object sender, EventArgs e)
    {
        this.manipulateUnitStrength(0);
    }

    private void b_RemoveAlertTraits_Click(object sender, EventArgs e)
    {
        this.debug("b_RemoveAlertTraits_Click:", false);
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, true);
        if (list != null)
        {
            this.addComboBoxBuffers();
            this.setButtonsStatus(false);
            foreach (int num in list)
            {
                this.l_Status_Text.Text = "Remove all characters 'alert' traits of faction " + num.ToString() + "...";
                this.l_Status_Text.Refresh();
                Util_Character character = new Util_Character(this);
                for (int i = 0; i < character.getFactionCharacterSize(num); i++)
                {
                    Table_Data_Character character2 = character.createCharacter(num, i, false);
                    if (character2.traits.EndsWith("*"))
                    {
                        List<int> list2 = new List<int>();
                        int index = 0;
                        while (true)
                        {
                            if ((index >= character2.trait.Length) || (character2.trait[index] == null))
                            {
                                if (list2.Count > 0)
                                {
                                    list2.Reverse();
                                    foreach (int num4 in list2)
                                    {
                                        character.removeTrait(num, i, num4);
                                    }
                                }
                                break;
                            }
                            foreach (string str in GlobalData.traitAlert)
                            {
                                if (character2.trait[index].Contains(str))
                                {
                                    string[] textArray1 = new string[] { "- remove trait(index: ", index.ToString(), ") 'item' from character ", i.ToString(), " of faction ", num.ToString(), "." };
                                    this.debug(string.Concat(textArray1), false);
                                    list2.Add(index);
                                }
                            }
                            index++;
                        }
                    }
                }
            }
            this.setButtonsStatus(true);
            this.l_Status_Text.Text = "All 'alert' traits removed.";
        }
    }

    private void b_Replenish_Click(object sender, EventArgs e)
    {
        this.manipulateUnitStrength(1);
    }

    private void b_ResearchAddAllProjects_Click(object sender, EventArgs e)
    {
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, true);
        if (list != null)
        {
            this.addComboBoxBuffers();
            this.setButtonsStatus(false);
            foreach (int num in list)
            {
                this.l_Status_Text.Text = "Add all research project of faction " + num.ToString() + "...";
                this.l_Status_Text.Refresh();
                new Util_Faction(this).addAllResearchProjects(num, null);
                this.setButtonsStatus(true);
                this.l_Status_Text.Text = "Research projects added.";
            }
        }
    }

    private void b_ResearchAddSelectedProjects_Click(object sender, EventArgs e)
    {
        Window_Selector selector = new Window_Selector("Research projects");
        ArrayList lists = new ArrayList();
        bool[] locked = new bool[DataBase.dbt_Technologies.Count];
        Util_Faction faction = new Util_Faction(this);
        int factionIndex = this.getFactionIndices(this.cb_EditFactions.Text, false)[0];
        ArrayList list2 = faction.getResearched(factionIndex);
        int index = 0;
        foreach (Data_Technology technology in DataBase.dbt_Technologies)
        {
            ArrayList list3 = new ArrayList {
                technology.key
            };
            lists.Add(list3);
            if (list2.Contains(technology.key))
            {
                locked[index] = true;
            }
            index++;
        }
        string[] headers = new string[] { "Tech" };
        selector.setTableData(headers, lists, locked, locked);
        selector.ShowDialog();
        if (selector.DialogResult == DialogResult.OK)
        {
            faction.addAllResearchProjects(factionIndex, selector.getData());
        }
        selector.Dispose();
    }

    private void b_ResearchComplete_Click(object sender, EventArgs e)
    {
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, true);
        if (list != null)
        {
            this.addComboBoxBuffers();
            this.setButtonsStatus(false);
            foreach (int num in list)
            {
                this.l_Status_Text.Text = "Complete research project of faction " + num.ToString() + "...";
                this.l_Status_Text.Refresh();
                new Util_Faction(this).setResearch(num);
            }
            this.setButtonsStatus(true);
            this.l_Status_Text.Text = "Research complete.";
        }
    }

    private void b_ResetAgentAction_Click(object sender, EventArgs e)
    {
        this.addComboBoxBuffers();
        this.setButtonsStatus(false);
        this.l_Status_Text.Text = "Resetting all agent action abililites player faction ...";
        this.l_Status_Text.Refresh();
        Util_Character character = new Util_Character(this);
        for (int i = 0; i < character.getFactionCharacterSize(0); i++)
        {
            character.setAgentActionAbility(0, i);
        }
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = "Agent actions abililties reset.";
    }

    private void cb_DisplayTable_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.tb_ParseFaction.Text = !this.tsmi_Options_RememberFactionValue.Checked ? "0" : this.selectedFactionArrayIndex;
        this.cb_Filter.Items.Clear();
        this.cb_Filter.SelectedIndex = -1;
        this.cb_Filter.Text = "";
        if ((this.cb_DisplayTable.SelectedIndex == 1) && ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))))
        {
            this.cb_Filter.Items.Add("all");
            this.cb_Filter.Items.Add("only active");
            this.cb_Filter.Items.Add("destroyed");
            this.cb_Filter.SelectedIndex = 1;
            this.tb_ParseFaction.Text = "-1";
        }
        if (this.cb_DisplayTable.SelectedIndex == 6)
        {
            this.cb_Filter.Items.Add("all");
            this.cb_Filter.Items.Add("only active");
            this.cb_Filter.SelectedIndex = 1;
        }
        if ((this.cb_DisplayTable.SelectedIndex == 0) && ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 3)))))))
        {
            this.cb_Filter.Items.Add("- ALL");
            if ((this.gameTag == 1) || ((this.gameTag == 5) || (this.gameTag == 6)))
            {
                this.cb_Filter.Items.Add("generals");
            }
            else if (this.gameTag == 0)
            {
                this.cb_Filter.Items.Add("- GENERALS & LADIES");
                this.cb_Filter.Items.Add("generals");
                this.cb_Filter.Items.Add("ladies");
            }
            else if (this.gameTag == 2)
            {
                this.cb_Filter.Items.Add("- GENERALS & COLONELS");
                this.cb_Filter.Items.Add("generals");
                this.cb_Filter.Items.Add("colonels");
            }
            else if (this.gameTag == 3)
            {
                this.cb_Filter.Items.Add("General");
                this.cb_Filter.Items.Add("admiral");
                this.cb_Filter.Items.Add("minister");
            }
            this.cb_Filter.Items.Add("- AGENTS");
            if ((this.gameTag == 1) || (this.gameTag == 0))
            {
                this.cb_Filter.Items.Add("spies");
                this.cb_Filter.Items.Add("champions");
                this.cb_Filter.Items.Add("dignitaries");
            }
            else if ((this.gameTag == 5) || (this.gameTag == 6))
            {
                this.cb_Filter.Items.Add("champions");
                this.cb_Filter.Items.Add("wizards");
                this.cb_Filter.Items.Add("spies");
                this.cb_Filter.Items.Add("dignitaries");
                this.cb_Filter.Items.Add("runesmiths");
            }
            else if (this.gameTag == 2)
            {
                this.cb_Filter.Items.Add("ninja, spy, shinobi");
                this.cb_Filter.Items.Add("monk, veteran");
                this.cb_Filter.Items.Add("metsuke, inspector, shinsengumi");
                this.cb_Filter.Items.Add("geisha, dancer");
                this.cb_Filter.Items.Add("- OTHERS");
                this.cb_Filter.Items.Add("captain");
                this.cb_Filter.Items.Add("minister");
            }
            if (this.gameTag == 3)
            {
                this.cb_Filter.Items.Add("rake");
                this.cb_Filter.Items.Add("gentleman");
            }
            if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
            {
                this.cb_Filter.Items.Add("- COLONELS");
            }
            this.cb_Filter.SelectedIndex = 0;
        }
        if (this.cb_DisplayTable.SelectedIndex == 4)
        {
            this.cb_Filter.Items.Add("armies");
            if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
            {
                this.cb_Filter.Items.Add("armies and garrisons");
            }
            this.cb_Filter.SelectedIndex = 0;
        }
        if (this.cb_DisplayTable.SelectedIndex == 5)
        {
            this.cb_Filter.Items.Add("armies");
            if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
            {
                this.cb_Filter.Items.Add("armies and garrisons");
            }
            this.cb_Filter.SelectedIndex = 0;
        }
        if ((this.cb_DisplayTable.SelectedIndex != 0) && ((this.cb_DisplayTable.SelectedIndex != 4) && ((this.cb_DisplayTable.SelectedIndex != 5) && ((this.cb_DisplayTable.SelectedIndex != 6) && ((this.cb_DisplayTable.SelectedIndex != 3) && ((this.cb_DisplayTable.SelectedIndex != 2) || ((this.gameTag != 5) && ((this.gameTag != 6) && ((this.gameTag != 0) && ((this.gameTag != 1) && ((this.gameTag != 2) && (this.gameTag != 7))))))))))))
        {
            this.tb_ParseFaction.Enabled = false;
            this.b_FactionArrayIndex.Enabled = false;
        }
        else
        {
            this.tb_ParseFaction.Enabled = true;
            this.b_FactionArrayIndex.Enabled = true;
        }
    }

    private void cb_Factions_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.b_FactionArrayIndex.Visible = true;
        this.tb_ParseFaction.Visible = true;
        this.cb_Factions.Visible = false;
        this.tb_ParseFaction.Text = ((KeyValuePair<int, string>) this.cb_Factions.SelectedItem).Key.ToString();
    }

    private void cb_Global_UnitSizeScale_SelectionChangeCommitted(object sender, EventArgs e)
    {
        this.addComboBoxBuffers();
        this.parserFunctions.setUnitSizeScale((string) this.cb_Global_UnitSizeScale.SelectedItem);
        this.l_Status_Text.Text = "Unit strength scale value set.";
    }

    private void cellDoubleClick_DataTable(object sender, DataGridViewCellEventArgs e)
    {
        if (((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))) && (this.cb_DisplayTable.SelectedIndex == 0))
        {
            if (e.RowIndex >= 0)
            {
                int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                this.processEditorDialog_Character(factionIndex, Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[2].Value));
            }
        }
        else if (((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))) && (this.cb_DisplayTable.SelectedIndex == 3))
        {
            if (e.RowIndex >= 0)
            {
                int provinceIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                Window_Editor_WAR2_Province province = new Window_Editor_WAR2_Province(this, this.gameTag);
                province.setData(this.parserFunctions.getProvince(provinceIndex));
                province.ShowDialog();
                if (province.DialogResult == DialogResult.OK)
                {
                    Table_Data_Province province2 = province.getData();
                    Util_Province province3 = new Util_Province(this);
                    for (int i = 0; i < 4; i++)
                    {
                        if (province2.factionProvinceManager_factionID[i] != -1)
                        {
                            province3.setPopulationSurplus(provinceIndex, i, province2.factionProvinceManager_populationSurplus[i]);
                            province3.setHappiness(provinceIndex, i, province2.factionProvinceManager_happiness[i]);
                        }
                    }
                }
                province.Dispose();
            }
        }
        else if (((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))) && (this.cb_DisplayTable.SelectedIndex == 2))
        {
            if (e.RowIndex >= 0)
            {
                int regionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                Window_Editor_WAR2_Region region = new Window_Editor_WAR2_Region(this, this.gameTag);
                region.setData(this.parserFunctions.getRegion(regionIndex));
                region.ShowDialog();
                if (region.DialogResult == DialogResult.OK)
                {
                    Table_Data_Region region2 = region.getData();
                    Util_Region region3 = new Util_Region(this);
                    if ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))
                    {
                        region3.setReligion(regionIndex, region2.religion);
                    }
                    for (int i = 0; i < 0x12; i++)
                    {
                        if (region2.buildingConstructionItem_Done[i] != -1)
                        {
                            region3.setBuildingTurnsDone(regionIndex, i, region2.buildingConstructionItem_Done[i]);
                        }
                        if (region2.building[i] != "-")
                        {
                            region3.setBuilding(regionIndex, i, region2.building[i], region2.buildingFaction[i]);
                        }
                    }
                }
                region.Dispose();
            }
        }
        else if (((this.gameTag != 1) && ((this.gameTag != 0) && ((this.gameTag != 2) && ((this.gameTag != 5) && ((this.gameTag != 6) && (this.gameTag != 7)))))) || (this.cb_DisplayTable.SelectedIndex != 4))
        {
            if (((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7))))) && (this.cb_DisplayTable.SelectedIndex == 1))
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == 10)
                    {
                        int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                        this.processEditorDialog_AncillaryPool(factionIndex);
                    }
                    else
                    {
                        int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                        Window_Editor_WAR2_Faction faction = new Window_Editor_WAR2_Faction(this, this.gameTag);
                        faction.setData(this.parserFunctions.getFaction(factionIndex));
                        faction.ShowDialog();
                        if (faction.DialogResult == DialogResult.OK)
                        {
                            Table_Data_Faction faction2 = faction.getData();
                            Util_Faction faction3 = new Util_Faction(this);
                            faction3.setCampaignEffects(factionIndex, faction2.campaignEffects);
                            if ((this.gameTag == 5) || (this.gameTag == 6))
                            {
                                faction3.setResources(factionIndex, faction2.resources);
                            }
                            if (this.gameTag == 6)
                            {
                                faction3.setResourcesW2(factionIndex, faction2.influence, faction2.fragments);
                                faction3.setPooledResourcesW2(factionIndex, faction2.poolResourceValue);
                            }
                            faction3.setMoney(factionIndex, faction2.money);
                            faction3.setPoliticalPower(factionIndex, 0, faction2.politicalPower[0]);
                            faction3.setPoliticalPower(factionIndex, 1, faction2.politicalPower[1]);
                            faction3.setImperiumLevel(factionIndex, faction2.imperium);
                            faction3.setCapital(factionIndex, faction2.capital);
                        }
                        faction.Dispose();
                    }
                }
            }
            else if ((((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || (this.gameTag == 5)))) && (this.cb_DisplayTable.SelectedIndex == 5)) && (e.RowIndex >= 0))
            {
                int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                int armyIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
                int unitIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[2].Value);
                if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6))))
                {
                    Window_Editor_WAR2_Unit unit = new Window_Editor_WAR2_Unit(this, this.gameTag);
                    unit.setData(this.parserFunctions.getUnit(factionIndex, armyIndex, unitIndex));
                    unit.ShowDialog();
                    if (unit.DialogResult == DialogResult.OK)
                    {
                        Table_Data_Unit unit2 = unit.getData();
                        Util_Unit unit1 = new Util_Unit(this);
                        unit1.setMax(factionIndex, armyIndex, unitIndex, unit2.max);
                        unit1.setStrength(factionIndex, armyIndex, unitIndex, unit2.strength);
                        unit1.setRank(factionIndex, armyIndex, unitIndex, unit2.rank);
                        unit1.setBonusStats(factionIndex, armyIndex, unitIndex, unit2.bonus, null);
                    }
                    unit.Dispose();
                }
                else
                {
                    Window_Editor_S2_Unit unit3 = new Window_Editor_S2_Unit(this, this.gameTag);
                    unit3.setData(this.parserFunctions.getUnit(factionIndex, armyIndex, unitIndex));
                    unit3.ShowDialog();
                    if (unit3.DialogResult == DialogResult.OK)
                    {
                        Table_Data_Unit unit4 = unit3.getData();
                        Util_Unit unit5 = new Util_Unit(this);
                        unit5.setMax(factionIndex, armyIndex, unitIndex, unit4.max);
                        unit5.setStrength(factionIndex, armyIndex, unitIndex, unit4.strength);
                        unit5.setRank(factionIndex, armyIndex, unitIndex, unit4.rank);
                        unit5.setBonusStats(factionIndex, armyIndex, unitIndex, unit4.bonus, unit4.bonusString);
                    }
                    unit3.Dispose();
                }
            }
        }
        else if (e.RowIndex >= 0)
        {
            if (e.ColumnIndex == 9)
            {
                int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                uint key = Convert.ToUInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[10].Value);
                if (DataBase.dic_CharacterIdIndex.ContainsKey(key))
                {
                    this.processEditorDialog_Character(factionIndex, DataBase.dic_CharacterIdIndex[key]);
                }
            }
            else if (e.ColumnIndex == 6)
            {
                int num10 = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                int option = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
                this.cb_DisplayTable.SelectedIndex = 5;
                this.tb_ParseFaction.Text = num10.ToString();
                this.parsing(option);
            }
            else if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
            {
                int factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                int armyIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
                Window_Editor_WAR2_Army army = new Window_Editor_WAR2_Army(this, this.gameTag);
                army.setData(this.parserFunctions.getArmy(factionIndex, armyIndex));
                army.ShowDialog();
                if (army.DialogResult == DialogResult.OK)
                {
                    Table_Data_Army army2 = army.getData();
                    if (army2.scale != "1")
                    {
                        this.parserFunctions.setScaleToUnitsArmy(army2.scale, factionIndex, armyIndex);
                    }
                    Util_Army army3 = new Util_Army(this);
                    if ((this.gameTag != 5) || (this.gameTag == 6))
                    {
                        army3.setRank(factionIndex, armyIndex, army2.rank);
                        army3.setTraitPoints(factionIndex, armyIndex, army2.traitPoints);
                    }
                    if (army2.pop > -1)
                    {
                        army3.setPopSurplus(factionIndex, armyIndex, army2.pop);
                    }
                    if (army2.morale > -1M)
                    {
                        army3.setMorale(factionIndex, armyIndex, army2.morale);
                    }
                    if (army2.name != army.oldName)
                    {
                        army3.setName(factionIndex, armyIndex, army2.name);
                    }
                    if (army2.strength != army.oldValueStrength)
                    {
                        for (int i = 0; i < army3.getFactionArmyUnitSize(factionIndex, armyIndex); i++)
                        {
                            army3.setStrength(factionIndex, armyIndex, i, -1, army2.strength);
                        }
                    }
                    if ((this.gameTag == 0) || ((this.gameTag == 5) || (this.gameTag == 6)))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (army2.buildingConstructionItem_Done[i] != -1)
                            {
                                army3.setBuildingTurnsDone(factionIndex, armyIndex, i, army2.buildingConstructionItem_Done[i]);
                            }
                            if (army2.building[i] != "-")
                            {
                                army3.setBuilding(factionIndex, armyIndex, i, army2.building[i], army2.buildingFaction[i]);
                            }
                        }
                    }
                }
                army.Dispose();
            }
        }
    }

    private void cellMouseUp_DataTable(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            this.clickedRowIndex = e.RowIndex;
            this.cms_Table.Show(Cursor.Position);
        }
    }

    private void cellValueChanged_DataTable(object sender, DataGridViewCellEventArgs e)
    {
        int columnIndex = e.ColumnIndex;
        int factionIndex = -999;
        int money = -999;
        if (this.cb_DisplayTable.SelectedIndex != 6)
        {
            factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
            money = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
        }
        if (this.cb_DisplayTable.SelectedIndex == 6)
        {
            factionIndex = Convert.ToInt32(this.tb_ParseFaction.Text);
            int index = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
            object obj2 = this.dgv_ParserResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            int factionID = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
            int targetFactionArrayIndex = this.parserFunctions.getFactionArrayIndexOfFactionID(factionID);
            if (columnIndex == 3)
            {
                string stance = obj2.ToString();
                if ((stance == "neutral") || ((stance == "war") || ((stance == "military_allies") || (stance == "defensive_allies"))))
                {
                    new Util_Diplomacy(this).setStance(factionIndex, index, stance, targetFactionArrayIndex);
                }
                else
                {
                    this.dgv_ParserResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "INVALID";
                    this.l_Status_Text.Text = "invalid stance value.";
                }
            }
            else if (columnIndex == 4)
            {
                new Util_Diplomacy(this).setKnown(factionIndex, index, ((bool) obj2) ? "True" : "False", targetFactionArrayIndex);
            }
            else if (columnIndex == 5)
            {
                new Util_Diplomacy(this).setTradeAgreement(factionIndex, index, ((bool) obj2) ? "True" : "False", targetFactionArrayIndex);
            }
        }
        else if (this.cb_DisplayTable.SelectedIndex == 1)
        {
            if (columnIndex == 3)
            {
                new Util_Faction(this).setMoney(factionIndex, money);
            }
        }
        else if (this.cb_DisplayTable.SelectedIndex == 4)
        {
            int armyIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
            if (columnIndex == 4)
            {
                new Util_Army(this).setRank(factionIndex, armyIndex, money);
            }
            else if (columnIndex == 5)
            {
                new Util_Army(this).setTraitPoints(factionIndex, armyIndex, money);
            }
            else if (columnIndex == 11)
            {
                if (money > -1)
                {
                    new Util_Army(this).setPopSurplus(factionIndex, armyIndex, money);
                }
            }
            else if ((columnIndex == 12) && (money > -1))
            {
                new Util_Army(this).setMorale(factionIndex, armyIndex, money);
            }
        }
        else if (this.cb_DisplayTable.SelectedIndex != 0)
        {
            if (this.cb_DisplayTable.SelectedIndex == 5)
            {
                factionIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[0].Value);
                int armyIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[1].Value);
                int unitIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[2].Value);
                if (columnIndex == 4)
                {
                    new Util_Unit(this).setRank(factionIndex, armyIndex, unitIndex, money);
                }
                else if (columnIndex == 6)
                {
                    new Util_Unit(this).setMax(factionIndex, armyIndex, unitIndex, money);
                }
                else if (columnIndex == 7)
                {
                    new Util_Unit(this).setStrength(factionIndex, armyIndex, unitIndex, money);
                }
            }
        }
        else
        {
            int characterIndex = Convert.ToInt32(this.dgv_ParserResult.Rows[e.RowIndex].Cells[2].Value);
            Util_Character character = new Util_Character(this);
            if (columnIndex == 5)
            {
                character.setRank(factionIndex, characterIndex, money);
            }
            else if (columnIndex == 6)
            {
                character.setSkillPoints(factionIndex, characterIndex, money);
            }
            else if ((columnIndex == 7) || ((columnIndex == 8) || (columnIndex == 9)))
            {
                character.setAgentAttribute(factionIndex, characterIndex, e.ColumnIndex - 7, money);
            }
            else if ((columnIndex == 10) || (columnIndex == 11))
            {
                character.setPoliticsAttribute(factionIndex, characterIndex, e.ColumnIndex - 10, money);
            }
            else if (columnIndex == 12)
            {
                character.setBirthYear(factionIndex, characterIndex, money, -1);
            }
            else if (columnIndex == 13)
            {
                character.setBirthYear(factionIndex, characterIndex, -1, money);
            }
        }
        this.dgv_ParserResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
    }

    private bool[] checkConfiguration()
    {
        this.debug("checking game configurations:", false);
        bool[,] flagArray = new bool[GlobalData.savefileDirectory.Length, 5];
        string[] strArray = new string[GlobalData.savefileDirectory.Length];
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string[] strArray2 = new string[] { @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}", @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App {0}" };
        string[] textArray2 = new string[9];
        textArray2[0] = "325610";
        textArray2[1] = "214950";
        textArray2[2] = "34330";
        textArray2[3] = "34030";
        textArray2[4] = "10500";
        textArray2[5] = "364360";
        textArray2[6] = "594570";
        textArray2[7] = "712100";
        textArray2[8] = "779340";
        string[] strArray3 = textArray2;
        string[] textArray3 = new string[9];
        textArray3[0] = "Attila";
        textArray3[1] = "Rome2";
        textArray3[2] = "Shogun2";
        textArray3[3] = "Napoleon";
        textArray3[4] = "Empire";
        textArray3[5] = "Warhammer";
        textArray3[6] = "Warhammer2";
        textArray3[7] = "Thrones";
        textArray3[8] = "Three_Kingdoms";
        string[] strArray4 = textArray3;
        for (int i = 0; i < GlobalData.savefileDirectory.Length; i++)
        {
            this.debug("- Checking configuration for: " + GlobalData.savefileDirectory[i], false);
            string str3 = @"\The Creative Assembly\" + GlobalData.savefileDirectory[i] + @"\save_games\";
            string path = folderPath + str3;
            flagArray[i, 0] = Directory.Exists(path);
            this.debug(string.Format("-- savegame path exists                     : " + flagArray[i, 0].ToString(), Array.Empty<object>()), false);
            if (!flagArray[i, 0])
            {
                this.debug("-- No savegame path found. No further checks needed.", LogLevelType.info, false);
            }
            else
            {
                string str5 = null;
                try
                {
                    for (int k = 0; k < strArray2.Length; k++)
                    {
                        str5 = (string) Registry.GetValue(string.Format(strArray2[k], strArray3[i]), "InstallLocation", "");
                        if (!string.IsNullOrEmpty(str5))
                        {
                            flagArray[i, 1] = true;
                            strArray[i] = str5.Trim();
                            break;
                        }
                    }
                }
                catch
                {
                    flagArray[i, 1] = false;
                }
                this.debug(string.Format("-- game path found in registry              : " + flagArray[i, 1].ToString(), Array.Empty<object>()), false);
                if (flagArray[i, 1])
                {
                    this.debug(string.Format("--- game path in registry                   : " + strArray[i], Array.Empty<object>()), false);
                    bool flag2 = Directory.Exists(strArray[i]);
                    this.debug(string.Format("--- game path in registry valid             : " + flag2.ToString(), Array.Empty<object>()), false);
                    if (!flag2)
                    {
                        flagArray[i, 1] = false;
                    }
                }
                string str6 = this.dic_properties[GlobalData.gamePathProperty[i]];
                flagArray[i, 2] = !string.IsNullOrEmpty(str6) && Directory.Exists(str6);
                this.debug(string.Format("-- game path set correcty in SaveParser.ini : " + flagArray[i, 2].ToString(), Array.Empty<object>()), false);
                string str7 = null;
                flagArray[i, 4] = false;
                if (flagArray[i, 1])
                {
                    str7 = strArray[i];
                }
                else if (flagArray[i, 2])
                {
                    str7 = str6;
                }
                if (str7 != null)
                {
                    flagArray[i, 4] = File.Exists(str7 + (str7.EndsWith(@"\") ? "" : @"\") + strArray4[i] + ".exe");
                }
                this.debug(string.Format("-- game exe exists                          : " + flagArray[i, 4].ToString(), Array.Empty<object>()), false);
                flagArray[i, 3] = File.Exists(@"data\dbt_" + GlobalData.gameTag[i] + "_Names.xml");
                this.debug(string.Format("-- xml files exists                         : " + flagArray[i, 3].ToString(), Array.Empty<object>()), false);
                bool flag = Directory.Exists(@"data\campaign_maps");
                this.debug(string.Format("-- map files exists                         : " + flag.ToString(), Array.Empty<object>()), false);
            }
        }
        string message = "";
        bool[] flagArray2 = new bool[GlobalData.savefileDirectory.Length];
        for (int j = 0; j < flagArray2.Length; j++)
        {
            if (flagArray[j, 1] && !flagArray[j, 2])
            {
                this.dic_properties[GlobalData.gamePathProperty[j]] = strArray[j];
            }
            flagArray2[j] = (flagArray[j, 0] && flagArray[j, 4]) && ((flagArray[j, 1] || flagArray[j, 2]) || flagArray[j, 3]);
            if (flagArray[j, 0] && (flagArray[j, 4] && !flagArray2[j]))
            {
                message = ((((((message + "SaveParser for game '" + GlobalData.savefileDirectory[j] + "' not properly configurated:\n") + "--- savegame path                exists : " + flagArray[j, 0].ToString() + " \n") + "--- game exe                     exists : " + flagArray[j, 4].ToString() + " \n") + "--- game path (registry)         exists : " + flagArray[j, 1].ToString() + " \n") + "--- game path (SaveParser.ini)   exists : " + flagArray[j, 2].ToString() + " \n") + "--- xml files (XMLPackDataFiles) exists : " + flagArray[j, 3].ToString() + " \n") + "--- At least one of the game paths or xml files must be true.\n";
            }
        }
        if (message.Length > 0)
        {
            this.showError(message, "Configuration Error", false);
        }
        return flagArray2;
    }

    private void dataGridViewResult_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || ((this.gameTag == 5) || (this.gameTag == 6))))) && (this.cb_DisplayTable.SelectedIndex == 0))
        {
            this.setColumnProperties();
        }
    }

    private void dataGridViewResult_DataError(object sender, DataGridViewDataErrorEventArgs anError)
    {
        MessageBox.Show("Invalid input at field: " + this.dgv_ParserResult.Columns[anError.ColumnIndex].HeaderText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }

    public void debug(string s, bool noLineBreak = false)
    {
        this.debug(s, LogLevelType.normal, noLineBreak);
    }

    public void debug(string s, LogLevelType level, bool noLineBreak = false)
    {
        if (this.windowDebug != null)
        {
            this.windowDebug.debug(s, level, noLineBreak);
        }
        if (this.logWriter != null)
        {
            this.logWriter.Write(s + (!noLineBreak ? "\n" : ""));
            this.logWriter.Flush();
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void exportDBDictionary<T1, T2>(Dictionary<T1, T2> dic, string fileName)
    {
        SerializableDictionary<T1, T2> o = new SerializableDictionary<T1, T2>();
        foreach (KeyValuePair<T1, T2> pair in dic)
        {
            T1 key = pair.Key;
            T2 local2 = pair.Value;
            o.Add(key, local2);
        }
        FileStream stream = File.Create(Path.Combine(this.config_AppDataPath, fileName + ".xml"));
        new XmlSerializer(o.GetType()).Serialize((Stream) stream, o);
        stream.Close();
    }

    private void exportDBTable<T>(object data, string fileName)
    {
        this.debug("- export " + fileName + "...", false);
        FileStream stream = File.Create(Path.Combine(this.config_AppDataPath, fileName + ".xml"));
        new XmlSerializer(typeof(List<T>)).Serialize((Stream) stream, data);
        stream.Close();
    }

    private void formMain_Closed(object sender, FormClosedEventArgs e)
    {
        if (this.logWriter != null)
        {
            this.logWriter.Flush();
        }
    }

    private void formMain_Shown(object sender, EventArgs e)
    {
        this.l_Status_Text.Text = "Loading data files...";
        this.l_Status_Text.Refresh();
        this.parserFunctions = new ParserFunctions(this);
        this.columnAttrib = this.setTableAttributes();
        int index = -1;
        bool[] flagArray = this.checkConfiguration();
        ToolStripMenuItem[] itemArray1 = new ToolStripMenuItem[9];
        itemArray1[0] = this.tsmi_Attila;
        itemArray1[1] = this.tsmi_Rome2;
        itemArray1[2] = this.tsmi_Shogun2;
        itemArray1[3] = this.tsmi_Napoleon;
        itemArray1[4] = this.tsmi_Empire;
        itemArray1[5] = this.tsmi_Warhammer;
        itemArray1[6] = this.tsmi_Warhammer2;
        itemArray1[7] = this.tsmi_Britannia;
        itemArray1[8] = this.tsmi_ThreeKingdoms;
        ToolStripMenuItem[] itemArray = itemArray1;
        int num2 = -1;
        try
        {
            num2 = int.Parse(this.dic_properties["startUp.game"]);
            if ((num2 < 0) || (num2 > itemArray.Length))
            {
                throw new Exception();
            }
        }
        catch (Exception)
        {
            this.debug("- invalid startUp.game value, entry is ignored.", LogLevelType.warning, false);
        }
        for (int i = 0; i < flagArray.Length; i++)
        {
            itemArray[i].Enabled = flagArray[i];
            if (flagArray[i] && (index == -1))
            {
                index = i;
            }
        }
        if ((num2 > -1) && flagArray[num2])
        {
            index = num2;
        }
        this.tsmi_Game_Click(itemArray[index], null);
        this.debug("- first valid setting: " + index.ToString(), false);
        object[] args = new object[] { true };
        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.NonPublic | BindingFlags.Instance, null, this.dgv_ParserResult, args);
    }

    private List<int> getFactionIndices(string s, bool allAllowed)
    {
        List<int> list = new List<int>();
        if (s.Trim() != "-1")
        {
            char[] separator = new char[] { ';' };
            string[] strArray = s.Trim().Split(separator);
            int index = 0;
            while (true)
            {
                if (index >= strArray.Length)
                {
                    break;
                }
                string str = strArray[index];
                int item = -1;
                try
                {
                    item = Convert.ToInt32(str);
                    if (item < 0)
                    {
                        throw new Exception();
                    }
                    list.Add(item);
                }
                catch (Exception)
                {
                    this.l_Status_Text.Text = "invalid input at faction: " + str;
                    return null;
                }
                index++;
            }
        }
        else
        {
            if (!allAllowed)
            {
                this.l_Status_Text.Text = "invalid input at faction: '-1' not allowed for this function.";
                return null;
            }
            list = new List<int>(DataBase.dic_FactionIndexNameActive.Keys);
        }
        return list;
    }

    private int getObjectSize(object TestObject)
    {
        MemoryStream serializationStream = new MemoryStream();
        new BinaryFormatter().Serialize(serializationStream, TestObject);
        return serializationStream.ToArray().Length;
    }

    private List<int> getRegionIndices()
    {
        object obj1;
        List<int> list = new List<int>();
        Util_Region region = new Util_Region(this);
        if (string.IsNullOrEmpty(this.cb_EditRegions.Text.Trim()))
        {
            obj1 = null;
        }
        else
        {
            char[] separator = new char[] { ';' };
            obj1 = this.cb_EditRegions.Text.Trim().Split(separator);
        }
        string[] strArray = (string[]) obj1;
        if (strArray != null)
        {
            foreach (string str in strArray)
            {
                try
                {
                    int item = Convert.ToInt32(str);
                    if (!list.Contains(item))
                    {
                        list.Add(item);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        List<int> list2 = this.getFactionIndices(this.cb_EditFactions.Text, false);
        if (list2 != null)
        {
            int[] source = new int[list2.Count];
            int index = 0;
            while (true)
            {
                if (index >= source.Length)
                {
                    List<int> list1 = new List<int>();
                    int num3 = region.getRegionSize();
                    for (int i = 0; i < num3; i++)
                    {
                        Table_Data_Region region2 = region.createRegion(i, true);
                        if (source.Contains<int>(region2.controllerId) && !list.Contains(i))
                        {
                            list.Add(i);
                        }
                    }
                    break;
                }
                source[index] = DataBase.dic_FactionIndexId[list2[index]];
                index++;
            }
        }
        return list;
    }

    private Dictionary<T1, T2> importDBDictionary<T1, T2>(string fileName)
    {
        Dictionary<T1, T2> dictionary = new Dictionary<T1, T2>();
        foreach (KeyValuePair<T1, T2> pair in (SerializableDictionary<T1, T2>) new XmlSerializer(new SerializableDictionary<T1, T2>().GetType()).Deserialize(new StreamReader(this.config_AppDataPath + fileName + ".xml")))
        {
            T1 key = pair.Key;
            T2 local2 = pair.Value;
            dictionary.Add(key, local2);
        }
        return dictionary;
    }

    private List<T> importDBTable<T>(string fileName)
    {
        List<T> list;
        this.debug("- import db data from '" + fileName + "'...", false);
        try
        {
            StreamReader textReader = new StreamReader(this.config_AppDataPath + fileName + ".xml");
            textReader.Close();
            list = (List<T>) new XmlSerializer(typeof(List<T>)).Deserialize(textReader);
        }
        catch (Exception)
        {
            throw;
        }
        return list;
    }

    private void InitializeComponent()
    {
        this.components = new Container();
        ComponentResourceManager manager = new ComponentResourceManager(typeof(SaveParser.SaveParser));
        this.ms_Main = new MenuStrip();
        this.mi_File = new ToolStripMenuItem();
        this.tsmi_OpenFile = new ToolStripMenuItem();
        this.tsmi_SaveFile = new ToolStripMenuItem();
        this.tsmi_SaveFileAs = new ToolStripMenuItem();
        this.tss_File = new ToolStripSeparator();
        this.tsmi_FileType = new ToolStripMenuItem();
        this.tsmi_Multiplayer = new ToolStripMenuItem();
        this.mi_Game = new ToolStripMenuItem();
        this.tsmi_Warhammer2 = new ToolStripMenuItem();
        this.tsmi_Warhammer = new ToolStripMenuItem();
        this.tsmi_Attila = new ToolStripMenuItem();
        this.tsmi_Rome2 = new ToolStripMenuItem();
        this.tsmi_Shogun2 = new ToolStripMenuItem();
        this.tsmi_Napoleon = new ToolStripMenuItem();
        this.tsmi_Empire = new ToolStripMenuItem();
        this.tsmi_Britannia = new ToolStripMenuItem();
        this.tsmi_ThreeKingdoms = new ToolStripMenuItem();
        this.mi_Tools = new ToolStripMenuItem();
        this.tsmi_EditSF = new ToolStripMenuItem();
        this.tsmi_PFM = new ToolStripMenuItem();
        this.tsmi_SaveViewer = new ToolStripMenuItem();
        this.tss_Tools_1 = new ToolStripSeparator();
        this.tsmi_SavegameFiles = new ToolStripMenuItem();
        this.tsmi_SaveParserIni = new ToolStripMenuItem();
        this.tsmi_EditConfigFile = new ToolStripMenuItem();
        this.tsmi_RestartConfig = new ToolStripMenuItem();
        this.tsmi_OpenFile_Data = new ToolStripMenuItem();
        this.tsmi_OpenFile_CustomFile = new ToolStripMenuItem();
        this.tsmi_OpenFile_Names = new ToolStripMenuItem();
        this.tsmi_ExportSavegame = new ToolStripMenuItem();
        this.tsmi_ExportTable = new ToolStripMenuItem();
        this.tss_Tools_2 = new ToolStripSeparator();
        this.tsmi_EditSFDialog = new ToolStripMenuItem();
        this.tsmi_OpenBatchController = new ToolStripMenuItem();
        this.mi_Options = new ToolStripMenuItem();
        this.tsmi_FactionArrayIndex = new ToolStripMenuItem();
        this.tsmi_Options_ActiveFactions = new ToolStripMenuItem();
        this.tsmi_Options_RememberFactionValue = new ToolStripMenuItem();
        this.mi_Info = new ToolStripMenuItem();
        this.tsmi_Version = new ToolStripMenuItem();
        this.tsmi_ReadMe = new ToolStripMenuItem();
        this.mi_Admin = new ToolStripMenuItem();
        this.tsmi_DebugWindow = new ToolStripMenuItem();
        this.internalDBToolStripMenuItem = new ToolStripMenuItem();
        this.tsmi_ExportInternalDBTables = new ToolStripMenuItem();
        this.tsmi_InternalDBViewer = new ToolStripMenuItem();
        this.mi_Donation = new ToolStripMenuItem();
        this.tsmi_PayPal = new ToolStripMenuItem();
        this.openFileDialog_Savegame = new OpenFileDialog();
        this.cms_Table = new ContextMenuStrip(this.components);
        this.tsmi_JumpToEditSF = new ToolStripMenuItem();
        this.p_Status = new Panel();
        this.l_Status_Text = new Label();
        this.l_Status_Label = new Label();
        this.l_Status_GameTag = new Label();
        this.toolTip = new ToolTip(this.components);
        this.b_Global_MoneyDistribution = new Button();
        this.cb_Global_MoneyDistribution_Rebels = new CheckBox();
        this.b_OpenTableDialog_Factions = new Button();
        this.b_Reduce = new Button();
        this.b_Replenish = new Button();
        this.b_ResearchComplete = new Button();
        this.b_ResearchAddAllProjects = new Button();
        this.b_MovementPoints = new Button();
        this.b_ResetAgentAction = new Button();
        this.b_BirthYears = new Button();
        this.b_ConstructionComplete = new Button();
        this.b_Parse = new Button();
        this.tb_ParseFaction = new TextBox();
        this.b_FactionArrayIndex = new Button();
        this.b_Global_EnablePolitics = new Button();
        this.b_ProvinceSetHappiness = new Button();
        this.nup_Age = new NumericUpDown();
        this.b_ResetMovementPoints = new Button();
        this.cb_EditFactions = new ComboBox();
        this.cb_EditRegions = new ComboBox();
        this.cb_EditProvinces = new ComboBox();
        this.b_RemoveAlertTraits = new Button();
        this.b_Global_Climate = new Button();
        this.b_RecruitmentComplete = new Button();
        this.b_Map = new Button();
        this.b_Corruption_Clear = new Button();
        this.b_Global_SetUnitSizeToScale = new Button();
        this.saveFileDialog_Savegame = new SaveFileDialog();
        this.b_Reuse = new Button();
        this.tabPage_Edit = new TabPage();
        this.gb_Recruitment = new GroupBox();
        this.l_Rank = new Label();
        this.nup_EditRank = new NumericUpDown();
        this.gb_Province = new GroupBox();
        this.nup_HappinessValue = new NumericUpDown();
        this.l_HappinessValue = new Label();
        this.b_OpenTableDialog_Provinces = new Button();
        this.gb_Region = new GroupBox();
        this.b_OpenTableDialog_Regions = new Button();
        this.gb_Character = new GroupBox();
        this.l_Age = new Label();
        this.gb_Movement = new GroupBox();
        this.nup_MovementPoints = new NumericUpDown();
        this.l_MovementPoints = new Label();
        this.gb_Research = new GroupBox();
        this.b_ResearchAddSelectedProjects = new Button();
        this.l_ResearchComplete = new Label();
        this.gb_ArmyStrength = new GroupBox();
        this.l_UnitChange = new Label();
        this.gb_Parameter = new GroupBox();
        this.gb_Global_MoneyDistribution = new GroupBox();
        this.l_MoneyDistributionLimit = new Label();
        this.l_MoneyDistribution = new Label();
        this.nup_Global_MoneyDistributionLimit = new NumericUpDown();
        this.nup_Global_MoneyDistribution = new NumericUpDown();
        this.tabPage_Parser = new TabPage();
        this.dgv_ParserResult = new DataGridView();
        this.p_Control = new Panel();
        this.cb_Factions = new ComboBox();
        this.l_Filter = new Label();
        this.cb_Filter = new ComboBox();
        this.cb_DisplayTable = new ComboBox();
        this.l_DisplayTable = new Label();
        this.tabControl_Main = new TabControl();
        this.tabPage_Edit_Global = new TabPage();
        this.gb_Global_Army = new GroupBox();
        this.b_Global_MaxUnits = new Button();
        this.cb_Global_UnitSizeScale = new ComboBox();
        this.l_Global_UnitSizeScale = new Label();
        this.cb_Global_MaxUnits = new ComboBox();
        this.gb_Global_GameInfo = new GroupBox();
        this.tb_Global_CampaignTag = new TextBox();
        this.l_CampaignTag = new Label();
        this.gb_Global_GameSettings = new GroupBox();
        this.nup_Global_Climate = new NumericUpDown();
        this.b_Global_RemoveFamousBattleMarker = new Button();
        this.b_Global_TPY = new Button();
        this.nup_Global_TPY = new NumericUpDown();
        this.ms_Main.SuspendLayout();
        this.cms_Table.SuspendLayout();
        this.p_Status.SuspendLayout();
        this.nup_Age.BeginInit();
        this.tabPage_Edit.SuspendLayout();
        this.gb_Recruitment.SuspendLayout();
        this.nup_EditRank.BeginInit();
        this.gb_Province.SuspendLayout();
        this.nup_HappinessValue.BeginInit();
        this.gb_Region.SuspendLayout();
        this.gb_Character.SuspendLayout();
        this.gb_Movement.SuspendLayout();
        this.nup_MovementPoints.BeginInit();
        this.gb_Research.SuspendLayout();
        this.gb_ArmyStrength.SuspendLayout();
        this.gb_Parameter.SuspendLayout();
        this.gb_Global_MoneyDistribution.SuspendLayout();
        this.nup_Global_MoneyDistributionLimit.BeginInit();
        this.nup_Global_MoneyDistribution.BeginInit();
        this.tabPage_Parser.SuspendLayout();
        ((ISupportInitialize) this.dgv_ParserResult).BeginInit();
        this.p_Control.SuspendLayout();
        this.tabControl_Main.SuspendLayout();
        this.tabPage_Edit_Global.SuspendLayout();
        this.gb_Global_Army.SuspendLayout();
        this.gb_Global_GameInfo.SuspendLayout();
        this.gb_Global_GameSettings.SuspendLayout();
        this.nup_Global_Climate.BeginInit();
        this.nup_Global_TPY.BeginInit();
        base.SuspendLayout();
        this.ms_Main.ImageScalingSize = new Size(20, 20);
        ToolStripItem[] toolStripItems = new ToolStripItem[] { this.mi_File, this.mi_Game, this.mi_Tools, this.mi_Options, this.mi_Info, this.mi_Admin, this.mi_Donation };
        this.ms_Main.Items.AddRange(toolStripItems);
        manager.ApplyResources(this.ms_Main, "ms_Main");
        this.ms_Main.Name = "ms_Main";
        ToolStripItem[] itemArray2 = new ToolStripItem[] { this.tsmi_OpenFile, this.tsmi_SaveFile, this.tsmi_SaveFileAs, this.tss_File, this.tsmi_FileType, this.tsmi_Multiplayer };
        this.mi_File.DropDownItems.AddRange(itemArray2);
        this.mi_File.Name = "mi_File";
        manager.ApplyResources(this.mi_File, "mi_File");
        manager.ApplyResources(this.tsmi_OpenFile, "tsmi_OpenFile");
        this.tsmi_OpenFile.Name = "tsmi_OpenFile";
        this.tsmi_OpenFile.Click += new EventHandler(this.tsmi_OpenFile_Click);
        manager.ApplyResources(this.tsmi_SaveFile, "tsmi_SaveFile");
        this.tsmi_SaveFile.Name = "tsmi_SaveFile";
        this.tsmi_SaveFile.Click += new EventHandler(this.tsmi_SaveFile_Click);
        manager.ApplyResources(this.tsmi_SaveFileAs, "tsmi_SaveFileAs");
        this.tsmi_SaveFileAs.Name = "tsmi_SaveFileAs";
        this.tsmi_SaveFileAs.Click += new EventHandler(this.tsmi_SaveFileAs_Click);
        this.tss_File.Name = "tss_File";
        manager.ApplyResources(this.tss_File, "tss_File");
        manager.ApplyResources(this.tsmi_FileType, "tsmi_FileType");
        this.tsmi_FileType.Name = "tsmi_FileType";
        this.tsmi_FileType.Click += new EventHandler(this.tsmi_FileType_Click);
        this.tsmi_Multiplayer.CheckOnClick = true;
        manager.ApplyResources(this.tsmi_Multiplayer, "tsmi_Multiplayer");
        this.tsmi_Multiplayer.Name = "tsmi_Multiplayer";
        this.tsmi_Multiplayer.Click += new EventHandler(this.tsmi_Mulitplayer_Click);
        ToolStripItem[] itemArray3 = new ToolStripItem[9];
        itemArray3[0] = this.tsmi_Warhammer2;
        itemArray3[1] = this.tsmi_Warhammer;
        itemArray3[2] = this.tsmi_Attila;
        itemArray3[3] = this.tsmi_Rome2;
        itemArray3[4] = this.tsmi_Shogun2;
        itemArray3[5] = this.tsmi_Napoleon;
        itemArray3[6] = this.tsmi_Empire;
        itemArray3[7] = this.tsmi_Britannia;
        itemArray3[8] = this.tsmi_ThreeKingdoms;
        this.mi_Game.DropDownItems.AddRange(itemArray3);
        this.mi_Game.Name = "mi_Game";
        manager.ApplyResources(this.mi_Game, "mi_Game");
        manager.ApplyResources(this.tsmi_Warhammer2, "tsmi_Warhammer2");
        this.tsmi_Warhammer2.Name = "tsmi_Warhammer2";
        this.tsmi_Warhammer2.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Warhammer, "tsmi_Warhammer");
        this.tsmi_Warhammer.Name = "tsmi_Warhammer";
        this.tsmi_Warhammer.Click += new EventHandler(this.tsmi_Game_Click);
        this.tsmi_Attila.Checked = true;
        this.tsmi_Attila.CheckState = CheckState.Checked;
        manager.ApplyResources(this.tsmi_Attila, "tsmi_Attila");
        this.tsmi_Attila.Name = "tsmi_Attila";
        this.tsmi_Attila.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Rome2, "tsmi_Rome2");
        this.tsmi_Rome2.Name = "tsmi_Rome2";
        this.tsmi_Rome2.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Shogun2, "tsmi_Shogun2");
        this.tsmi_Shogun2.Name = "tsmi_Shogun2";
        this.tsmi_Shogun2.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Napoleon, "tsmi_Napoleon");
        this.tsmi_Napoleon.Name = "tsmi_Napoleon";
        this.tsmi_Napoleon.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Empire, "tsmi_Empire");
        this.tsmi_Empire.Name = "tsmi_Empire";
        this.tsmi_Empire.Click += new EventHandler(this.tsmi_Game_Click);
        manager.ApplyResources(this.tsmi_Britannia, "tsmi_Britannia");
        this.tsmi_Britannia.Name = "tsmi_Britannia";
        this.tsmi_Britannia.Click += new EventHandler(this.tsmi_Game_Click);
        this.tsmi_ThreeKingdoms.Name = "tsmi_ThreeKingdoms";
        manager.ApplyResources(this.tsmi_ThreeKingdoms, "tsmi_ThreeKingdoms");
        this.tsmi_ThreeKingdoms.Click += new EventHandler(this.tsmi_Game_Click);
        ToolStripItem[] itemArray4 = new ToolStripItem[12];
        itemArray4[0] = this.tsmi_EditSF;
        itemArray4[1] = this.tsmi_PFM;
        itemArray4[2] = this.tsmi_SaveViewer;
        itemArray4[3] = this.tss_Tools_1;
        itemArray4[4] = this.tsmi_SavegameFiles;
        itemArray4[5] = this.tsmi_SaveParserIni;
        itemArray4[6] = this.tsmi_OpenFile_Data;
        itemArray4[7] = this.tsmi_ExportSavegame;
        itemArray4[8] = this.tsmi_ExportTable;
        itemArray4[9] = this.tss_Tools_2;
        itemArray4[10] = this.tsmi_EditSFDialog;
        itemArray4[11] = this.tsmi_OpenBatchController;
        this.mi_Tools.DropDownItems.AddRange(itemArray4);
        this.mi_Tools.Name = "mi_Tools";
        manager.ApplyResources(this.mi_Tools, "mi_Tools");
        manager.ApplyResources(this.tsmi_EditSF, "tsmi_EditSF");
        this.tsmi_EditSF.Name = "tsmi_EditSF";
        this.tsmi_EditSF.Click += new EventHandler(this.tsmi_EditSF_Click);
        manager.ApplyResources(this.tsmi_PFM, "tsmi_PFM");
        this.tsmi_PFM.Name = "tsmi_PFM";
        this.tsmi_PFM.Click += new EventHandler(this.tsmi_PFM_Click);
        manager.ApplyResources(this.tsmi_SaveViewer, "tsmi_SaveViewer");
        this.tsmi_SaveViewer.Name = "tsmi_SaveViewer";
        this.tsmi_SaveViewer.Click += new EventHandler(this.tsmi_SaveViewer_Click);
        this.tss_Tools_1.Name = "tss_Tools_1";
        manager.ApplyResources(this.tss_Tools_1, "tss_Tools_1");
        manager.ApplyResources(this.tsmi_SavegameFiles, "tsmi_SavegameFiles");
        this.tsmi_SavegameFiles.Name = "tsmi_SavegameFiles";
        this.tsmi_SavegameFiles.Click += new EventHandler(this.tsmi_SavegameFiles_Click);
        ToolStripItem[] itemArray5 = new ToolStripItem[] { this.tsmi_EditConfigFile, this.tsmi_RestartConfig };
        this.tsmi_SaveParserIni.DropDownItems.AddRange(itemArray5);
        manager.ApplyResources(this.tsmi_SaveParserIni, "tsmi_SaveParserIni");
        this.tsmi_SaveParserIni.Name = "tsmi_SaveParserIni";
        manager.ApplyResources(this.tsmi_EditConfigFile, "tsmi_EditConfigFile");
        this.tsmi_EditConfigFile.Name = "tsmi_EditConfigFile";
        this.tsmi_EditConfigFile.Click += new EventHandler(this.tsmi_EditConfigFile_Click);
        manager.ApplyResources(this.tsmi_RestartConfig, "tsmi_RestartConfig");
        this.tsmi_RestartConfig.Name = "tsmi_RestartConfig";
        this.tsmi_RestartConfig.Click += new EventHandler(this.tsmi_RestartConfig_Click);
        ToolStripItem[] itemArray6 = new ToolStripItem[] { this.tsmi_OpenFile_CustomFile, this.tsmi_OpenFile_Names };
        this.tsmi_OpenFile_Data.DropDownItems.AddRange(itemArray6);
        this.tsmi_OpenFile_Data.Name = "tsmi_OpenFile_Data";
        manager.ApplyResources(this.tsmi_OpenFile_Data, "tsmi_OpenFile_Data");
        manager.ApplyResources(this.tsmi_OpenFile_CustomFile, "tsmi_OpenFile_CustomFile");
        this.tsmi_OpenFile_CustomFile.Name = "tsmi_OpenFile_CustomFile";
        this.tsmi_OpenFile_CustomFile.Click += new EventHandler(this.tsmi_OpenFile_CustomFile_Click);
        manager.ApplyResources(this.tsmi_OpenFile_Names, "tsmi_OpenFile_Names");
        this.tsmi_OpenFile_Names.Name = "tsmi_OpenFile_Names";
        this.tsmi_OpenFile_Names.Click += new EventHandler(this.tsmi_OpenFile_Data_Click);
        manager.ApplyResources(this.tsmi_ExportSavegame, "tsmi_ExportSavegame");
        this.tsmi_ExportSavegame.Name = "tsmi_ExportSavegame";
        this.tsmi_ExportSavegame.Click += new EventHandler(this.tsmi_ExportSavegame_Click);
        manager.ApplyResources(this.tsmi_ExportTable, "tsmi_ExportTable");
        this.tsmi_ExportTable.Name = "tsmi_ExportTable";
        this.tsmi_ExportTable.Click += new EventHandler(this.tsmi_ExportTable_Click);
        this.tss_Tools_2.Name = "tss_Tools_2";
        manager.ApplyResources(this.tss_Tools_2, "tss_Tools_2");
        manager.ApplyResources(this.tsmi_EditSFDialog, "tsmi_EditSFDialog");
        this.tsmi_EditSFDialog.Name = "tsmi_EditSFDialog";
        this.tsmi_EditSFDialog.Click += new EventHandler(this.tsmi_EditSFDialog_Click);
        manager.ApplyResources(this.tsmi_OpenBatchController, "tsmi_OpenBatchController");
        this.tsmi_OpenBatchController.Name = "tsmi_OpenBatchController";
        this.tsmi_OpenBatchController.Click += new EventHandler(this.tsmi_OpenBatchController_Click);
        ToolStripItem[] itemArray7 = new ToolStripItem[] { this.tsmi_FactionArrayIndex };
        this.mi_Options.DropDownItems.AddRange(itemArray7);
        this.mi_Options.Name = "mi_Options";
        manager.ApplyResources(this.mi_Options, "mi_Options");
        ToolStripItem[] itemArray8 = new ToolStripItem[] { this.tsmi_Options_ActiveFactions, this.tsmi_Options_RememberFactionValue };
        this.tsmi_FactionArrayIndex.DropDownItems.AddRange(itemArray8);
        this.tsmi_FactionArrayIndex.Name = "tsmi_FactionArrayIndex";
        manager.ApplyResources(this.tsmi_FactionArrayIndex, "tsmi_FactionArrayIndex");
        manager.ApplyResources(this.tsmi_Options_ActiveFactions, "tsmi_Options_ActiveFactions");
        this.tsmi_Options_ActiveFactions.Name = "tsmi_Options_ActiveFactions";
        this.tsmi_Options_RememberFactionValue.Checked = true;
        this.tsmi_Options_RememberFactionValue.CheckOnClick = true;
        this.tsmi_Options_RememberFactionValue.CheckState = CheckState.Checked;
        this.tsmi_Options_RememberFactionValue.Name = "tsmi_Options_RememberFactionValue";
        manager.ApplyResources(this.tsmi_Options_RememberFactionValue, "tsmi_Options_RememberFactionValue");
        ToolStripItem[] itemArray9 = new ToolStripItem[] { this.tsmi_Version, this.tsmi_ReadMe };
        this.mi_Info.DropDownItems.AddRange(itemArray9);
        this.mi_Info.Name = "mi_Info";
        manager.ApplyResources(this.mi_Info, "mi_Info");
        manager.ApplyResources(this.tsmi_Version, "tsmi_Version");
        this.tsmi_Version.Name = "tsmi_Version";
        manager.ApplyResources(this.tsmi_ReadMe, "tsmi_ReadMe");
        this.tsmi_ReadMe.Name = "tsmi_ReadMe";
        this.tsmi_ReadMe.Click += new EventHandler(this.tsmi_ReadMe_Click);
        ToolStripItem[] itemArray10 = new ToolStripItem[] { this.tsmi_DebugWindow, this.internalDBToolStripMenuItem };
        this.mi_Admin.DropDownItems.AddRange(itemArray10);
        this.mi_Admin.Name = "mi_Admin";
        manager.ApplyResources(this.mi_Admin, "mi_Admin");
        manager.ApplyResources(this.tsmi_DebugWindow, "tsmi_DebugWindow");
        this.tsmi_DebugWindow.Name = "tsmi_DebugWindow";
        this.tsmi_DebugWindow.Click += new EventHandler(this.tsmi_DebugWindow_Click);
        ToolStripItem[] itemArray11 = new ToolStripItem[] { this.tsmi_ExportInternalDBTables, this.tsmi_InternalDBViewer };
        this.internalDBToolStripMenuItem.DropDownItems.AddRange(itemArray11);
        manager.ApplyResources(this.internalDBToolStripMenuItem, "internalDBToolStripMenuItem");
        this.internalDBToolStripMenuItem.Name = "internalDBToolStripMenuItem";
        manager.ApplyResources(this.tsmi_ExportInternalDBTables, "tsmi_ExportInternalDBTables");
        this.tsmi_ExportInternalDBTables.Name = "tsmi_ExportInternalDBTables";
        this.tsmi_ExportInternalDBTables.Click += new EventHandler(this.tsmi_ExportInternalDBTables_Click);
        manager.ApplyResources(this.tsmi_InternalDBViewer, "tsmi_InternalDBViewer");
        this.tsmi_InternalDBViewer.Name = "tsmi_InternalDBViewer";
        this.tsmi_InternalDBViewer.Click += new EventHandler(this.tsmi_InternalDBViewer_Click);
        ToolStripItem[] itemArray12 = new ToolStripItem[] { this.tsmi_PayPal };
        this.mi_Donation.DropDownItems.AddRange(itemArray12);
        this.mi_Donation.Name = "mi_Donation";
        manager.ApplyResources(this.mi_Donation, "mi_Donation");
        manager.ApplyResources(this.tsmi_PayPal, "tsmi_PayPal");
        this.tsmi_PayPal.Name = "tsmi_PayPal";
        this.tsmi_PayPal.Click += new EventHandler(this.tsmi_PayPal_Click);
        this.openFileDialog_Savegame.FileName = "*.save";
        this.cms_Table.ImageScalingSize = new Size(20, 20);
        ToolStripItem[] itemArray13 = new ToolStripItem[] { this.tsmi_JumpToEditSF };
        this.cms_Table.Items.AddRange(itemArray13);
        this.cms_Table.Name = "cms_Table";
        manager.ApplyResources(this.cms_Table, "cms_Table");
        this.tsmi_JumpToEditSF.Name = "tsmi_JumpToEditSF";
        manager.ApplyResources(this.tsmi_JumpToEditSF, "tsmi_JumpToEditSF");
        this.tsmi_JumpToEditSF.Click += new EventHandler(this.tsmi_JumpToEditSF_Click);
        this.p_Status.BorderStyle = BorderStyle.Fixed3D;
        this.p_Status.Controls.Add(this.l_Status_Text);
        this.p_Status.Controls.Add(this.l_Status_Label);
        this.p_Status.Controls.Add(this.l_Status_GameTag);
        manager.ApplyResources(this.p_Status, "p_Status");
        this.p_Status.Name = "p_Status";
        this.toolTip.SetToolTip(this.p_Status, manager.GetString("p_Status.ToolTip"));
        manager.ApplyResources(this.l_Status_Text, "l_Status_Text");
        this.l_Status_Text.Name = "l_Status_Text";
        manager.ApplyResources(this.l_Status_Label, "l_Status_Label");
        this.l_Status_Label.Name = "l_Status_Label";
        this.l_Status_GameTag.BorderStyle = BorderStyle.Fixed3D;
        manager.ApplyResources(this.l_Status_GameTag, "l_Status_GameTag");
        this.l_Status_GameTag.Name = "l_Status_GameTag";
        this.toolTip.SetToolTip(this.l_Status_GameTag, manager.GetString("l_Status_GameTag.ToolTip"));
        this.toolTip.ShowAlways = true;
        manager.ApplyResources(this.b_Global_MoneyDistribution, "b_Global_MoneyDistribution");
        this.b_Global_MoneyDistribution.Name = "b_Global_MoneyDistribution";
        this.toolTip.SetToolTip(this.b_Global_MoneyDistribution, manager.GetString("b_Global_MoneyDistribution.ToolTip"));
        this.b_Global_MoneyDistribution.UseVisualStyleBackColor = true;
        this.b_Global_MoneyDistribution.Click += new EventHandler(this.b_Global_MoneyDistribution_Click);
        manager.ApplyResources(this.cb_Global_MoneyDistribution_Rebels, "cb_Global_MoneyDistribution_Rebels");
        this.cb_Global_MoneyDistribution_Rebels.Name = "cb_Global_MoneyDistribution_Rebels";
        this.toolTip.SetToolTip(this.cb_Global_MoneyDistribution_Rebels, manager.GetString("cb_Global_MoneyDistribution_Rebels.ToolTip"));
        this.cb_Global_MoneyDistribution_Rebels.UseVisualStyleBackColor = true;
        manager.ApplyResources(this.b_OpenTableDialog_Factions, "b_OpenTableDialog_Factions");
        this.b_OpenTableDialog_Factions.Name = "b_OpenTableDialog_Factions";
        this.toolTip.SetToolTip(this.b_OpenTableDialog_Factions, manager.GetString("b_OpenTableDialog_Factions.ToolTip"));
        this.b_OpenTableDialog_Factions.UseVisualStyleBackColor = true;
        this.b_OpenTableDialog_Factions.Click += new EventHandler(this.b_OpenTableDialog_Factions_Click);
        manager.ApplyResources(this.b_Reduce, "b_Reduce");
        this.b_Reduce.Name = "b_Reduce";
        this.toolTip.SetToolTip(this.b_Reduce, manager.GetString("b_Reduce.ToolTip"));
        this.b_Reduce.UseVisualStyleBackColor = true;
        this.b_Reduce.Click += new EventHandler(this.b_Reduce_Click);
        manager.ApplyResources(this.b_Replenish, "b_Replenish");
        this.b_Replenish.Name = "b_Replenish";
        this.toolTip.SetToolTip(this.b_Replenish, manager.GetString("b_Replenish.ToolTip"));
        this.b_Replenish.UseVisualStyleBackColor = true;
        this.b_Replenish.Click += new EventHandler(this.b_Replenish_Click);
        manager.ApplyResources(this.b_ResearchComplete, "b_ResearchComplete");
        this.b_ResearchComplete.Name = "b_ResearchComplete";
        this.toolTip.SetToolTip(this.b_ResearchComplete, manager.GetString("b_ResearchComplete.ToolTip"));
        this.b_ResearchComplete.UseVisualStyleBackColor = true;
        this.b_ResearchComplete.Click += new EventHandler(this.b_ResearchComplete_Click);
        manager.ApplyResources(this.b_ResearchAddAllProjects, "b_ResearchAddAllProjects");
        this.b_ResearchAddAllProjects.Name = "b_ResearchAddAllProjects";
        this.toolTip.SetToolTip(this.b_ResearchAddAllProjects, manager.GetString("b_ResearchAddAllProjects.ToolTip"));
        this.b_ResearchAddAllProjects.UseVisualStyleBackColor = true;
        this.b_ResearchAddAllProjects.Click += new EventHandler(this.b_ResearchAddAllProjects_Click);
        manager.ApplyResources(this.b_MovementPoints, "b_MovementPoints");
        this.b_MovementPoints.Name = "b_MovementPoints";
        this.toolTip.SetToolTip(this.b_MovementPoints, manager.GetString("b_MovementPoints.ToolTip"));
        this.b_MovementPoints.UseVisualStyleBackColor = true;
        this.b_MovementPoints.Click += new EventHandler(this.b_MovementPoints_Click);
        manager.ApplyResources(this.b_ResetAgentAction, "b_ResetAgentAction");
        this.b_ResetAgentAction.Name = "b_ResetAgentAction";
        this.toolTip.SetToolTip(this.b_ResetAgentAction, manager.GetString("b_ResetAgentAction.ToolTip"));
        this.b_ResetAgentAction.UseVisualStyleBackColor = true;
        this.b_ResetAgentAction.Click += new EventHandler(this.b_ResetAgentAction_Click);
        manager.ApplyResources(this.b_BirthYears, "b_BirthYears");
        this.b_BirthYears.Name = "b_BirthYears";
        this.toolTip.SetToolTip(this.b_BirthYears, manager.GetString("b_BirthYears.ToolTip"));
        this.b_BirthYears.UseVisualStyleBackColor = true;
        this.b_BirthYears.Click += new EventHandler(this.b_BirthYears_Click);
        manager.ApplyResources(this.b_ConstructionComplete, "b_ConstructionComplete");
        this.b_ConstructionComplete.Name = "b_ConstructionComplete";
        this.toolTip.SetToolTip(this.b_ConstructionComplete, manager.GetString("b_ConstructionComplete.ToolTip"));
        this.b_ConstructionComplete.UseVisualStyleBackColor = true;
        this.b_ConstructionComplete.Click += new EventHandler(this.b_ConstructionComplete_Click);
        manager.ApplyResources(this.b_Parse, "b_Parse");
        this.b_Parse.Name = "b_Parse";
        this.toolTip.SetToolTip(this.b_Parse, manager.GetString("b_Parse.ToolTip"));
        this.b_Parse.UseVisualStyleBackColor = true;
        this.b_Parse.Click += new EventHandler(this.b_Parse_Click);
        manager.ApplyResources(this.tb_ParseFaction, "tb_ParseFaction");
        this.tb_ParseFaction.Name = "tb_ParseFaction";
        this.toolTip.SetToolTip(this.tb_ParseFaction, manager.GetString("tb_ParseFaction.ToolTip"));
        manager.ApplyResources(this.b_FactionArrayIndex, "b_FactionArrayIndex");
        this.b_FactionArrayIndex.Name = "b_FactionArrayIndex";
        this.toolTip.SetToolTip(this.b_FactionArrayIndex, manager.GetString("b_FactionArrayIndex.ToolTip"));
        this.b_FactionArrayIndex.UseVisualStyleBackColor = true;
        this.b_FactionArrayIndex.Click += new EventHandler(this.b_FactionArrayIndex_Click);
        manager.ApplyResources(this.b_Global_EnablePolitics, "b_Global_EnablePolitics");
        this.b_Global_EnablePolitics.Name = "b_Global_EnablePolitics";
        this.toolTip.SetToolTip(this.b_Global_EnablePolitics, manager.GetString("b_Global_EnablePolitics.ToolTip"));
        this.b_Global_EnablePolitics.UseVisualStyleBackColor = true;
        this.b_Global_EnablePolitics.Click += new EventHandler(this.b_Global_EnablePolitics_Click);
        manager.ApplyResources(this.b_ProvinceSetHappiness, "b_ProvinceSetHappiness");
        this.b_ProvinceSetHappiness.Name = "b_ProvinceSetHappiness";
        this.toolTip.SetToolTip(this.b_ProvinceSetHappiness, manager.GetString("b_ProvinceSetHappiness.ToolTip"));
        this.b_ProvinceSetHappiness.UseVisualStyleBackColor = true;
        this.b_ProvinceSetHappiness.Click += new EventHandler(this.b_ProvinceSetHappiness_Click);
        manager.ApplyResources(this.nup_Age, "nup_Age");
        int[] bits = new int[4];
        bits[0] = 0x5dc;
        this.nup_Age.Maximum = new decimal(bits);
        this.nup_Age.Name = "nup_Age";
        this.toolTip.SetToolTip(this.nup_Age, manager.GetString("nup_Age.ToolTip"));
        int[] numArray2 = new int[4];
        numArray2[0] = 20;
        this.nup_Age.Value = new decimal(numArray2);
        manager.ApplyResources(this.b_ResetMovementPoints, "b_ResetMovementPoints");
        this.b_ResetMovementPoints.Name = "b_ResetMovementPoints";
        this.toolTip.SetToolTip(this.b_ResetMovementPoints, manager.GetString("b_ResetMovementPoints.ToolTip"));
        this.b_ResetMovementPoints.UseVisualStyleBackColor = true;
        this.b_ResetMovementPoints.Click += new EventHandler(this.b_MovementPoints_Click);
        this.cb_EditFactions.FormattingEnabled = true;
        manager.ApplyResources(this.cb_EditFactions, "cb_EditFactions");
        this.cb_EditFactions.Name = "cb_EditFactions";
        this.toolTip.SetToolTip(this.cb_EditFactions, manager.GetString("cb_EditFactions.ToolTip"));
        this.cb_EditRegions.FormattingEnabled = true;
        manager.ApplyResources(this.cb_EditRegions, "cb_EditRegions");
        this.cb_EditRegions.Name = "cb_EditRegions";
        this.toolTip.SetToolTip(this.cb_EditRegions, manager.GetString("cb_EditRegions.ToolTip"));
        this.cb_EditProvinces.FormattingEnabled = true;
        manager.ApplyResources(this.cb_EditProvinces, "cb_EditProvinces");
        this.cb_EditProvinces.Name = "cb_EditProvinces";
        this.toolTip.SetToolTip(this.cb_EditProvinces, manager.GetString("cb_EditProvinces.ToolTip"));
        manager.ApplyResources(this.b_RemoveAlertTraits, "b_RemoveAlertTraits");
        this.b_RemoveAlertTraits.Name = "b_RemoveAlertTraits";
        this.toolTip.SetToolTip(this.b_RemoveAlertTraits, manager.GetString("b_RemoveAlertTraits.ToolTip"));
        this.b_RemoveAlertTraits.UseVisualStyleBackColor = true;
        this.b_RemoveAlertTraits.Click += new EventHandler(this.b_RemoveAlertTraits_Click);
        manager.ApplyResources(this.b_Global_Climate, "b_Global_Climate");
        this.b_Global_Climate.Name = "b_Global_Climate";
        this.toolTip.SetToolTip(this.b_Global_Climate, manager.GetString("b_Global_Climate.ToolTip"));
        this.b_Global_Climate.UseVisualStyleBackColor = true;
        this.b_Global_Climate.Click += new EventHandler(this.b_Global_Climate_Click);
        manager.ApplyResources(this.b_RecruitmentComplete, "b_RecruitmentComplete");
        this.b_RecruitmentComplete.Name = "b_RecruitmentComplete";
        this.toolTip.SetToolTip(this.b_RecruitmentComplete, manager.GetString("b_RecruitmentComplete.ToolTip"));
        this.b_RecruitmentComplete.UseVisualStyleBackColor = true;
        this.b_RecruitmentComplete.Click += new EventHandler(this.b_RecruitmentComplete_Click);
        manager.ApplyResources(this.b_Map, "b_Map");
        this.b_Map.Name = "b_Map";
        this.toolTip.SetToolTip(this.b_Map, manager.GetString("b_Map.ToolTip"));
        this.b_Map.UseVisualStyleBackColor = true;
        this.b_Map.Click += new EventHandler(this.b_Map_Click);
        manager.ApplyResources(this.b_Corruption_Clear, "b_Corruption_Clear");
        this.b_Corruption_Clear.Name = "b_Corruption_Clear";
        this.toolTip.SetToolTip(this.b_Corruption_Clear, manager.GetString("b_Corruption_Clear.ToolTip"));
        this.b_Corruption_Clear.UseVisualStyleBackColor = true;
        this.b_Corruption_Clear.Click += new EventHandler(this.b_Corruption_Clear_Click);
        manager.ApplyResources(this.b_Global_SetUnitSizeToScale, "b_Global_SetUnitSizeToScale");
        this.b_Global_SetUnitSizeToScale.Name = "b_Global_SetUnitSizeToScale";
        this.toolTip.SetToolTip(this.b_Global_SetUnitSizeToScale, manager.GetString("b_Global_SetUnitSizeToScale.ToolTip"));
        this.b_Global_SetUnitSizeToScale.UseVisualStyleBackColor = true;
        this.b_Global_SetUnitSizeToScale.Click += new EventHandler(this.b_Global_SetUnitSizeToScale_Click);
        this.saveFileDialog_Savegame.FileName = "*.save";
        manager.ApplyResources(this.b_Reuse, "b_Reuse");
        this.b_Reuse.Name = "b_Reuse";
        manager.ApplyResources(this.tabPage_Edit, "tabPage_Edit");
        this.tabPage_Edit.Controls.Add(this.b_Map);
        this.tabPage_Edit.Controls.Add(this.gb_Recruitment);
        this.tabPage_Edit.Controls.Add(this.gb_Province);
        this.tabPage_Edit.Controls.Add(this.gb_Region);
        this.tabPage_Edit.Controls.Add(this.gb_Character);
        this.tabPage_Edit.Controls.Add(this.gb_Movement);
        this.tabPage_Edit.Controls.Add(this.gb_Research);
        this.tabPage_Edit.Controls.Add(this.gb_ArmyStrength);
        this.tabPage_Edit.Controls.Add(this.gb_Parameter);
        this.tabPage_Edit.Name = "tabPage_Edit";
        this.tabPage_Edit.UseVisualStyleBackColor = true;
        this.gb_Recruitment.Controls.Add(this.b_RecruitmentComplete);
        this.gb_Recruitment.Controls.Add(this.l_Rank);
        this.gb_Recruitment.Controls.Add(this.nup_EditRank);
        manager.ApplyResources(this.gb_Recruitment, "gb_Recruitment");
        this.gb_Recruitment.Name = "gb_Recruitment";
        this.gb_Recruitment.TabStop = false;
        manager.ApplyResources(this.l_Rank, "l_Rank");
        this.l_Rank.Name = "l_Rank";
        manager.ApplyResources(this.nup_EditRank, "nup_EditRank");
        int[] numArray3 = new int[4];
        numArray3[0] = 9;
        this.nup_EditRank.Maximum = new decimal(numArray3);
        int[] numArray4 = new int[4];
        numArray4[0] = 1;
        numArray4[3] = -2_147_483_648;
        this.nup_EditRank.Minimum = new decimal(numArray4);
        this.nup_EditRank.Name = "nup_EditRank";
        int[] numArray5 = new int[4];
        numArray5[0] = 1;
        numArray5[3] = -2_147_483_648;
        this.nup_EditRank.Value = new decimal(numArray5);
        this.gb_Province.Controls.Add(this.cb_EditProvinces);
        this.gb_Province.Controls.Add(this.nup_HappinessValue);
        this.gb_Province.Controls.Add(this.l_HappinessValue);
        this.gb_Province.Controls.Add(this.b_OpenTableDialog_Provinces);
        this.gb_Province.Controls.Add(this.b_ProvinceSetHappiness);
        manager.ApplyResources(this.gb_Province, "gb_Province");
        this.gb_Province.Name = "gb_Province";
        this.gb_Province.TabStop = false;
        manager.ApplyResources(this.nup_HappinessValue, "nup_HappinessValue");
        int[] numArray6 = new int[4];
        numArray6[0] = 100;
        numArray6[3] = -2_147_483_648;
        this.nup_HappinessValue.Minimum = new decimal(numArray6);
        this.nup_HappinessValue.Name = "nup_HappinessValue";
        int[] numArray7 = new int[4];
        numArray7[0] = 100;
        this.nup_HappinessValue.Value = new decimal(numArray7);
        manager.ApplyResources(this.l_HappinessValue, "l_HappinessValue");
        this.l_HappinessValue.Name = "l_HappinessValue";
        manager.ApplyResources(this.b_OpenTableDialog_Provinces, "b_OpenTableDialog_Provinces");
        this.b_OpenTableDialog_Provinces.Name = "b_OpenTableDialog_Provinces";
        this.b_OpenTableDialog_Provinces.UseVisualStyleBackColor = true;
        this.b_OpenTableDialog_Provinces.Click += new EventHandler(this.b_OpenTableDialog_Provinces_Click);
        this.gb_Region.Controls.Add(this.b_Corruption_Clear);
        this.gb_Region.Controls.Add(this.b_OpenTableDialog_Regions);
        this.gb_Region.Controls.Add(this.cb_EditRegions);
        this.gb_Region.Controls.Add(this.b_ConstructionComplete);
        manager.ApplyResources(this.gb_Region, "gb_Region");
        this.gb_Region.Name = "gb_Region";
        this.gb_Region.TabStop = false;
        manager.ApplyResources(this.b_OpenTableDialog_Regions, "b_OpenTableDialog_Regions");
        this.b_OpenTableDialog_Regions.Name = "b_OpenTableDialog_Regions";
        this.b_OpenTableDialog_Regions.UseVisualStyleBackColor = true;
        this.b_OpenTableDialog_Regions.Click += new EventHandler(this.b_OpenTableDialog_Regions_Click);
        this.gb_Character.Controls.Add(this.b_RemoveAlertTraits);
        this.gb_Character.Controls.Add(this.l_Age);
        this.gb_Character.Controls.Add(this.b_BirthYears);
        this.gb_Character.Controls.Add(this.nup_Age);
        manager.ApplyResources(this.gb_Character, "gb_Character");
        this.gb_Character.Name = "gb_Character";
        this.gb_Character.TabStop = false;
        manager.ApplyResources(this.l_Age, "l_Age");
        this.l_Age.Name = "l_Age";
        this.gb_Movement.Controls.Add(this.b_ResetMovementPoints);
        this.gb_Movement.Controls.Add(this.nup_MovementPoints);
        this.gb_Movement.Controls.Add(this.b_ResetAgentAction);
        this.gb_Movement.Controls.Add(this.b_MovementPoints);
        this.gb_Movement.Controls.Add(this.l_MovementPoints);
        manager.ApplyResources(this.gb_Movement, "gb_Movement");
        this.gb_Movement.Name = "gb_Movement";
        this.gb_Movement.TabStop = false;
        manager.ApplyResources(this.nup_MovementPoints, "nup_MovementPoints");
        int[] numArray8 = new int[4];
        numArray8[0] = 0x1_86a0;
        this.nup_MovementPoints.Maximum = new decimal(numArray8);
        this.nup_MovementPoints.Name = "nup_MovementPoints";
        int[] numArray9 = new int[4];
        numArray9[0] = 0x1f40;
        this.nup_MovementPoints.Value = new decimal(numArray9);
        manager.ApplyResources(this.l_MovementPoints, "l_MovementPoints");
        this.l_MovementPoints.ForeColor = SystemColors.Highlight;
        this.l_MovementPoints.Name = "l_MovementPoints";
        this.gb_Research.Controls.Add(this.b_ResearchAddSelectedProjects);
        this.gb_Research.Controls.Add(this.b_ResearchAddAllProjects);
        this.gb_Research.Controls.Add(this.b_ResearchComplete);
        this.gb_Research.Controls.Add(this.l_ResearchComplete);
        manager.ApplyResources(this.gb_Research, "gb_Research");
        this.gb_Research.Name = "gb_Research";
        this.gb_Research.TabStop = false;
        manager.ApplyResources(this.b_ResearchAddSelectedProjects, "b_ResearchAddSelectedProjects");
        this.b_ResearchAddSelectedProjects.Name = "b_ResearchAddSelectedProjects";
        this.b_ResearchAddSelectedProjects.UseVisualStyleBackColor = true;
        this.b_ResearchAddSelectedProjects.Click += new EventHandler(this.b_ResearchAddSelectedProjects_Click);
        manager.ApplyResources(this.l_ResearchComplete, "l_ResearchComplete");
        this.l_ResearchComplete.ForeColor = SystemColors.Highlight;
        this.l_ResearchComplete.Name = "l_ResearchComplete";
        this.gb_ArmyStrength.Controls.Add(this.b_Replenish);
        this.gb_ArmyStrength.Controls.Add(this.b_Reduce);
        this.gb_ArmyStrength.Controls.Add(this.l_UnitChange);
        manager.ApplyResources(this.gb_ArmyStrength, "gb_ArmyStrength");
        this.gb_ArmyStrength.Name = "gb_ArmyStrength";
        this.gb_ArmyStrength.TabStop = false;
        manager.ApplyResources(this.l_UnitChange, "l_UnitChange");
        this.l_UnitChange.ForeColor = SystemColors.Highlight;
        this.l_UnitChange.Name = "l_UnitChange";
        this.gb_Parameter.Controls.Add(this.cb_EditFactions);
        this.gb_Parameter.Controls.Add(this.b_OpenTableDialog_Factions);
        manager.ApplyResources(this.gb_Parameter, "gb_Parameter");
        this.gb_Parameter.Name = "gb_Parameter";
        this.gb_Parameter.TabStop = false;
        this.gb_Global_MoneyDistribution.Controls.Add(this.cb_Global_MoneyDistribution_Rebels);
        this.gb_Global_MoneyDistribution.Controls.Add(this.b_Global_MoneyDistribution);
        this.gb_Global_MoneyDistribution.Controls.Add(this.l_MoneyDistributionLimit);
        this.gb_Global_MoneyDistribution.Controls.Add(this.l_MoneyDistribution);
        this.gb_Global_MoneyDistribution.Controls.Add(this.nup_Global_MoneyDistributionLimit);
        this.gb_Global_MoneyDistribution.Controls.Add(this.nup_Global_MoneyDistribution);
        manager.ApplyResources(this.gb_Global_MoneyDistribution, "gb_Global_MoneyDistribution");
        this.gb_Global_MoneyDistribution.Name = "gb_Global_MoneyDistribution";
        this.gb_Global_MoneyDistribution.TabStop = false;
        manager.ApplyResources(this.l_MoneyDistributionLimit, "l_MoneyDistributionLimit");
        this.l_MoneyDistributionLimit.Name = "l_MoneyDistributionLimit";
        manager.ApplyResources(this.l_MoneyDistribution, "l_MoneyDistribution");
        this.l_MoneyDistribution.Name = "l_MoneyDistribution";
        manager.ApplyResources(this.nup_Global_MoneyDistributionLimit, "nup_Global_MoneyDistributionLimit");
        int[] numArray10 = new int[4];
        numArray10[0] = 0x3b9a_ca00;
        this.nup_Global_MoneyDistributionLimit.Maximum = new decimal(numArray10);
        this.nup_Global_MoneyDistributionLimit.Name = "nup_Global_MoneyDistributionLimit";
        int[] numArray11 = new int[4];
        numArray11[0] = 0x4e20;
        this.nup_Global_MoneyDistributionLimit.Value = new decimal(numArray11);
        manager.ApplyResources(this.nup_Global_MoneyDistribution, "nup_Global_MoneyDistribution");
        int[] numArray12 = new int[4];
        numArray12[0] = 0x3b9a_ca00;
        this.nup_Global_MoneyDistribution.Maximum = new decimal(numArray12);
        int[] numArray13 = new int[4];
        numArray13[0] = 0x3b9a_ca00;
        numArray13[3] = -2_147_483_648;
        this.nup_Global_MoneyDistribution.Minimum = new decimal(numArray13);
        this.nup_Global_MoneyDistribution.Name = "nup_Global_MoneyDistribution";
        this.tabPage_Parser.Controls.Add(this.dgv_ParserResult);
        this.tabPage_Parser.Controls.Add(this.p_Control);
        manager.ApplyResources(this.tabPage_Parser, "tabPage_Parser");
        this.tabPage_Parser.Name = "tabPage_Parser";
        this.tabPage_Parser.UseVisualStyleBackColor = true;
        this.dgv_ParserResult.AllowUserToAddRows = false;
        this.dgv_ParserResult.AllowUserToDeleteRows = false;
        this.dgv_ParserResult.AllowUserToOrderColumns = true;
        this.dgv_ParserResult.AllowUserToResizeRows = false;
        this.dgv_ParserResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        this.dgv_ParserResult.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
        this.dgv_ParserResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        manager.ApplyResources(this.dgv_ParserResult, "dgv_ParserResult");
        this.dgv_ParserResult.EnableHeadersVisualStyles = false;
        this.dgv_ParserResult.Name = "dgv_ParserResult";
        this.dgv_ParserResult.RowHeadersVisible = false;
        this.dgv_ParserResult.CellDoubleClick += new DataGridViewCellEventHandler(this.cellDoubleClick_DataTable);
        this.dgv_ParserResult.CellMouseUp += new DataGridViewCellMouseEventHandler(this.cellMouseUp_DataTable);
        this.dgv_ParserResult.CellValueChanged += new DataGridViewCellEventHandler(this.cellValueChanged_DataTable);
        this.dgv_ParserResult.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(this.dataGridViewResult_ColumnHeaderMouseClick);
        this.dgv_ParserResult.DataError += new DataGridViewDataErrorEventHandler(this.dataGridViewResult_DataError);
        this.p_Control.BorderStyle = BorderStyle.FixedSingle;
        this.p_Control.Controls.Add(this.b_FactionArrayIndex);
        this.p_Control.Controls.Add(this.tb_ParseFaction);
        this.p_Control.Controls.Add(this.cb_Factions);
        this.p_Control.Controls.Add(this.l_Filter);
        this.p_Control.Controls.Add(this.cb_Filter);
        this.p_Control.Controls.Add(this.cb_DisplayTable);
        this.p_Control.Controls.Add(this.l_DisplayTable);
        this.p_Control.Controls.Add(this.b_Parse);
        manager.ApplyResources(this.p_Control, "p_Control");
        this.p_Control.Name = "p_Control";
        this.cb_Factions.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        this.cb_Factions.AutoCompleteSource = AutoCompleteSource.ListItems;
        this.cb_Factions.FormattingEnabled = true;
        manager.ApplyResources(this.cb_Factions, "cb_Factions");
        this.cb_Factions.Name = "cb_Factions";
        this.cb_Factions.SelectedIndexChanged += new EventHandler(this.cb_Factions_SelectedIndexChanged);
        manager.ApplyResources(this.l_Filter, "l_Filter");
        this.l_Filter.Name = "l_Filter";
        this.cb_Filter.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cb_Filter.FormattingEnabled = true;
        manager.ApplyResources(this.cb_Filter, "cb_Filter");
        this.cb_Filter.Name = "cb_Filter";
        this.cb_DisplayTable.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cb_DisplayTable.FormattingEnabled = true;
        manager.ApplyResources(this.cb_DisplayTable, "cb_DisplayTable");
        this.cb_DisplayTable.Name = "cb_DisplayTable";
        this.cb_DisplayTable.SelectedIndexChanged += new EventHandler(this.cb_DisplayTable_SelectedIndexChanged);
        manager.ApplyResources(this.l_DisplayTable, "l_DisplayTable");
        this.l_DisplayTable.Name = "l_DisplayTable";
        this.tabControl_Main.Controls.Add(this.tabPage_Parser);
        this.tabControl_Main.Controls.Add(this.tabPage_Edit);
        this.tabControl_Main.Controls.Add(this.tabPage_Edit_Global);
        manager.ApplyResources(this.tabControl_Main, "tabControl_Main");
        this.tabControl_Main.Name = "tabControl_Main";
        this.tabControl_Main.SelectedIndex = 0;
        this.tabPage_Edit_Global.Controls.Add(this.gb_Global_Army);
        this.tabPage_Edit_Global.Controls.Add(this.gb_Global_GameInfo);
        this.tabPage_Edit_Global.Controls.Add(this.gb_Global_MoneyDistribution);
        this.tabPage_Edit_Global.Controls.Add(this.gb_Global_GameSettings);
        manager.ApplyResources(this.tabPage_Edit_Global, "tabPage_Edit_Global");
        this.tabPage_Edit_Global.Name = "tabPage_Edit_Global";
        this.tabPage_Edit_Global.UseVisualStyleBackColor = true;
        this.gb_Global_Army.Controls.Add(this.b_Global_MaxUnits);
        this.gb_Global_Army.Controls.Add(this.b_Global_SetUnitSizeToScale);
        this.gb_Global_Army.Controls.Add(this.cb_Global_UnitSizeScale);
        this.gb_Global_Army.Controls.Add(this.l_Global_UnitSizeScale);
        this.gb_Global_Army.Controls.Add(this.cb_Global_MaxUnits);
        manager.ApplyResources(this.gb_Global_Army, "gb_Global_Army");
        this.gb_Global_Army.Name = "gb_Global_Army";
        this.gb_Global_Army.TabStop = false;
        manager.ApplyResources(this.b_Global_MaxUnits, "b_Global_MaxUnits");
        this.b_Global_MaxUnits.Name = "b_Global_MaxUnits";
        this.b_Global_MaxUnits.UseVisualStyleBackColor = true;
        this.b_Global_MaxUnits.Click += new EventHandler(this.b_Global_MaxUnits_Click);
        this.cb_Global_UnitSizeScale.FormattingEnabled = true;
        manager.ApplyResources(this.cb_Global_UnitSizeScale, "cb_Global_UnitSizeScale");
        this.cb_Global_UnitSizeScale.Name = "cb_Global_UnitSizeScale";
        this.cb_Global_UnitSizeScale.SelectionChangeCommitted += new EventHandler(this.cb_Global_UnitSizeScale_SelectionChangeCommitted);
        manager.ApplyResources(this.l_Global_UnitSizeScale, "l_Global_UnitSizeScale");
        this.l_Global_UnitSizeScale.Name = "l_Global_UnitSizeScale";
        this.cb_Global_MaxUnits.FormattingEnabled = true;
        manager.ApplyResources(this.cb_Global_MaxUnits, "cb_Global_MaxUnits");
        this.cb_Global_MaxUnits.Name = "cb_Global_MaxUnits";
        this.gb_Global_GameInfo.Controls.Add(this.tb_Global_CampaignTag);
        this.gb_Global_GameInfo.Controls.Add(this.l_CampaignTag);
        manager.ApplyResources(this.gb_Global_GameInfo, "gb_Global_GameInfo");
        this.gb_Global_GameInfo.Name = "gb_Global_GameInfo";
        this.gb_Global_GameInfo.TabStop = false;
        manager.ApplyResources(this.tb_Global_CampaignTag, "tb_Global_CampaignTag");
        this.tb_Global_CampaignTag.Name = "tb_Global_CampaignTag";
        this.tb_Global_CampaignTag.ReadOnly = true;
        manager.ApplyResources(this.l_CampaignTag, "l_CampaignTag");
        this.l_CampaignTag.Name = "l_CampaignTag";
        this.gb_Global_GameSettings.Controls.Add(this.b_Global_Climate);
        this.gb_Global_GameSettings.Controls.Add(this.nup_Global_Climate);
        this.gb_Global_GameSettings.Controls.Add(this.b_Global_RemoveFamousBattleMarker);
        this.gb_Global_GameSettings.Controls.Add(this.b_Global_TPY);
        this.gb_Global_GameSettings.Controls.Add(this.nup_Global_TPY);
        this.gb_Global_GameSettings.Controls.Add(this.b_Global_EnablePolitics);
        manager.ApplyResources(this.gb_Global_GameSettings, "gb_Global_GameSettings");
        this.gb_Global_GameSettings.Name = "gb_Global_GameSettings";
        this.gb_Global_GameSettings.TabStop = false;
        manager.ApplyResources(this.nup_Global_Climate, "nup_Global_Climate");
        int[] numArray14 = new int[4];
        numArray14[0] = 4;
        this.nup_Global_Climate.Maximum = new decimal(numArray14);
        this.nup_Global_Climate.Name = "nup_Global_Climate";
        manager.ApplyResources(this.b_Global_RemoveFamousBattleMarker, "b_Global_RemoveFamousBattleMarker");
        this.b_Global_RemoveFamousBattleMarker.Name = "b_Global_RemoveFamousBattleMarker";
        this.b_Global_RemoveFamousBattleMarker.UseVisualStyleBackColor = true;
        this.b_Global_RemoveFamousBattleMarker.Click += new EventHandler(this.b_Global_RemoveFamousBattleMarker_Click);
        manager.ApplyResources(this.b_Global_TPY, "b_Global_TPY");
        this.b_Global_TPY.Name = "b_Global_TPY";
        this.b_Global_TPY.UseVisualStyleBackColor = true;
        this.b_Global_TPY.Click += new EventHandler(this.b_Global_TPY_Click);
        manager.ApplyResources(this.nup_Global_TPY, "nup_Global_TPY");
        int[] numArray15 = new int[4];
        numArray15[0] = 1;
        this.nup_Global_TPY.Minimum = new decimal(numArray15);
        this.nup_Global_TPY.Name = "nup_Global_TPY";
        int[] numArray16 = new int[4];
        numArray16[0] = 1;
        this.nup_Global_TPY.Value = new decimal(numArray16);
        manager.ApplyResources(this, "$this");
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Controls.Add(this.tabControl_Main);
        base.Controls.Add(this.p_Status);
        base.Controls.Add(this.ms_Main);
        base.Name = "SaveParser";
        base.FormClosed += new FormClosedEventHandler(this.formMain_Closed);
        base.Shown += new EventHandler(this.formMain_Shown);
        this.ms_Main.ResumeLayout(false);
        this.ms_Main.PerformLayout();
        this.cms_Table.ResumeLayout(false);
        this.p_Status.ResumeLayout(false);
        this.p_Status.PerformLayout();
        this.nup_Age.EndInit();
        this.tabPage_Edit.ResumeLayout(false);
        this.gb_Recruitment.ResumeLayout(false);
        this.gb_Recruitment.PerformLayout();
        this.nup_EditRank.EndInit();
        this.gb_Province.ResumeLayout(false);
        this.gb_Province.PerformLayout();
        this.nup_HappinessValue.EndInit();
        this.gb_Region.ResumeLayout(false);
        this.gb_Character.ResumeLayout(false);
        this.gb_Character.PerformLayout();
        this.gb_Movement.ResumeLayout(false);
        this.gb_Movement.PerformLayout();
        this.nup_MovementPoints.EndInit();
        this.gb_Research.ResumeLayout(false);
        this.gb_Research.PerformLayout();
        this.gb_ArmyStrength.ResumeLayout(false);
        this.gb_ArmyStrength.PerformLayout();
        this.gb_Parameter.ResumeLayout(false);
        this.gb_Global_MoneyDistribution.ResumeLayout(false);
        this.gb_Global_MoneyDistribution.PerformLayout();
        this.nup_Global_MoneyDistributionLimit.EndInit();
        this.nup_Global_MoneyDistribution.EndInit();
        this.tabPage_Parser.ResumeLayout(false);
        ((ISupportInitialize) this.dgv_ParserResult).EndInit();
        this.p_Control.ResumeLayout(false);
        this.p_Control.PerformLayout();
        this.tabControl_Main.ResumeLayout(false);
        this.tabPage_Edit_Global.ResumeLayout(false);
        this.gb_Global_Army.ResumeLayout(false);
        this.gb_Global_Army.PerformLayout();
        this.gb_Global_GameInfo.ResumeLayout(false);
        this.gb_Global_GameInfo.PerformLayout();
        this.gb_Global_GameSettings.ResumeLayout(false);
        this.nup_Global_Climate.EndInit();
        this.nup_Global_TPY.EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private bool loadCampaignMap()
    {
        bool flag = true;
        this.debug("- loadCampaignMap:", false);
        if (!GlobalData.isCampaignMapReloadNeeded)
        {
            this.debug("  - no load needed.", false);
            return true;
        }
        this.stopwatch = Stopwatch.StartNew();
        string gamePath = this.dic_properties[GlobalData.gamePathProperty[this.gameTag]];
        this.debug("  - campaignMapPath   : " + GlobalData.campaignMapPath, false);
        string packName = "data.pack";
        string packedFileName = "clickgen_map.jpg";
        switch (this.gameTag)
        {
            case 0:
                packedFileName = "geographic_map.png";
                break;

            case 1:
                packName = "data_rome2.pack";
                if (GlobalData.campaignTag == "inv")
                {
                    packedFileName = "main_invasion_map.jpg";
                }
                break;

            case 5:
                packedFileName = (GlobalData.campaignTag != "wh_we") ? "wh_main_map_minimap.png" : "wh_dlc05_wood_elves_map.png";
                break;

            case 6:
                if (GlobalData.campaignTag == "wh2_me")
                {
                    packName = "local_en.pack";
                    packedFileName = "wh_main_map.png";
                }
                else
                {
                    packName = "local_en.pack";
                    packedFileName = "wh2_main_great_vortex_map.png";
                }
                break;

            default:
                break;
        }
        this.debug("  - packName          : " + packName, false);
        this.debug("  - mapFilename       : " + packedFileName, false);
        if (this.usePackFiles)
        {
            try
            {
                GlobalData.campaignMap = PackParser.getPackedFileData(gamePath, this.debugging ? this : null, this.gameTag, packName, GlobalData.campaignMapPath.Replace('/', '\\'), packedFileName).ImagePackedFile;
            }
            catch (Exception)
            {
                this.debug("Warning: game pack locked. Unable to load map from game pack.", LogLevelType.warning, false);
                flag = false;
            }
        }
        if (!flag || !this.usePackFiles)
        {
            string str4 = packedFileName.Substring(0, packedFileName.IndexOf(".")) + ".jpg";
            string path = @"data\" + GlobalData.campaignMapPath.Replace('/', '\\') + @"\" + str4;
            if (File.Exists(path))
            {
                this.debug("Map files found in data directory. Loading from data folder.", LogLevelType.info, false);
                GlobalData.campaignMap = (byte[]) new ImageConverter().ConvertTo(Image.FromFile(path), typeof(byte[]));
                flag = true;
            }
        }
        this.debug("  - campaignMap.Length: " + ((GlobalData.campaignMap != null) ? GlobalData.campaignMap.Length.ToString() : "NULL"), false);
        this.stopwatch.Stop();
        this.debug("  - " + this.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s"), false);
        return flag;
    }

    private void loadPackDataFromFiles()
    {
        bool flag = false;
        this.stopwatch = Stopwatch.StartNew();
        this.debug("Loading " + GlobalData.gameName[this.gameTag] + " pack data from files...", false);
        this.l_Status_Text.Text = "Loading pack data from files...";
        this.l_Status_Text.Refresh();
        Cursor.Current = Cursors.WaitCursor;
        DataLoader loader = new DataLoader(this);
        string str = "";
        try
        {
            string str2 = GlobalData.gameTag[this.gameTag] + ((this.mod != null) ? ("-" + this.mod) : "");
            DataBase.dbt_Names = this.importDBTable<Data_Name>(str = "dbt_" + str2 + "_Names");
            DataBase.dbt_Technologies = this.importDBTable<Data_Technology>(str = "dbt_" + str2 + "_Technologies");
            DataBase.dbt_Portraits = this.importDBTable<Data_Portrait>(str = "dbt_" + str2 + "_Portraits");
            DataBase.dbt_Traits = this.importDBTable<Data_Trait>(str = "dbt_" + str2 + "_Traits");
            DataBase.dbt_TraitEffects = this.importDBTable<Data_TraitEffect>(str = "dbt_" + str2 + "_TraitEffects");
            DataBase.dbt_Ancillaries = this.importDBTable<Data_Ancillary>(str = "dbt_" + str2 + "_Ancillaries");
            if ((this.gameTag == 5) || (this.gameTag == 6))
            {
                DataBase.dbt_Skills = this.importDBTable<Data_Skill>(str = "dbt_" + str2 + "_Skills");
                DataBase.dbt_SkillEffects = this.importDBTable<Data_SkillEffect>(str = "dbt_" + str2 + "_SkillEffects");
                DataBase.dbt_Factions = this.importDBTable<Data_Faction>(str = "dbt_" + str2 + "_Factions");
                DataBase.dbt_Buildings = this.importDBTable<Data_Building>(str = "dbt_" + str2 + "_Buildings");
                DataBase.dbt_AncillaryEffects = this.importDBTable<Data_AncillaryEffect>(str = "dbt_" + str2 + "_AncillaryEffects");
            }
            else if ((this.gameTag != 0) && (this.gameTag != 1))
            {
                if (this.gameTag == 7)
                {
                    DataBase.dbt_Skills = this.importDBTable<Data_Skill>(str = "dbt_" + str2 + "_Skills");
                    DataBase.dbt_SkillEffects = this.importDBTable<Data_SkillEffect>(str = "dbt_" + str2 + "_SkillEffects");
                }
            }
            else
            {
                DataBase.dbt_Skills = this.importDBTable<Data_Skill>(str = "dbt_" + str2 + "_Skills");
                DataBase.dbt_Factions = this.importDBTable<Data_Faction>(str = "dbt_" + str2 + "_Factions");
                DataBase.dbt_Buildings = this.importDBTable<Data_Building>(str = "dbt_" + str2 + "_Buildings");
                DataBase.dbt_Cultures = this.importDBTable<Data_Culture>(str = "dbt_" + str2 + "_Cultures");
                DataBase.dbt_SkillEffects = this.importDBTable<Data_SkillEffect>(str = "dbt_" + str2 + "_SkillEffects");
                DataBase.dbt_AncillaryEffects = this.importDBTable<Data_AncillaryEffect>(str = "dbt_" + str2 + "_AncillaryEffects");
                DataBase.dic_ArmyNames = this.importDBDictionary<string, string>("dic_" + str2 + "_ArmyNames");
            }
        }
        catch (Exception)
        {
            flag = true;
        }
        finally
        {
            Cursor.Current = Cursors.Default;
            this.stopwatch.Stop();
        }
        if (!flag)
        {
            if ((this.gameTag != 5) && (this.gameTag != 6))
            {
                DataBase.dic_Ancillaries = loader.getDictionary_Ancillaries(DataBase.dbt_Ancillaries);
            }
            DataBase.dic_CharacterNames = loader.getDictionary_CharacterNames(DataBase.dbt_Names);
            this.debug(GlobalData.gameName[this.gameTag] + " pack data via files loaded. (" + this.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s") + ")", false);
        }
        else
        {
            string[] textArray1 = new string[] { "Error: ", GlobalData.gameName[this.gameTag], " pack data via file '", str, "' NOT loaded." };
            this.debug(string.Concat(textArray1), LogLevelType.error, false);
            if (this.windowDebug != null)
            {
                this.windowDebug.BringToFront();
                this.windowDebug.Focus();
            }
            this.showError("Data pack file '" + str + "' not loaded.\n", "Configuration Error", true);
        }
    }

    [AsyncStateMachine(typeof(<loadPackDataFromGame>d__27))]
    private void loadPackDataFromGame()
    {
        <loadPackDataFromGame>d__27 d__;
        d__.<>t__builder = AsyncVoidMethodBuilder.Create();
        d__.<>4__this = this;
        d__.<>1__state = -1;
        d__.<>t__builder.Start<<loadPackDataFromGame>d__27>(ref d__);
    }

    private void manageComponentsTabPageEdit()
    {
        if (this.gameTag == 1)
        {
            this.b_Global_EnablePolitics.Visible = true;
            this.b_Global_TPY.Visible = true;
            this.nup_Global_TPY.Visible = true;
            this.gb_Movement.Visible = true;
            this.gb_Region.Visible = true;
            this.gb_Recruitment.Visible = true;
            this.b_Global_Climate.Visible = false;
            this.nup_Global_Climate.Visible = false;
            this.b_Map.Visible = true;
            this.b_Corruption_Clear.Visible = false;
            this.l_Global_UnitSizeScale.Visible = true;
            this.cb_Global_UnitSizeScale.Visible = true;
            this.b_Global_SetUnitSizeToScale.Visible = true;
        }
        else if (this.gameTag == 0)
        {
            this.b_Global_EnablePolitics.Visible = false;
            this.b_Global_TPY.Visible = true;
            this.nup_Global_TPY.Visible = true;
            this.gb_Movement.Visible = true;
            this.gb_Region.Visible = true;
            this.gb_Recruitment.Visible = true;
            this.b_Global_Climate.Visible = true;
            this.nup_Global_Climate.Visible = true;
            this.b_Map.Visible = true;
            this.b_Corruption_Clear.Visible = false;
            this.l_Global_UnitSizeScale.Visible = true;
            this.cb_Global_UnitSizeScale.Visible = true;
            this.b_Global_SetUnitSizeToScale.Visible = true;
        }
        else if (this.gameTag == 2)
        {
            this.b_Global_Climate.Visible = false;
            this.nup_Global_Climate.Visible = false;
            this.gb_Province.Visible = false;
            this.gb_Region.Visible = false;
            this.gb_ArmyStrength.Visible = true;
            this.gb_Movement.Visible = true;
            this.gb_Global_MoneyDistribution.Visible = false;
            this.gb_Global_GameSettings.Visible = true;
            this.nup_Global_TPY.Visible = true;
            this.b_Global_TPY.Visible = true;
            this.b_Map.Visible = false;
            this.b_Corruption_Clear.Visible = false;
            this.gb_Recruitment.Visible = false;
            this.l_Global_UnitSizeScale.Visible = false;
            this.cb_Global_UnitSizeScale.Visible = false;
            this.b_Global_SetUnitSizeToScale.Visible = false;
        }
        else if (this.gameTag == 5)
        {
            this.b_Global_Climate.Visible = false;
            this.nup_Global_Climate.Visible = false;
            this.b_Global_EnablePolitics.Visible = false;
            this.nup_Global_TPY.Visible = false;
            this.b_Global_TPY.Visible = false;
            this.nup_Age.Visible = false;
            this.l_Age.Visible = false;
            this.b_BirthYears.Visible = false;
            this.b_Map.Visible = true;
            this.b_Corruption_Clear.Visible = true;
            this.gb_Recruitment.Visible = true;
            this.l_Global_UnitSizeScale.Visible = false;
            this.cb_Global_UnitSizeScale.Visible = false;
            this.b_Global_SetUnitSizeToScale.Visible = false;
        }
        else if (this.gameTag == 6)
        {
            this.b_Global_Climate.Visible = false;
            this.nup_Global_Climate.Visible = false;
            this.b_Global_EnablePolitics.Visible = false;
            this.nup_Global_TPY.Visible = false;
            this.b_Global_TPY.Visible = false;
            this.nup_Age.Visible = false;
            this.l_Age.Visible = false;
            this.b_BirthYears.Visible = false;
            this.b_Map.Visible = true;
            this.b_Corruption_Clear.Visible = true;
            this.gb_Recruitment.Visible = true;
            this.l_Global_UnitSizeScale.Visible = false;
            this.cb_Global_UnitSizeScale.Visible = false;
            this.b_Global_SetUnitSizeToScale.Visible = false;
        }
        else if (this.gameTag == 7)
        {
            this.b_Global_Climate.Visible = false;
            this.nup_Global_Climate.Visible = false;
            this.b_Global_EnablePolitics.Visible = false;
            this.nup_Age.Visible = false;
            this.l_Age.Visible = false;
            this.b_BirthYears.Visible = false;
            this.nup_Global_TPY.Visible = true;
            this.b_Global_TPY.Visible = true;
            this.gb_Region.Visible = true;
            this.gb_Province.Visible = false;
            this.b_Corruption_Clear.Visible = false;
            this.gb_Recruitment.Visible = false;
            this.b_ResetAgentAction.Visible = false;
            this.b_Map.Visible = true;
            this.l_Global_UnitSizeScale.Visible = true;
            this.cb_Global_UnitSizeScale.Visible = true;
            this.b_Global_SetUnitSizeToScale.Visible = true;
        }
    }

    private void manipulateUnitStrength(int mode)
    {
        string str = (mode == 0) ? "Reducing" : "Replenishing";
        List<int> list = this.getFactionIndices(this.cb_EditFactions.Text, true);
        if (list != null)
        {
            this.addComboBoxBuffers();
            this.setButtonsStatus(false);
            foreach (int num in list)
            {
                this.l_Status_Text.Text = str + " armies of faction " + num.ToString() + "...";
                this.l_Status_Text.Refresh();
                Util_Army army = new Util_Army(this);
                int armyIndex = 0;
                while (armyIndex < army.getFactionArmySize(num))
                {
                    int unitIndex = 0;
                    while (true)
                    {
                        if (unitIndex >= army.getFactionArmyUnitSize(num, armyIndex))
                        {
                            armyIndex++;
                            break;
                        }
                        army.setStrength(num, armyIndex, unitIndex, mode, -1);
                        unitIndex++;
                    }
                }
            }
            this.setButtonsStatus(true);
            this.l_Status_Text.Text = str + " done.";
        }
    }

    private void openEditSFDialog(string treeNodeJumpToPath, bool viewMode)
    {
        Window_EditSF tsf = new Window_EditSF(SaveParser_Utils.nodes.esfFile, treeNodeJumpToPath, viewMode);
        if (viewMode)
        {
            tsf.Show();
        }
        else
        {
            tsf.ShowDialog();
        }
        if (tsf.DialogResult == DialogResult.OK)
        {
            SaveParser_Utils.nodes.esfFile = tsf.getESFFile();
        }
    }

    private void parsing(int option)
    {
        if (this.cb_DisplayTable.SelectedItem.ToString() != "-")
        {
            Cursor.Current = Cursors.WaitCursor;
            if (this.tb_ParseFaction.Text.Trim() != "-1")
            {
                this.selectedFactionArrayIndex = this.tb_ParseFaction.Text;
            }
            this.debug("parse table '" + this.cb_DisplayTable.SelectedItem.ToString() + "' for faction: " + this.tb_ParseFaction.Text, false);
            string str = (this.cb_DisplayTable.SelectedIndex != 1) ? ("faction " + this.tb_ParseFaction.Text) : "factions";
            this.l_Status_Text.Text = "parsing for " + str + "...";
            this.l_Status_Text.Refresh();
            this.dgv_ParserResult.DataSource = null;
            this.dgv_ParserResult.Refresh();
            int selectedIndex = this.cb_Filter.SelectedIndex;
            bool onlyActive = this.cb_Filter.SelectedIndex == 1;
            int filter = -1;
            try
            {
                filter = Convert.ToInt32(this.tb_ParseFaction.Text.Trim());
            }
            catch (Exception)
            {
                this.l_Status_Text.Text = "invalid input at faction array";
                return;
            }
            int fromIndex = filter;
            int toIndex = filter + 1;
            if (filter == -1)
            {
                fromIndex = 0;
                toIndex = new Util_Faction(this).getFactionSize();
            }
            IList iList = null;
            string str2 = "";
            if (this.cb_DisplayTable.SelectedIndex == 0)
            {
                iList = this.parserFunctions.loadCharacters(fromIndex, toIndex, selectedIndex, false);
                if (iList == null)
                {
                    this.l_Status_Text.Text = "invalid faction ID";
                    return;
                }
                str2 = "characters";
                this.setDataSource<Table_Data_Character>(iList);
            }
            else if (this.cb_DisplayTable.SelectedIndex == 4)
            {
                iList = this.parserFunctions.loadArmies(fromIndex, toIndex, selectedIndex, false);
                if (iList == null)
                {
                    this.l_Status_Text.Text = "invalid faction ID";
                    return;
                }
                str2 = "armies";
                this.setDataSource<Table_Data_Army>(iList);
            }
            else if (this.cb_DisplayTable.SelectedIndex == 1)
            {
                iList = this.parserFunctions.loadFactions(selectedIndex);
                str2 = "factions";
                this.setDataSource<Table_Data_Faction>(iList);
            }
            else if (this.cb_DisplayTable.SelectedIndex == 2)
            {
                iList = this.parserFunctions.loadRegions(filter, false);
                str2 = "regions";
                this.setDataSource<Table_Data_Region>(iList);
            }
            else if (this.cb_DisplayTable.SelectedIndex == 3)
            {
                List<int> fXList = this.getFactionIndices(this.tb_ParseFaction.Text, true);
                iList = this.parserFunctions.loadProvinces(fXList);
                str2 = "provinces";
                this.setDataSource<Table_Data_Province>(iList);
            }
            else if (this.cb_DisplayTable.SelectedIndex != 6)
            {
                if (this.cb_DisplayTable.SelectedIndex == 5)
                {
                    iList = this.parserFunctions.loadUnits(fromIndex, toIndex, option, selectedIndex);
                    if (iList == null)
                    {
                        this.l_Status_Text.Text = "invalid faction ID";
                        return;
                    }
                    str2 = "units";
                    this.setDataSource<Table_Data_Unit>(iList);
                }
            }
            else
            {
                if (filter == -1)
                {
                    this.l_Status_Text.Text = "invalid input at faction array. -1 not allowed.";
                    return;
                }
                iList = this.parserFunctions.loadDiplomacies(filter, onlyActive);
                if (iList == null)
                {
                    this.l_Status_Text.Text = "invalid faction ID";
                    return;
                }
                str2 = "diplomacies";
                this.setDataSource<Table_Data_Diplomacy>(iList);
            }
            this.debug("Result list size: " + iList.Count.ToString(), false);
            this.l_Status_Text.Text = iList.Count.ToString() + " " + str2 + " displayed.";
            this.setColumnProperties();
            Cursor.Current = Cursors.Default;
        }
    }

    private void processEditorDialog_AncillaryPool(int factionIndex)
    {
        new Window_Editor_AncillaryPool(this, this.gameTag).Dispose();
    }

    private void processEditorDialog_Character(int factionIndex, int characterIndex)
    {
        Table_Data_Character character = this.parserFunctions.getCharacter(factionIndex, characterIndex);
        Util_Character character2 = new Util_Character(this);
        if ((this.gameTag == 1) || (this.gameTag == 0))
        {
            Window_Editor_AR2_Character character4 = new Window_Editor_AR2_Character(this, this.gameTag);
            character4.setData(character);
            character4.ShowDialog();
            if (character4.DialogResult == DialogResult.OK)
            {
                character = character4.getData();
                character2.setName(factionIndex, characterIndex, 0, character.namePart[0], false);
                character2.setName(factionIndex, characterIndex, 1, character.namePart[1], false);
                character2.setName(factionIndex, characterIndex, 2, character.namePart[2], false);
                character2.setName(factionIndex, characterIndex, 3, character.namePart[3], false);
                character2.setBirthYear(factionIndex, characterIndex, character.birthYear, -1);
                character2.setAgentAttribute(factionIndex, characterIndex, 0, character.subterfuge);
                character2.setAgentAttribute(factionIndex, characterIndex, 1, character.zeal);
                character2.setAgentAttribute(factionIndex, characterIndex, 2, character.authority);
                character2.setPoliticsAttribute(factionIndex, characterIndex, 0, character.ambition);
                character2.setPoliticsAttribute(factionIndex, characterIndex, 1, character.gravitas);
                character2.setRank(factionIndex, characterIndex, character.rank);
                character2.setSkillPoints(factionIndex, characterIndex, character.skillPoints);
                character2.setPortrait(factionIndex, characterIndex, character.portrait);
                character2.setMovementPoints(factionIndex, characterIndex, character.movementPoints, character.movementPointsMax);
                if (character4.editedEnableAgentAction)
                {
                    character2.setEnableAgentAction(factionIndex, characterIndex);
                }
                if (character4.editedLocTuple)
                {
                    character2.setLocation(factionIndex, characterIndex, character.locTuple, character.loc_1, character.loc_2);
                }
                character2.setTraits(character);
                if (character4.editedBackground)
                {
                    character2.setSkillBackground(factionIndex, characterIndex, character.skill[character.backgroundSkillIndex], Convert.ToInt32(character.indent[character.backgroundSkillIndex]), character.backgroundSkillIndex, -1);
                }
                if (this.gameTag != 1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!string.IsNullOrEmpty(character.ancillary[i]))
                        {
                            character2.setAncillary(factionIndex, characterIndex, i, character.ancillary[i]);
                        }
                    }
                }
                else
                {
                    int ancillaryIndex = -1;
                    for (int i = 0; i < 4; i++)
                    {
                        if (!string.IsNullOrEmpty(character.ancillary[i]))
                        {
                            ancillaryIndex++;
                            character2.setAncillary(factionIndex, characterIndex, ancillaryIndex, character.ancillary[i]);
                        }
                    }
                }
            }
            character4.Dispose();
        }
        else if (this.gameTag == 2)
        {
            Window_Editor_S2_Character character5 = new Window_Editor_S2_Character(this, this.gameTag);
            character5.setData(character);
            character5.ShowDialog();
            if (character5.DialogResult == DialogResult.OK)
            {
                character = character5.getData();
                character2.setPortrait(factionIndex, characterIndex, character.portrait);
                character2.setName(factionIndex, characterIndex, 0, character.namePart[0], false);
                character2.setName(factionIndex, characterIndex, 1, character.namePart[1], false);
                character2.setBirthYear(factionIndex, characterIndex, character.birthYear, -1);
                character2.setRank(factionIndex, characterIndex, character.rank);
                character2.setSkillPoints(factionIndex, characterIndex, character.skillPoints);
                character2.setTraits(character);
                character2.setAncillary(factionIndex, characterIndex, -1, character.retainers);
            }
            character5.Dispose();
        }
        else if ((this.gameTag != 5) && (this.gameTag != 6))
        {
            if (this.gameTag == 7)
            {
                Window_Editor_B_Character character7 = new Window_Editor_B_Character(this, this.gameTag);
                character7.setData(character);
                character7.ShowDialog();
                if (character7.DialogResult == DialogResult.OK)
                {
                    character = character7.getData();
                    character2.setPortrait(factionIndex, characterIndex, character.portrait);
                    character2.setName(factionIndex, characterIndex, 0, character.namePart[0], false);
                    character2.setRank(factionIndex, characterIndex, character.rank);
                    character2.setSkillPoints(factionIndex, characterIndex, character.skillPoints);
                    character2.setMovementPoints(factionIndex, characterIndex, character.movementPoints, character.movementPointsMax);
                    character2.setPoliticsAttribute(factionIndex, characterIndex, 0, character.ambition);
                    character2.setPoliticsAttribute(factionIndex, characterIndex, 1, character.gravitas);
                    if (character7.editedBackground)
                    {
                        character2.setSkillBackground(factionIndex, characterIndex, character.skill[character.backgroundSkillIndex], Convert.ToInt32(character.indent[character.backgroundSkillIndex]), character.backgroundSkillIndex, Convert.ToInt32(character.tier[character.backgroundSkillIndex]));
                    }
                    character2.setTraits(character);
                }
                character7.Dispose();
            }
        }
        else
        {
            Window_Editor_W_Character character6 = new Window_Editor_W_Character(this, this.gameTag);
            character6.setData(character);
            character6.ShowDialog();
            if (character6.DialogResult == DialogResult.OK)
            {
                character = character6.getData();
                character2.setPortrait(factionIndex, characterIndex, character.portrait);
                if (this.gameTag == 5)
                {
                    character2.setName(factionIndex, characterIndex, 1, character.namePart[1], !character.namePartGeneric[1]);
                    character2.setName(factionIndex, characterIndex, 2, character.namePart[2], !character.namePartGeneric[2]);
                }
                character2.setRank(factionIndex, characterIndex, character.rank);
                character2.setSkillPoints(factionIndex, characterIndex, character.skillPoints);
                character2.setAgentAttribute(factionIndex, characterIndex, 0, character.subterfuge);
                character2.setAgentAttribute(factionIndex, characterIndex, 1, character.zeal);
                character2.setAgentAttribute(factionIndex, characterIndex, 2, character.authority);
                character2.setMovementPoints(factionIndex, characterIndex, character.movementPoints, character.movementPointsMax);
                if (character.loyalty > -1)
                {
                    character2.setLoyalty(factionIndex, characterIndex, character.loyalty);
                }
                if (character6.editedBackground)
                {
                    character2.setSkillBackground(factionIndex, characterIndex, character.skill[character.backgroundSkillIndex], Convert.ToInt32(character.indent[character.backgroundSkillIndex]), character.backgroundSkillIndex, Convert.ToInt32(character.tier[character.backgroundSkillIndex]));
                }
                if (character6.editedEnableAgentAction)
                {
                    character2.setEnableAgentAction(factionIndex, characterIndex);
                }
                if (character6.editedLocTuple)
                {
                    character2.setLocation(factionIndex, characterIndex, character.locTuple, character.loc_1, character.loc_2);
                }
                character2.setTraits(character);
                for (int i = 0; i < character.ancillary.Length; i++)
                {
                    if (!string.IsNullOrEmpty(character.ancillary[i]))
                    {
                        character2.setAncillary(factionIndex, characterIndex, i, character.ancillary[i]);
                    }
                }
            }
            character6.Dispose();
        }
        Table_Data_Character character3 = character2.createCharacter(factionIndex, characterIndex, true);
        DataBase.dic_CharacterIdName[character.id] = character3.name;
    }

    private void readINIFile()
    {
        if (!File.Exists("SaveParser.ini"))
        {
            this.showError("Error at reading SaveParser.ini:\n- File 'SaveParser.ini does not exist.", "Configuration Error", true);
        }
        this.dic_properties = Utils.parseINIFile("SaveParser.ini");
        if (this.dic_properties == null)
        {
            this.showError("Error at reading SaveParser.ini:\n- at least one entry is invalid at character.traitAlert.*", "Configuration Error", true);
        }
        try
        {
            if (this.dic_properties["logFile.enabled"] == "true")
            {
                this.debugging = true;
                FileStream stream = File.Create("SaveParser.log");
                this.logWriter = new StreamWriter(stream);
                this.tsmi_DebugWindow_Click(null, null);
                this.debug("Start logging:", false);
            }
        }
        catch (Exception)
        {
            this.showError("Error at reading SaveParser.ini: 'logFile.enabled' entry missing.", "Configuration Error", false);
        }
        if (this.debugging)
        {
            if (this.windowDebug != null)
            {
                this.debug("TEST Error", LogLevelType.error, false);
                this.debug("TEST Header", LogLevelType.header, false);
                this.debug("TEST normal", LogLevelType.normal, false);
                this.debug("TEST warning", LogLevelType.warning, false);
                this.debug("TEST info", LogLevelType.info, false);
            }
            this.debug("SaveParser.ini content:", LogLevelType.header, false);
            foreach (KeyValuePair<string, string> pair in this.dic_properties)
            {
                this.debug(string.Format("- {0,-26} = {1}", pair.Key, pair.Value), false);
            }
        }
        try
        {
            GlobalData.TableConfig.character_skills_short = this.dic_properties["character.skills.display"].ToLower() == "background";
            GlobalData.TableConfig.character_traits_text = this.dic_properties["character.skills.display"].ToLower() == "text";
            GlobalData.TableConfig.faction_balance = this.dic_properties["faction.balance.display"].ToLower() == "true";
            GlobalData.TableConfig.army_id = this.dic_properties["army.id.display"].ToLower() == "true";
            GlobalData.TableConfig.army_commanderID = this.dic_properties["army.commanderID.display"].ToLower() == "true";
            this.tsmi_Options_ActiveFactions.Checked = this.dic_properties["button.factionArray.active"].ToLower() == "true";
            this.tsmi_OpenFile_CustomFile.Text = this.dic_properties["customFileMenuName"];
            this.usePackFiles = this.dic_properties["usePackDataFiles"].ToLower() == "true";
            GlobalData.traitAlert = new List<string>();
            foreach (KeyValuePair<string, string> pair2 in this.dic_properties)
            {
                if (pair2.Key.Contains("character.traitAlert"))
                {
                    GlobalData.traitAlert.Add(pair2.Value);
                }
            }
            char[] separator = new char[] { ',' };
            GlobalData.DataFilter.Attila.skill = ("," + this.dic_properties["filter.skill.attila"]).Replace(" ", "").Split(separator);
            char[] chArray2 = new char[] { ',' };
            GlobalData.DataFilter.Attila.trait = ("," + this.dic_properties["filter.trait.attila"]).Replace(" ", "").Split(chArray2);
            char[] chArray3 = new char[] { ',' };
            GlobalData.DataFilter.Attila.ancillary = ("," + this.dic_properties["filter.ancillary.attila"]).Replace(" ", "").Split(chArray3);
            char[] chArray4 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer.skill = ("," + this.dic_properties["filter.skill.warhammer"]).Replace(" ", "").Split(chArray4);
            char[] chArray5 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer.trait = ("," + this.dic_properties["filter.trait.warhammer"]).Replace(" ", "").Split(chArray5);
            char[] chArray6 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer.ancillary = ("," + this.dic_properties["filter.ancillary.warhammer"]).Replace(" ", "").Split(chArray6);
            char[] chArray7 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer2.skill = ("," + this.dic_properties["filter.skill.warhammer2"]).Replace(" ", "").Split(chArray7);
            char[] chArray8 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer2.trait = ("," + this.dic_properties["filter.trait.warhammer2"]).Replace(" ", "").Split(chArray8);
            char[] chArray9 = new char[] { ',' };
            GlobalData.DataFilter.Warhammer2.ancillary = ("," + this.dic_properties["filter.ancillary.warhammer2"]).Replace(" ", "").Split(chArray9);
            char[] chArray10 = new char[] { ',' };
            GlobalData.DataFilter.Rome2.skill = ("," + this.dic_properties["filter.skill.rome2"]).Replace(" ", "").Split(chArray10);
            char[] chArray11 = new char[] { ',' };
            GlobalData.DataFilter.Rome2.trait = ("," + this.dic_properties["filter.trait.rome2"]).Replace(" ", "").Split(chArray11);
            char[] chArray12 = new char[] { ',' };
            GlobalData.DataFilter.Rome2.ancillary = ("," + this.dic_properties["filter.ancillary.rome2"]).Replace(" ", "").Split(chArray12);
            char[] chArray13 = new char[] { ',' };
            GlobalData.DataFilter.Shogun2.trait = ("," + this.dic_properties["filter.trait.shogun2"]).Replace(" ", "").Split(chArray13);
            char[] chArray14 = new char[] { ',' };
            GlobalData.DataFilter.Shogun2.ancillary = ("," + this.dic_properties["filter.ancillary.shogun2"]).Replace(" ", "").Split(chArray14);
            char[] chArray15 = new char[] { ',' };
            GlobalData.DataFilter.Britannia.skill = ("," + this.dic_properties["filter.skill.britannia"]).Replace(" ", "").Split(chArray15);
            char[] chArray16 = new char[] { ',' };
            GlobalData.DataFilter.Britannia.trait = ("," + this.dic_properties["filter.trait.britannia"]).Replace(" ", "").Split(chArray16);
        }
        catch (Exception)
        {
            this.showError("Error at reading SaveParser.ini: error at entries", "Configuration Error", true);
        }
        this.dic_ModShortPackFile = new Dictionary<string, string>();
        int num = -1;
        foreach (string str in this.dic_properties.Keys)
        {
            if (str.StartsWith("mod."))
            {
                string s = str.Substring(4);
                s = s.Substring(0, s.IndexOf('.'));
                try
                {
                    int num2 = int.Parse(s);
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        num++;
        this.debug("Amout of mods embedded: " + num.ToString(), false);
        this.tsmi_Mods = new ToolStripMenuItem[num];
        ToolStripMenuItem item = null;
        for (int i = 0; i < num; i++)
        {
            this.tsmi_Mods[i] = new ToolStripMenuItem();
            this.tsmi_Mods[i].Text = this.dic_properties["mod." + i.ToString() + ".modName"];
            this.tsmi_Mods[i].ToolTipText = this.dic_properties["mod." + i.ToString() + ".mainGame"] + "|" + this.dic_properties["mod." + i.ToString() + ".modNameShort"];
            string str3 = this.dic_properties["mod." + i.ToString() + ".mainGame"];
            if (str3 == "Attila")
            {
                item = this.tsmi_Attila;
            }
            else if (str3 == "Rome2")
            {
                item = this.tsmi_Rome2;
            }
            else if (str3 == "Warhammer")
            {
                item = this.tsmi_Warhammer;
            }
            else if (str3 == "Warhammer2")
            {
                item = this.tsmi_Warhammer2;
            }
            else if (str3 == "Shogun2")
            {
                item = this.tsmi_Shogun2;
            }
            else if (str3 == "Britannia")
            {
                item = this.tsmi_Britannia;
            }
            int index = Array.IndexOf<string>(GlobalData.savefileDirectory, this.dic_properties["mod." + i.ToString() + ".mainGame"]);
            if ((index > -1) && File.Exists(Path.Combine(this.dic_properties[GlobalData.gamePathProperty[index]], "data", this.dic_properties["mod." + i.ToString() + ".dataFile"])))
            {
                this.tsmi_Mods[i].Click += new EventHandler(this.tsmi_Game_Click);
                ToolStripItem[] toolStripItems = new ToolStripItem[] { this.tsmi_Mods[i] };
                item.DropDownItems.AddRange(toolStripItems);
                this.dic_ModShortPackFile.Add(this.dic_properties["mod." + i.ToString() + ".modNameShort"], this.dic_properties["mod." + i.ToString() + ".dataFile"]);
            }
        }
    }

    private void saveGame(string fileName, string path)
    {
        Cursor.Current = Cursors.WaitCursor;
        this.setButtonsStatus(false);
        this.l_Status_Text.Text = "Saving savegame file '" + fileName + "'...";
        this.l_Status_Text.Refresh();
        string[] textArray1 = new string[] { "Writing savegame file:\n- type: ", this.savegameFileType.ToString(), "\n- game: ", GlobalData.gameTag[this.gameTag], "\n- file: '", Path.Combine(path, fileName), "' ..." };
        this.debug(string.Concat(textArray1), false);
        DateTime now = DateTime.Now;
        if (this.gameTag == 6)
        {
            this.parserFunctions.markSavegameFileAsModifiedW2(SaveParser_Utils.nodes.esfFile);
        }
        EsfCodecUtil.WriteEsfFile(Path.Combine(path, fileName), SaveParser_Utils.nodes.esfFile);
        double num = Math.Round(DateTime.Now.Subtract(now).TotalSeconds, 2);
        this.debug("- duration: " + num.ToString() + " s", false);
        this.setButtonsStatus(true);
        this.l_Status_Text.Text = fileName + " saved. (" + num.ToString() + " secs)";
        this.dgv_ParserResult.DataSource = null;
        Cursor.Current = Cursors.Default;
    }

    private void setButtonsStatus(bool status)
    {
        this.cb_DisplayTable.Enabled = status;
        this.b_Replenish.Enabled = status;
        this.b_Reduce.Enabled = status;
        this.b_Parse.Enabled = status;
        this.b_ResearchComplete.Enabled = status;
        this.b_ResearchAddAllProjects.Enabled = status;
        this.b_MovementPoints.Enabled = status;
        this.b_ResetMovementPoints.Enabled = status;
        this.b_Global_MoneyDistribution.Enabled = status;
        this.b_Global_EnablePolitics.Enabled = status;
        this.b_Global_RemoveFamousBattleMarker.Enabled = status;
        this.b_FactionArrayIndex.Enabled = status;
        this.b_OpenTableDialog_Factions.Enabled = status;
        this.b_BirthYears.Enabled = status;
        this.b_ResetAgentAction.Enabled = status;
        this.b_OpenTableDialog_Regions.Enabled = status;
        this.b_OpenTableDialog_Provinces.Enabled = status;
        this.b_ConstructionComplete.Enabled = status;
        this.b_RecruitmentComplete.Enabled = status;
        this.b_ProvinceSetHappiness.Enabled = status;
        this.cb_Global_MaxUnits.Enabled = status;
        this.cb_Global_UnitSizeScale.Enabled = status;
        this.b_Global_SetUnitSizeToScale.Enabled = status;
        this.b_Global_MaxUnits.Enabled = status;
        this.b_Global_Climate.Enabled = status;
        this.b_RemoveAlertTraits.Enabled = status;
        this.b_Map.Enabled = status;
        this.b_Corruption_Clear.Enabled = status;
        this.b_Global_TPY.Enabled = status;
        this.cb_DisplayTable.Refresh();
        this.b_Replenish.Refresh();
        this.b_Reduce.Refresh();
        this.b_Parse.Refresh();
        this.b_ResearchComplete.Refresh();
        this.b_ResearchAddAllProjects.Refresh();
        this.b_MovementPoints.Refresh();
        this.b_ResetMovementPoints.Refresh();
        this.b_Global_MoneyDistribution.Refresh();
        this.b_Global_EnablePolitics.Refresh();
        this.b_Global_RemoveFamousBattleMarker.Refresh();
        this.b_FactionArrayIndex.Refresh();
        this.b_OpenTableDialog_Factions.Refresh();
        this.b_BirthYears.Refresh();
        this.b_ResetAgentAction.Refresh();
        this.b_OpenTableDialog_Regions.Refresh();
        this.b_OpenTableDialog_Provinces.Refresh();
        this.b_ConstructionComplete.Refresh();
        this.b_RecruitmentComplete.Refresh();
        this.b_ProvinceSetHappiness.Refresh();
        this.cb_Global_MaxUnits.Refresh();
        this.cb_Global_UnitSizeScale.Refresh();
        this.cb_Global_MaxUnits.Refresh();
        this.b_Global_SetUnitSizeToScale.Refresh();
        this.b_Global_Climate.Refresh();
        this.b_Global_TPY.Refresh();
        this.b_RemoveAlertTraits.Refresh();
        this.b_Map.Refresh();
        this.tsmi_SaveFile.Enabled = status;
        this.tsmi_SaveFileAs.Enabled = status;
        this.tsmi_Options_ActiveFactions.Enabled = status;
        this.tsmi_OpenBatchController.Enabled = status;
        this.tsmi_EditSFDialog.Enabled = status;
    }

    private void setColumnProperties()
    {
        foreach (DataGridViewColumn column in this.dgv_ParserResult.Columns)
        {
            column.SortMode = DataGridViewColumnSortMode.Automatic;
            column.ReadOnly = true;
            bool flag = false;
            bool flag2 = true;
            bool flag3 = false;
            string str = this.columnAttrib[this.gameTag, this.cb_DisplayTable.SelectedIndex, column.Index];
            if ((str != null) && (str != ""))
            {
                if (str == "E")
                {
                    flag = true;
                }
                else if (str == "I")
                {
                    flag2 = false;
                }
                else if (str == "L")
                {
                    flag3 = true;
                }
                else if (str == "X")
                {
                    flag2 = false;
                    if (this.cb_DisplayTable.SelectedIndex == 0)
                    {
                        if ((column.Index == 0x11) && (this.dic_properties["character.family.display"].ToLower() == "true"))
                        {
                            flag2 = true;
                        }
                        else if ((column.Index == 0x12) && (this.dic_properties["character.portrait.display"].ToLower() == "true"))
                        {
                            flag2 = true;
                        }
                    }
                    else if (this.cb_DisplayTable.SelectedIndex == 1)
                    {
                        if ((column.Index == 4) && (this.dic_properties["faction.balance.display"].ToLower() == "true"))
                        {
                            flag2 = true;
                        }
                    }
                    else if (this.cb_DisplayTable.SelectedIndex == 4)
                    {
                        if ((column.Index == 8) && (this.dic_properties["army.id.display"].ToLower() == "true"))
                        {
                            flag2 = true;
                        }
                        if ((column.Index == 10) && (this.dic_properties["army.commanderID.display"].ToLower() == "true"))
                        {
                            flag2 = true;
                        }
                    }
                }
            }
            if (flag)
            {
                column.HeaderCell.Style.ForeColor = Color.Blue;
                column.ReadOnly = false;
            }
            if (flag3)
            {
                column.HeaderCell.Style.ForeColor = Color.Green;
            }
            column.Visible = flag2;
        }
        if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 2) || ((this.gameTag == 5) || (this.gameTag == 6)))))
        {
            foreach (DataGridViewRow row in (IEnumerable) this.dgv_ParserResult.Rows)
            {
                if (this.cb_DisplayTable.SelectedIndex == 0)
                {
                    string s = row.Cells[3].Value.ToString();
                    if ((this.gameTag == 5) || (this.gameTag == 6))
                    {
                        string[] textArray1 = new string[] { "general", "spy", "champion", "dignitary", "wizard", "runesmith" };
                        foreach (string str3 in textArray1)
                        {
                            if (s.StartsWith(str3))
                            {
                                s = str3;
                                break;
                            }
                        }
                    }
                    Color white = Color.White;
                    uint num2 = <PrivateImplementationDetails>.ComputeStringHash(s);
                    if (num2 <= 0x6f02_3d15)
                    {
                        if (num2 <= 0x2960_586e)
                        {
                            if (num2 > 0x247d_4d21)
                            {
                                if (num2 == 0x24b9_216b)
                                {
                                    if (s != "general")
                                    {
                                    }
                                }
                                else if ((num2 == 0x2960_586e) && (s == "inspector"))
                                {
                                    white = Color.Khaki;
                                }
                            }
                            else if (num2 != 0x2e1_9f9e)
                            {
                                if ((num2 == 0x247d_4d21) && (s == "metsuke"))
                                {
                                    white = Color.Khaki;
                                }
                            }
                            else if (s == "minister")
                            {
                                white = Color.LightGray;
                            }
                        }
                        else if (num2 <= 0x5fd3_fcc8)
                        {
                            if (num2 != 0x55d7_22eb)
                            {
                                if ((num2 == 0x5fd3_fcc8) && (s == "runesmith"))
                                {
                                    white = Color.Pink;
                                }
                            }
                            else if (s == "captain")
                            {
                                white = Color.LightGray;
                            }
                        }
                        else if (num2 == 0x64cc_4e3f)
                        {
                            if (s == "shinsengumi")
                            {
                                white = Color.Khaki;
                            }
                        }
                        else if (num2 != 0x66ce_ca66)
                        {
                            if ((num2 == 0x6f02_3d15) && (s == "lady"))
                            {
                                white = Color.LightGray;
                            }
                        }
                        else if (s == "geisha")
                        {
                            white = Color.Pink;
                        }
                    }
                    else if (num2 <= 0xd541_621d)
                    {
                        if (num2 <= 0x8174_98c3)
                        {
                            if (num2 != 0x774f_6eaa)
                            {
                                if ((num2 == 0x8174_98c3) && (s == "shinobi"))
                                {
                                    white = Color.DarkSeaGreen;
                                }
                            }
                            else if (s == "dancer")
                            {
                                white = Color.Pink;
                            }
                        }
                        else if (num2 != 0xaf53_7014)
                        {
                            if ((num2 == 0xd541_621d) && (s == "spy"))
                            {
                                white = Color.DarkSeaGreen;
                            }
                        }
                        else if (s == "veteran")
                        {
                            white = Color.LightSkyBlue;
                        }
                    }
                    else if (num2 <= 0xee86_e81e)
                    {
                        if (num2 != 0xea59_5ca6)
                        {
                            if ((num2 == 0xee86_e81e) && (s == "monk"))
                            {
                                white = Color.LightSkyBlue;
                            }
                        }
                        else if (s == "champion")
                        {
                            white = Color.Khaki;
                        }
                    }
                    else if (num2 == 0xf79c_50d8)
                    {
                        if (s == "dignitary")
                        {
                            white = Color.LightSkyBlue;
                        }
                    }
                    else if (num2 != 0xfa83_27ab)
                    {
                        if ((num2 == 0xfc03_0f0a) && (s == "wizard"))
                        {
                            white = Color.LightGreen;
                        }
                    }
                    else if (s == "ninja")
                    {
                        white = Color.DarkSeaGreen;
                    }
                    row.DefaultCellStyle.BackColor = white;
                    if (row.Cells[0x10].Value.ToString().IndexOf("*") > -1)
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle {
                            BackColor = Color.Red,
                            ForeColor = Color.White
                        };
                        row.Cells[0x10].Style = style;
                    }
                }
            }
        }
        this.dgv_ParserResult.Refresh();
    }

    private void setContent_FactionArrayCombobox()
    {
        this.cb_Factions.DataSource = new BindingSource(this.tsmi_Options_ActiveFactions.Checked ? DataBase.dic_FactionIndexNameActive : DataBase.dic_FactionIndexName, null);
        this.cb_Factions.DisplayMember = "Value";
        this.cb_Factions.ValueMember = "Key";
        try
        {
            this.cb_Factions.SelectedIndex = 1;
        }
        catch (Exception)
        {
            this.cb_Factions.SelectedIndex = 0;
        }
    }

    private void setDataSource<T>(IList iList)
    {
        SortableBindingList<T> list = new SortableBindingList<T>((List<T>) iList.Cast<T>());
        this.dgv_ParserResult.DataSource = list;
        if (this.cb_DisplayTable.SelectedIndex == 0)
        {
            int num = 0;
            using (IEnumerator enumerator = iList.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    string str = "";
                    string[] trait = ((Table_Data_Character) enumerator.Current).trait;
                    int index = 0;
                    while (true)
                    {
                        if (index >= trait.Length)
                        {
                            this.dgv_ParserResult.Rows[num].Cells[0x10].ToolTipText = str;
                            num++;
                            break;
                        }
                        string str2 = trait[index];
                        if (!string.IsNullOrEmpty(str2))
                        {
                            str = str + str2 + "\n";
                        }
                        index++;
                    }
                }
            }
        }
    }

    private void setSavegamePaths()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string[] textArray1 = new string[5];
        textArray1[0] = @"The Creative Assembly\";
        textArray1[1] = GlobalData.savefileDirectory[this.gameTag];
        textArray1[2] = @"\save_games";
        textArray1[3] = GlobalData.multiplayerSavegame ? "_multiplayer" : "";
        string[] local1 = textArray1;
        local1[4] = @"\";
        string str3 = Path.Combine(folderPath, string.Concat(local1));
        this.openFileDialog_Savegame.InitialDirectory = str3;
        this.openFileDialog_Savegame.FileName = "*." + GlobalData.savefileExtension[this.gameTag];
        this.saveFileDialog_Savegame.InitialDirectory = str3;
        this.saveFileDialog_Savegame.FileName = "*." + GlobalData.savefileExtension[this.gameTag];
    }

    private string[,,] setTableAttributes()
    {
        string[] textArray1 = new string[8, 7, 0x16];
        textArray1[0, 0, 0] = "V";
        textArray1[0, 0, 1] = "V";
        textArray1[0, 0, 2] = "V";
        textArray1[0, 0, 3] = "V";
        textArray1[0, 0, 4] = "V";
        textArray1[0, 0, 5] = "E";
        textArray1[0, 0, 6] = "E";
        textArray1[0, 0, 7] = "E";
        textArray1[0, 0, 8] = "E";
        textArray1[0, 0, 9] = "E";
        textArray1[0, 0, 10] = "E";
        textArray1[0, 0, 11] = "E";
        textArray1[0, 0, 12] = "E";
        textArray1[0, 0, 13] = "E";
        textArray1[0, 0, 14] = "V";
        textArray1[0, 0, 15] = "V";
        textArray1[0, 0, 0x10] = "V";
        textArray1[0, 0, 0x11] = "X";
        textArray1[0, 0, 0x12] = "X";
        textArray1[0, 0, 0x13] = "V";
        textArray1[0, 0, 20] = "V";
        textArray1[0, 0, 0x15] = "V";
        textArray1[0, 1, 0] = "V";
        textArray1[0, 1, 1] = "V";
        textArray1[0, 1, 2] = "V";
        textArray1[0, 1, 3] = "E";
        textArray1[0, 1, 4] = "X";
        textArray1[0, 1, 5] = "V";
        textArray1[0, 1, 6] = "V";
        textArray1[0, 1, 7] = "V";
        textArray1[0, 1, 8] = "V";
        textArray1[0, 1, 9] = "V";
        textArray1[0, 1, 10] = "V";
        textArray1[0, 1, 11] = "V";
        textArray1[0, 1, 12] = "V";
        textArray1[0, 1, 13] = "V";
        textArray1[0, 1, 14] = "V";
        textArray1[0, 1, 15] = "";
        textArray1[0, 1, 0x10] = "";
        textArray1[0, 1, 0x11] = "";
        textArray1[0, 1, 0x12] = "";
        textArray1[0, 1, 0x13] = "";
        textArray1[0, 1, 20] = "";
        textArray1[0, 1, 0x15] = "";
        textArray1[0, 2, 0] = "V";
        textArray1[0, 2, 1] = "V";
        textArray1[0, 2, 2] = "V";
        textArray1[0, 2, 3] = "V";
        textArray1[0, 2, 4] = "V";
        textArray1[0, 2, 5] = "V";
        textArray1[0, 2, 6] = "";
        textArray1[0, 2, 7] = "";
        textArray1[0, 2, 8] = "";
        textArray1[0, 2, 9] = "";
        textArray1[0, 2, 10] = "";
        textArray1[0, 2, 11] = "";
        textArray1[0, 2, 12] = "";
        textArray1[0, 2, 13] = "";
        textArray1[0, 2, 14] = "";
        textArray1[0, 2, 15] = "";
        textArray1[0, 2, 0x10] = "";
        textArray1[0, 2, 0x11] = "";
        textArray1[0, 2, 0x12] = "";
        textArray1[0, 2, 0x13] = "";
        textArray1[0, 2, 20] = "";
        textArray1[0, 2, 0x15] = "";
        textArray1[0, 3, 0] = "V";
        textArray1[0, 3, 1] = "V";
        textArray1[0, 3, 2] = "V";
        textArray1[0, 3, 3] = "";
        textArray1[0, 3, 4] = "";
        textArray1[0, 3, 5] = "";
        textArray1[0, 3, 6] = "";
        textArray1[0, 3, 7] = "";
        textArray1[0, 3, 8] = "";
        textArray1[0, 3, 9] = "";
        textArray1[0, 3, 10] = "";
        textArray1[0, 3, 11] = "";
        textArray1[0, 3, 12] = "";
        textArray1[0, 3, 13] = "";
        textArray1[0, 3, 14] = "";
        textArray1[0, 3, 15] = "";
        textArray1[0, 3, 0x10] = "";
        textArray1[0, 3, 0x11] = "";
        textArray1[0, 3, 0x12] = "";
        textArray1[0, 3, 0x13] = "";
        textArray1[0, 3, 20] = "";
        textArray1[0, 3, 0x15] = "";
        textArray1[0, 4, 0] = "V";
        textArray1[0, 4, 1] = "V";
        textArray1[0, 4, 2] = "V";
        textArray1[0, 4, 3] = "V";
        textArray1[0, 4, 4] = "E";
        textArray1[0, 4, 5] = "E";
        textArray1[0, 4, 6] = "L";
        textArray1[0, 4, 7] = "V";
        textArray1[0, 4, 8] = "X";
        textArray1[0, 4, 9] = "L";
        textArray1[0, 4, 10] = "X";
        textArray1[0, 4, 11] = "E";
        textArray1[0, 4, 12] = "E";
        textArray1[0, 4, 13] = "";
        textArray1[0, 4, 14] = "";
        textArray1[0, 4, 15] = "";
        textArray1[0, 4, 0x10] = "";
        textArray1[0, 4, 0x11] = "";
        textArray1[0, 4, 0x12] = "";
        textArray1[0, 4, 0x13] = "";
        textArray1[0, 4, 20] = "";
        textArray1[0, 4, 0x15] = "";
        textArray1[0, 5, 0] = "V";
        textArray1[0, 5, 1] = "V";
        textArray1[0, 5, 2] = "V";
        textArray1[0, 5, 3] = "V";
        textArray1[0, 5, 4] = "E";
        textArray1[0, 5, 5] = "V";
        textArray1[0, 5, 6] = "E";
        textArray1[0, 5, 7] = "E";
        textArray1[0, 5, 8] = "V";
        textArray1[0, 5, 9] = "";
        textArray1[0, 5, 10] = "";
        textArray1[0, 5, 11] = "";
        textArray1[0, 5, 12] = "";
        textArray1[0, 5, 13] = "";
        textArray1[0, 5, 14] = "";
        textArray1[0, 5, 15] = "";
        textArray1[0, 5, 0x10] = "";
        textArray1[0, 5, 0x11] = "";
        textArray1[0, 5, 0x12] = "";
        textArray1[0, 5, 0x13] = "";
        textArray1[0, 5, 20] = "";
        textArray1[0, 5, 0x15] = "";
        textArray1[0, 6, 0] = "V";
        textArray1[0, 6, 1] = "V";
        textArray1[0, 6, 2] = "V";
        textArray1[0, 6, 3] = "E";
        textArray1[0, 6, 4] = "E";
        textArray1[0, 6, 5] = "V";
        textArray1[0, 6, 6] = "";
        textArray1[0, 6, 7] = "";
        textArray1[0, 6, 8] = "";
        textArray1[0, 6, 9] = "";
        textArray1[0, 6, 10] = "";
        textArray1[0, 6, 11] = "";
        textArray1[0, 6, 12] = "";
        textArray1[0, 6, 13] = "";
        textArray1[0, 6, 14] = "";
        textArray1[0, 6, 15] = "";
        textArray1[0, 6, 0x10] = "";
        textArray1[0, 6, 0x11] = "";
        textArray1[0, 6, 0x12] = "";
        textArray1[0, 6, 0x13] = "";
        textArray1[0, 6, 20] = "";
        textArray1[0, 6, 0x15] = "";
        textArray1[1, 0, 0] = "V";
        textArray1[1, 0, 1] = "V";
        textArray1[1, 0, 2] = "V";
        textArray1[1, 0, 3] = "V";
        textArray1[1, 0, 4] = "V";
        textArray1[1, 0, 5] = "E";
        textArray1[1, 0, 6] = "E";
        textArray1[1, 0, 7] = "E";
        textArray1[1, 0, 8] = "E";
        textArray1[1, 0, 9] = "E";
        textArray1[1, 0, 10] = "E";
        textArray1[1, 0, 11] = "E";
        textArray1[1, 0, 12] = "E";
        textArray1[1, 0, 13] = "E";
        textArray1[1, 0, 14] = "V";
        textArray1[1, 0, 15] = "V";
        textArray1[1, 0, 0x10] = "V";
        textArray1[1, 0, 0x11] = "X";
        textArray1[1, 0, 0x12] = "X";
        textArray1[1, 0, 0x13] = "I";
        textArray1[1, 0, 20] = "V";
        textArray1[1, 0, 0x15] = "V";
        textArray1[1, 1, 0] = "V";
        textArray1[1, 1, 1] = "V";
        textArray1[1, 1, 2] = "V";
        textArray1[1, 1, 3] = "E";
        textArray1[1, 1, 4] = "X";
        textArray1[1, 1, 5] = "V";
        textArray1[1, 1, 6] = "V";
        textArray1[1, 1, 7] = "V";
        textArray1[1, 1, 8] = "V";
        textArray1[1, 1, 9] = "V";
        textArray1[1, 1, 10] = "V";
        textArray1[1, 1, 11] = "V";
        textArray1[1, 1, 12] = "I";
        textArray1[1, 1, 13] = "I";
        textArray1[1, 1, 14] = "V";
        textArray1[1, 1, 15] = "";
        textArray1[1, 1, 0x10] = "";
        textArray1[1, 1, 0x11] = "";
        textArray1[1, 1, 0x12] = "";
        textArray1[1, 1, 0x13] = "";
        textArray1[1, 1, 20] = "";
        textArray1[1, 1, 0x15] = "";
        textArray1[1, 2, 0] = "V";
        textArray1[1, 2, 1] = "V";
        textArray1[1, 2, 2] = "V";
        textArray1[1, 2, 3] = "V";
        textArray1[1, 2, 4] = "V";
        textArray1[1, 2, 5] = "V";
        textArray1[1, 2, 6] = "";
        textArray1[1, 2, 7] = "";
        textArray1[1, 2, 8] = "";
        textArray1[1, 2, 9] = "";
        textArray1[1, 2, 10] = "";
        textArray1[1, 2, 11] = "";
        textArray1[1, 2, 12] = "";
        textArray1[1, 2, 13] = "";
        textArray1[1, 2, 14] = "";
        textArray1[1, 2, 15] = "";
        textArray1[1, 2, 0x10] = "";
        textArray1[1, 2, 0x11] = "";
        textArray1[1, 2, 0x12] = "";
        textArray1[1, 2, 0x13] = "";
        textArray1[1, 2, 20] = "";
        textArray1[1, 2, 0x15] = "";
        textArray1[1, 3, 0] = "V";
        textArray1[1, 3, 1] = "V";
        textArray1[1, 3, 2] = "V";
        textArray1[1, 3, 3] = "";
        textArray1[1, 3, 4] = "";
        textArray1[1, 3, 5] = "";
        textArray1[1, 3, 6] = "";
        textArray1[1, 3, 7] = "";
        textArray1[1, 3, 8] = "";
        textArray1[1, 3, 9] = "";
        textArray1[1, 3, 10] = "";
        textArray1[1, 3, 11] = "";
        textArray1[1, 3, 12] = "";
        textArray1[1, 3, 13] = "";
        textArray1[1, 3, 14] = "";
        textArray1[1, 3, 15] = "";
        textArray1[1, 3, 0x10] = "";
        textArray1[1, 3, 0x11] = "";
        textArray1[1, 3, 0x12] = "";
        textArray1[1, 3, 0x13] = "";
        textArray1[1, 3, 20] = "";
        textArray1[1, 3, 0x15] = "";
        textArray1[1, 4, 0] = "V";
        textArray1[1, 4, 1] = "V";
        textArray1[1, 4, 2] = "V";
        textArray1[1, 4, 3] = "V";
        textArray1[1, 4, 4] = "E";
        textArray1[1, 4, 5] = "E";
        textArray1[1, 4, 6] = "L";
        textArray1[1, 4, 7] = "V";
        textArray1[1, 4, 8] = "X";
        textArray1[1, 4, 9] = "L";
        textArray1[1, 4, 10] = "X";
        textArray1[1, 4, 11] = "I";
        textArray1[1, 4, 12] = "I";
        textArray1[1, 4, 13] = "";
        textArray1[1, 4, 14] = "";
        textArray1[1, 4, 15] = "";
        textArray1[1, 4, 0x10] = "";
        textArray1[1, 4, 0x11] = "";
        textArray1[1, 4, 0x12] = "";
        textArray1[1, 4, 0x13] = "";
        textArray1[1, 4, 20] = "";
        textArray1[1, 4, 0x15] = "";
        textArray1[1, 5, 0] = "V";
        textArray1[1, 5, 1] = "V";
        textArray1[1, 5, 2] = "V";
        textArray1[1, 5, 3] = "V";
        textArray1[1, 5, 4] = "E";
        textArray1[1, 5, 5] = "V";
        textArray1[1, 5, 6] = "E";
        textArray1[1, 5, 7] = "E";
        textArray1[1, 5, 8] = "V";
        textArray1[1, 5, 9] = "";
        textArray1[1, 5, 10] = "";
        textArray1[1, 5, 11] = "";
        textArray1[1, 5, 12] = "";
        textArray1[1, 5, 13] = "";
        textArray1[1, 5, 14] = "";
        textArray1[1, 5, 15] = "";
        textArray1[1, 5, 0x10] = "";
        textArray1[1, 5, 0x11] = "";
        textArray1[1, 5, 0x12] = "";
        textArray1[1, 5, 0x13] = "";
        textArray1[1, 5, 20] = "";
        textArray1[1, 5, 0x15] = "";
        textArray1[1, 6, 0] = "V";
        textArray1[1, 6, 1] = "V";
        textArray1[1, 6, 2] = "V";
        textArray1[1, 6, 3] = "E";
        textArray1[1, 6, 4] = "E";
        textArray1[1, 6, 5] = "V";
        textArray1[1, 6, 6] = "";
        textArray1[1, 6, 7] = "";
        textArray1[1, 6, 8] = "";
        textArray1[1, 6, 9] = "";
        textArray1[1, 6, 10] = "";
        textArray1[1, 6, 11] = "";
        textArray1[1, 6, 12] = "";
        textArray1[1, 6, 13] = "";
        textArray1[1, 6, 14] = "";
        textArray1[1, 6, 15] = "";
        textArray1[1, 6, 0x10] = "";
        textArray1[1, 6, 0x11] = "";
        textArray1[1, 6, 0x12] = "";
        textArray1[1, 6, 0x13] = "";
        textArray1[1, 6, 20] = "";
        textArray1[1, 6, 0x15] = "";
        textArray1[2, 0, 0] = "V";
        textArray1[2, 0, 1] = "V";
        textArray1[2, 0, 2] = "V";
        textArray1[2, 0, 3] = "V";
        textArray1[2, 0, 4] = "V";
        textArray1[2, 0, 5] = "E";
        textArray1[2, 0, 6] = "E";
        textArray1[2, 0, 7] = "I";
        textArray1[2, 0, 8] = "I";
        textArray1[2, 0, 9] = "I";
        textArray1[2, 0, 10] = "I";
        textArray1[2, 0, 11] = "I";
        textArray1[2, 0, 12] = "E";
        textArray1[2, 0, 13] = "E";
        textArray1[2, 0, 14] = "V";
        textArray1[2, 0, 15] = "V";
        textArray1[2, 0, 0x10] = "V";
        textArray1[2, 0, 0x11] = "I";
        textArray1[2, 0, 0x12] = "I";
        textArray1[2, 0, 0x13] = "I";
        textArray1[2, 0, 20] = "I";
        textArray1[2, 0, 0x15] = "I";
        textArray1[2, 1, 0] = "V";
        textArray1[2, 1, 1] = "V";
        textArray1[2, 1, 2] = "V";
        textArray1[2, 1, 3] = "E";
        textArray1[2, 1, 4] = "V";
        textArray1[2, 1, 5] = "V";
        textArray1[2, 1, 6] = "I";
        textArray1[2, 1, 7] = "I";
        textArray1[2, 1, 8] = "I";
        textArray1[2, 1, 9] = "I";
        textArray1[2, 1, 10] = "I";
        textArray1[2, 1, 11] = "I";
        textArray1[2, 1, 12] = "I";
        textArray1[2, 1, 13] = "I";
        textArray1[2, 1, 14] = "I";
        textArray1[2, 1, 15] = "";
        textArray1[2, 1, 0x10] = "";
        textArray1[2, 1, 0x11] = "";
        textArray1[2, 1, 0x12] = "";
        textArray1[2, 1, 0x13] = "";
        textArray1[2, 1, 20] = "";
        textArray1[2, 1, 0x15] = "";
        textArray1[2, 2, 0] = "V";
        textArray1[2, 2, 1] = "V";
        textArray1[2, 2, 2] = "I";
        textArray1[2, 2, 3] = "V";
        textArray1[2, 2, 4] = "I";
        textArray1[2, 2, 5] = "I";
        textArray1[2, 2, 6] = "";
        textArray1[2, 2, 7] = "";
        textArray1[2, 2, 8] = "";
        textArray1[2, 2, 9] = "";
        textArray1[2, 2, 10] = "";
        textArray1[2, 2, 11] = "";
        textArray1[2, 2, 12] = "";
        textArray1[2, 2, 13] = "";
        textArray1[2, 2, 14] = "";
        textArray1[2, 2, 15] = "";
        textArray1[2, 2, 0x10] = "";
        textArray1[2, 2, 0x11] = "";
        textArray1[2, 2, 0x12] = "";
        textArray1[2, 2, 0x13] = "";
        textArray1[2, 2, 20] = "";
        textArray1[2, 2, 0x15] = "";
        textArray1[2, 3, 0] = "";
        textArray1[2, 3, 1] = "";
        textArray1[2, 3, 2] = "";
        textArray1[2, 3, 3] = "";
        textArray1[2, 3, 4] = "";
        textArray1[2, 3, 5] = "";
        textArray1[2, 3, 6] = "";
        textArray1[2, 3, 7] = "";
        textArray1[2, 3, 8] = "";
        textArray1[2, 3, 9] = "";
        textArray1[2, 3, 10] = "";
        textArray1[2, 3, 11] = "";
        textArray1[2, 3, 12] = "";
        textArray1[2, 3, 13] = "";
        textArray1[2, 3, 14] = "";
        textArray1[2, 3, 15] = "";
        textArray1[2, 3, 0x10] = "";
        textArray1[2, 3, 0x11] = "";
        textArray1[2, 3, 0x12] = "";
        textArray1[2, 3, 0x13] = "";
        textArray1[2, 3, 20] = "";
        textArray1[2, 3, 0x15] = "";
        textArray1[2, 4, 0] = "V";
        textArray1[2, 4, 1] = "V";
        textArray1[2, 4, 2] = "V";
        textArray1[2, 4, 3] = "I";
        textArray1[2, 4, 4] = "I";
        textArray1[2, 4, 5] = "I";
        textArray1[2, 4, 6] = "L";
        textArray1[2, 4, 7] = "V";
        textArray1[2, 4, 8] = "X";
        textArray1[2, 4, 9] = "L";
        textArray1[2, 4, 10] = "X";
        textArray1[2, 4, 11] = "I";
        textArray1[2, 4, 12] = "I";
        textArray1[2, 4, 13] = "";
        textArray1[2, 4, 14] = "";
        textArray1[2, 4, 15] = "";
        textArray1[2, 4, 0x10] = "";
        textArray1[2, 4, 0x11] = "";
        textArray1[2, 4, 0x12] = "";
        textArray1[2, 4, 0x13] = "";
        textArray1[2, 4, 20] = "";
        textArray1[2, 4, 0x15] = "";
        textArray1[2, 5, 0] = "V";
        textArray1[2, 5, 1] = "V";
        textArray1[2, 5, 2] = "V";
        textArray1[2, 5, 3] = "V";
        textArray1[2, 5, 4] = "E";
        textArray1[2, 5, 5] = "V";
        textArray1[2, 5, 6] = "E";
        textArray1[2, 5, 7] = "E";
        textArray1[2, 5, 8] = "V";
        textArray1[2, 5, 9] = "";
        textArray1[2, 5, 10] = "";
        textArray1[2, 5, 11] = "";
        textArray1[2, 5, 12] = "";
        textArray1[2, 5, 13] = "";
        textArray1[2, 5, 14] = "";
        textArray1[2, 5, 15] = "";
        textArray1[2, 5, 0x10] = "";
        textArray1[2, 5, 0x11] = "";
        textArray1[2, 5, 0x12] = "";
        textArray1[2, 5, 0x13] = "";
        textArray1[2, 5, 20] = "";
        textArray1[2, 5, 0x15] = "";
        textArray1[2, 6, 0] = "V";
        textArray1[2, 6, 1] = "V";
        textArray1[2, 6, 2] = "V";
        textArray1[2, 6, 3] = "E";
        textArray1[2, 6, 4] = "E";
        textArray1[2, 6, 5] = "";
        textArray1[2, 6, 6] = "";
        textArray1[2, 6, 7] = "";
        textArray1[2, 6, 8] = "";
        textArray1[2, 6, 9] = "";
        textArray1[2, 6, 10] = "";
        textArray1[2, 6, 11] = "";
        textArray1[2, 6, 12] = "";
        textArray1[2, 6, 13] = "";
        textArray1[2, 6, 14] = "";
        textArray1[2, 6, 15] = "";
        textArray1[2, 6, 0x10] = "";
        textArray1[2, 6, 0x11] = "";
        textArray1[2, 6, 0x12] = "";
        textArray1[2, 6, 0x13] = "";
        textArray1[2, 6, 20] = "";
        textArray1[2, 6, 0x15] = "";
        textArray1[3, 0, 0] = "V";
        textArray1[3, 0, 1] = "V";
        textArray1[3, 0, 2] = "V";
        textArray1[3, 0, 3] = "V";
        textArray1[3, 0, 4] = "V";
        textArray1[3, 0, 5] = "I";
        textArray1[3, 0, 6] = "I";
        textArray1[3, 0, 7] = "I";
        textArray1[3, 0, 8] = "I";
        textArray1[3, 0, 9] = "I";
        textArray1[3, 0, 10] = "I";
        textArray1[3, 0, 11] = "I";
        textArray1[3, 0, 12] = "E";
        textArray1[3, 0, 13] = "V";
        textArray1[3, 0, 14] = "V";
        textArray1[3, 0, 15] = "I";
        textArray1[3, 0, 0x10] = "I";
        textArray1[3, 0, 0x11] = "I";
        textArray1[3, 0, 0x12] = "I";
        textArray1[3, 0, 0x13] = "I";
        textArray1[3, 0, 20] = "I";
        textArray1[3, 0, 0x15] = "I";
        textArray1[3, 1, 0] = "V";
        textArray1[3, 1, 1] = "V";
        textArray1[3, 1, 2] = "V";
        textArray1[3, 1, 3] = "E";
        textArray1[3, 1, 4] = "I";
        textArray1[3, 1, 5] = "I";
        textArray1[3, 1, 6] = "I";
        textArray1[3, 1, 7] = "I";
        textArray1[3, 1, 8] = "I";
        textArray1[3, 1, 9] = "I";
        textArray1[3, 1, 10] = "I";
        textArray1[3, 1, 11] = "I";
        textArray1[3, 1, 12] = "I";
        textArray1[3, 1, 13] = "I";
        textArray1[3, 1, 14] = "I";
        textArray1[3, 1, 15] = "";
        textArray1[3, 1, 0x10] = "";
        textArray1[3, 1, 0x11] = "";
        textArray1[3, 1, 0x12] = "";
        textArray1[3, 1, 0x13] = "";
        textArray1[3, 1, 20] = "";
        textArray1[3, 1, 0x15] = "";
        textArray1[3, 2, 0] = "V";
        textArray1[3, 2, 1] = "V";
        textArray1[3, 2, 2] = "I";
        textArray1[3, 2, 3] = "I";
        textArray1[3, 2, 4] = "I";
        textArray1[3, 2, 5] = "I";
        textArray1[3, 2, 6] = "";
        textArray1[3, 2, 7] = "";
        textArray1[3, 2, 8] = "";
        textArray1[3, 2, 9] = "";
        textArray1[3, 2, 10] = "";
        textArray1[3, 2, 11] = "";
        textArray1[3, 2, 12] = "";
        textArray1[3, 2, 13] = "";
        textArray1[3, 2, 14] = "";
        textArray1[3, 2, 15] = "";
        textArray1[3, 2, 0x10] = "";
        textArray1[3, 2, 0x11] = "";
        textArray1[3, 2, 0x12] = "";
        textArray1[3, 2, 0x13] = "";
        textArray1[3, 2, 20] = "";
        textArray1[3, 2, 0x15] = "";
        textArray1[3, 3, 0] = "";
        textArray1[3, 3, 1] = "";
        textArray1[3, 3, 2] = "";
        textArray1[3, 3, 3] = "";
        textArray1[3, 3, 4] = "";
        textArray1[3, 3, 5] = "";
        textArray1[3, 3, 6] = "";
        textArray1[3, 3, 7] = "";
        textArray1[3, 3, 8] = "";
        textArray1[3, 3, 9] = "";
        textArray1[3, 3, 10] = "";
        textArray1[3, 3, 11] = "";
        textArray1[3, 3, 12] = "";
        textArray1[3, 3, 13] = "";
        textArray1[3, 3, 14] = "";
        textArray1[3, 3, 15] = "";
        textArray1[3, 3, 0x10] = "";
        textArray1[3, 3, 0x11] = "";
        textArray1[3, 3, 0x12] = "";
        textArray1[3, 3, 0x13] = "";
        textArray1[3, 3, 20] = "";
        textArray1[3, 3, 0x15] = "";
        textArray1[3, 4, 0] = "";
        textArray1[3, 4, 1] = "";
        textArray1[3, 4, 2] = "";
        textArray1[3, 4, 3] = "";
        textArray1[3, 4, 4] = "";
        textArray1[3, 4, 5] = "";
        textArray1[3, 4, 6] = "";
        textArray1[3, 4, 7] = "";
        textArray1[3, 4, 8] = "";
        textArray1[3, 4, 9] = "";
        textArray1[3, 4, 10] = "";
        textArray1[3, 4, 11] = "";
        textArray1[3, 4, 12] = "";
        textArray1[3, 4, 13] = "";
        textArray1[3, 4, 14] = "";
        textArray1[3, 4, 15] = "";
        textArray1[3, 4, 0x10] = "";
        textArray1[3, 4, 0x11] = "";
        textArray1[3, 4, 0x12] = "";
        textArray1[3, 4, 0x13] = "";
        textArray1[3, 4, 20] = "";
        textArray1[3, 4, 0x15] = "";
        textArray1[3, 5, 0] = "";
        textArray1[3, 5, 1] = "";
        textArray1[3, 5, 2] = "";
        textArray1[3, 5, 3] = "";
        textArray1[3, 5, 4] = "";
        textArray1[3, 5, 5] = "";
        textArray1[3, 5, 6] = "";
        textArray1[3, 5, 7] = "";
        textArray1[3, 5, 8] = "";
        textArray1[3, 5, 9] = "";
        textArray1[3, 5, 10] = "";
        textArray1[3, 5, 11] = "";
        textArray1[3, 5, 12] = "";
        textArray1[3, 5, 13] = "";
        textArray1[3, 5, 14] = "";
        textArray1[3, 5, 15] = "";
        textArray1[3, 5, 0x10] = "";
        textArray1[3, 5, 0x11] = "";
        textArray1[3, 5, 0x12] = "";
        textArray1[3, 5, 0x13] = "";
        textArray1[3, 5, 20] = "";
        textArray1[3, 5, 0x15] = "";
        textArray1[3, 6, 0] = "V";
        textArray1[3, 6, 1] = "V";
        textArray1[3, 6, 2] = "V";
        textArray1[3, 6, 3] = "E";
        textArray1[3, 6, 4] = "E";
        textArray1[3, 6, 5] = "";
        textArray1[3, 6, 6] = "";
        textArray1[3, 6, 7] = "";
        textArray1[3, 6, 8] = "";
        textArray1[3, 6, 9] = "";
        textArray1[3, 6, 10] = "";
        textArray1[3, 6, 11] = "";
        textArray1[3, 6, 12] = "";
        textArray1[3, 6, 13] = "";
        textArray1[3, 6, 14] = "";
        textArray1[3, 6, 15] = "";
        textArray1[3, 6, 0x10] = "";
        textArray1[3, 6, 0x11] = "";
        textArray1[3, 6, 0x12] = "";
        textArray1[3, 6, 0x13] = "";
        textArray1[3, 6, 20] = "";
        textArray1[3, 6, 0x15] = "";
        textArray1[4, 0, 0] = "V";
        textArray1[4, 0, 1] = "V";
        textArray1[4, 0, 2] = "V";
        textArray1[4, 0, 3] = "V";
        textArray1[4, 0, 4] = "V";
        textArray1[4, 0, 5] = "I";
        textArray1[4, 0, 6] = "I";
        textArray1[4, 0, 7] = "I";
        textArray1[4, 0, 8] = "I";
        textArray1[4, 0, 9] = "I";
        textArray1[4, 0, 10] = "I";
        textArray1[4, 0, 11] = "I";
        textArray1[4, 0, 12] = "E";
        textArray1[4, 0, 13] = "V";
        textArray1[4, 0, 14] = "V";
        textArray1[4, 0, 15] = "I";
        textArray1[4, 0, 0x10] = "I";
        textArray1[4, 0, 0x11] = "I";
        textArray1[4, 0, 0x12] = "I";
        textArray1[4, 0, 0x13] = "I";
        textArray1[4, 0, 20] = "I";
        textArray1[4, 0, 0x15] = "I";
        textArray1[4, 1, 0] = "V";
        textArray1[4, 1, 1] = "V";
        textArray1[4, 1, 2] = "V";
        textArray1[4, 1, 3] = "E";
        textArray1[4, 1, 4] = "I";
        textArray1[4, 1, 5] = "I";
        textArray1[4, 1, 6] = "I";
        textArray1[4, 1, 7] = "I";
        textArray1[4, 1, 8] = "I";
        textArray1[4, 1, 9] = "I";
        textArray1[4, 1, 10] = "I";
        textArray1[4, 1, 11] = "I";
        textArray1[4, 1, 12] = "I";
        textArray1[4, 1, 13] = "I";
        textArray1[4, 1, 14] = "I";
        textArray1[4, 1, 15] = "";
        textArray1[4, 1, 0x10] = "";
        textArray1[4, 1, 0x11] = "";
        textArray1[4, 1, 0x12] = "";
        textArray1[4, 1, 0x13] = "";
        textArray1[4, 1, 20] = "";
        textArray1[4, 1, 0x15] = "";
        textArray1[4, 2, 0] = "V";
        textArray1[4, 2, 1] = "V";
        textArray1[4, 2, 2] = "I";
        textArray1[4, 2, 3] = "I";
        textArray1[4, 2, 4] = "I";
        textArray1[4, 2, 5] = "I";
        textArray1[4, 2, 6] = "";
        textArray1[4, 2, 7] = "";
        textArray1[4, 2, 8] = "";
        textArray1[4, 2, 9] = "";
        textArray1[4, 2, 10] = "";
        textArray1[4, 2, 11] = "";
        textArray1[4, 2, 12] = "";
        textArray1[4, 2, 13] = "";
        textArray1[4, 2, 14] = "";
        textArray1[4, 2, 15] = "";
        textArray1[4, 2, 0x10] = "";
        textArray1[4, 2, 0x11] = "";
        textArray1[4, 2, 0x12] = "";
        textArray1[4, 2, 0x13] = "";
        textArray1[4, 2, 20] = "";
        textArray1[4, 2, 0x15] = "";
        textArray1[4, 3, 0] = "";
        textArray1[4, 3, 1] = "";
        textArray1[4, 3, 2] = "";
        textArray1[4, 3, 3] = "";
        textArray1[4, 3, 4] = "";
        textArray1[4, 3, 5] = "";
        textArray1[4, 3, 6] = "";
        textArray1[4, 3, 7] = "";
        textArray1[4, 3, 8] = "";
        textArray1[4, 3, 9] = "";
        textArray1[4, 3, 10] = "";
        textArray1[4, 3, 11] = "";
        textArray1[4, 3, 12] = "";
        textArray1[4, 3, 13] = "";
        textArray1[4, 3, 14] = "";
        textArray1[4, 3, 15] = "";
        textArray1[4, 3, 0x10] = "";
        textArray1[4, 3, 0x11] = "";
        textArray1[4, 3, 0x12] = "";
        textArray1[4, 3, 0x13] = "";
        textArray1[4, 3, 20] = "";
        textArray1[4, 3, 0x15] = "";
        textArray1[4, 4, 0] = "";
        textArray1[4, 4, 1] = "";
        textArray1[4, 4, 2] = "";
        textArray1[4, 4, 3] = "";
        textArray1[4, 4, 4] = "";
        textArray1[4, 4, 5] = "";
        textArray1[4, 4, 6] = "";
        textArray1[4, 4, 7] = "";
        textArray1[4, 4, 8] = "";
        textArray1[4, 4, 9] = "";
        textArray1[4, 4, 10] = "";
        textArray1[4, 4, 11] = "";
        textArray1[4, 4, 12] = "";
        textArray1[4, 4, 13] = "";
        textArray1[4, 4, 14] = "";
        textArray1[4, 4, 15] = "";
        textArray1[4, 4, 0x10] = "";
        textArray1[4, 4, 0x11] = "";
        textArray1[4, 4, 0x12] = "";
        textArray1[4, 4, 0x13] = "";
        textArray1[4, 4, 20] = "";
        textArray1[4, 4, 0x15] = "";
        textArray1[4, 5, 0] = "";
        textArray1[4, 5, 1] = "";
        textArray1[4, 5, 2] = "";
        textArray1[4, 5, 3] = "";
        textArray1[4, 5, 4] = "";
        textArray1[4, 5, 5] = "";
        textArray1[4, 5, 6] = "";
        textArray1[4, 5, 7] = "";
        textArray1[4, 5, 8] = "";
        textArray1[4, 5, 9] = "";
        textArray1[4, 5, 10] = "";
        textArray1[4, 5, 11] = "";
        textArray1[4, 5, 12] = "";
        textArray1[4, 5, 13] = "";
        textArray1[4, 5, 14] = "";
        textArray1[4, 5, 15] = "";
        textArray1[4, 5, 0x10] = "";
        textArray1[4, 5, 0x11] = "";
        textArray1[4, 5, 0x12] = "";
        textArray1[4, 5, 0x13] = "";
        textArray1[4, 5, 20] = "";
        textArray1[4, 5, 0x15] = "";
        textArray1[4, 6, 0] = "V";
        textArray1[4, 6, 1] = "V";
        textArray1[4, 6, 2] = "V";
        textArray1[4, 6, 3] = "E";
        textArray1[4, 6, 4] = "E";
        textArray1[4, 6, 5] = "";
        textArray1[4, 6, 6] = "";
        textArray1[4, 6, 7] = "";
        textArray1[4, 6, 8] = "";
        textArray1[4, 6, 9] = "";
        textArray1[4, 6, 10] = "";
        textArray1[4, 6, 11] = "";
        textArray1[4, 6, 12] = "";
        textArray1[4, 6, 13] = "";
        textArray1[4, 6, 14] = "";
        textArray1[4, 6, 15] = "";
        textArray1[4, 6, 0x10] = "";
        textArray1[4, 6, 0x11] = "";
        textArray1[4, 6, 0x12] = "";
        textArray1[4, 6, 0x13] = "";
        textArray1[4, 6, 20] = "";
        textArray1[4, 6, 0x15] = "";
        textArray1[5, 0, 0] = "V";
        textArray1[5, 0, 1] = "V";
        textArray1[5, 0, 2] = "V";
        textArray1[5, 0, 3] = "V";
        textArray1[5, 0, 4] = "V";
        textArray1[5, 0, 5] = "E";
        textArray1[5, 0, 6] = "E";
        textArray1[5, 0, 7] = "E";
        textArray1[5, 0, 8] = "E";
        textArray1[5, 0, 9] = "E";
        textArray1[5, 0, 10] = "I";
        textArray1[5, 0, 11] = "I";
        textArray1[5, 0, 12] = "I";
        textArray1[5, 0, 13] = "I";
        textArray1[5, 0, 14] = "V";
        textArray1[5, 0, 15] = "V";
        textArray1[5, 0, 0x10] = "V";
        textArray1[5, 0, 0x11] = "I";
        textArray1[5, 0, 0x12] = "X";
        textArray1[5, 0, 0x13] = "V";
        textArray1[5, 0, 20] = "V";
        textArray1[5, 0, 0x15] = "V";
        textArray1[5, 1, 0] = "V";
        textArray1[5, 1, 1] = "V";
        textArray1[5, 1, 2] = "V";
        textArray1[5, 1, 3] = "E";
        textArray1[5, 1, 4] = "X";
        textArray1[5, 1, 5] = "V";
        textArray1[5, 1, 6] = "V";
        textArray1[5, 1, 7] = "V";
        textArray1[5, 1, 8] = "V";
        textArray1[5, 1, 9] = "V";
        textArray1[5, 1, 10] = "V";
        textArray1[5, 1, 11] = "V";
        textArray1[5, 1, 12] = "V";
        textArray1[5, 1, 13] = "V";
        textArray1[5, 1, 14] = "V";
        textArray1[5, 1, 15] = "";
        textArray1[5, 1, 0x10] = "";
        textArray1[5, 1, 0x11] = "";
        textArray1[5, 1, 0x12] = "";
        textArray1[5, 1, 0x13] = "";
        textArray1[5, 1, 20] = "";
        textArray1[5, 1, 0x15] = "";
        textArray1[5, 2, 0] = "V";
        textArray1[5, 2, 1] = "V";
        textArray1[5, 2, 2] = "V";
        textArray1[5, 2, 3] = "V";
        textArray1[5, 2, 4] = "V";
        textArray1[5, 2, 5] = "V";
        textArray1[5, 2, 6] = "";
        textArray1[5, 2, 7] = "";
        textArray1[5, 2, 8] = "";
        textArray1[5, 2, 9] = "";
        textArray1[5, 2, 10] = "";
        textArray1[5, 2, 11] = "";
        textArray1[5, 2, 12] = "";
        textArray1[5, 2, 13] = "";
        textArray1[5, 2, 14] = "";
        textArray1[5, 2, 15] = "";
        textArray1[5, 2, 0x10] = "";
        textArray1[5, 2, 0x11] = "";
        textArray1[5, 2, 0x12] = "";
        textArray1[5, 2, 0x13] = "";
        textArray1[5, 2, 20] = "";
        textArray1[5, 2, 0x15] = "";
        textArray1[5, 3, 0] = "";
        textArray1[5, 3, 1] = "";
        textArray1[5, 3, 2] = "";
        textArray1[5, 3, 3] = "";
        textArray1[5, 3, 4] = "";
        textArray1[5, 3, 5] = "";
        textArray1[5, 3, 6] = "";
        textArray1[5, 3, 7] = "";
        textArray1[5, 3, 8] = "";
        textArray1[5, 3, 9] = "";
        textArray1[5, 3, 10] = "";
        textArray1[5, 3, 11] = "";
        textArray1[5, 3, 12] = "";
        textArray1[5, 3, 13] = "";
        textArray1[5, 3, 14] = "";
        textArray1[5, 3, 15] = "";
        textArray1[5, 3, 0x10] = "";
        textArray1[5, 3, 0x11] = "";
        textArray1[5, 3, 0x12] = "";
        textArray1[5, 3, 0x13] = "";
        textArray1[5, 3, 20] = "";
        textArray1[5, 3, 0x15] = "";
        textArray1[5, 4, 0] = "V";
        textArray1[5, 4, 1] = "V";
        textArray1[5, 4, 2] = "I";
        textArray1[5, 4, 3] = "V";
        textArray1[5, 4, 4] = "I";
        textArray1[5, 4, 5] = "I";
        textArray1[5, 4, 6] = "L";
        textArray1[5, 4, 7] = "V";
        textArray1[5, 4, 8] = "X";
        textArray1[5, 4, 9] = "L";
        textArray1[5, 4, 10] = "X";
        textArray1[5, 4, 11] = "E";
        textArray1[5, 4, 12] = "E";
        textArray1[5, 4, 13] = "";
        textArray1[5, 4, 14] = "";
        textArray1[5, 4, 15] = "";
        textArray1[5, 4, 0x10] = "";
        textArray1[5, 4, 0x11] = "";
        textArray1[5, 4, 0x12] = "";
        textArray1[5, 4, 0x13] = "";
        textArray1[5, 4, 20] = "";
        textArray1[5, 4, 0x15] = "";
        textArray1[5, 5, 0] = "V";
        textArray1[5, 5, 1] = "V";
        textArray1[5, 5, 2] = "V";
        textArray1[5, 5, 3] = "V";
        textArray1[5, 5, 4] = "E";
        textArray1[5, 5, 5] = "V";
        textArray1[5, 5, 6] = "V";
        textArray1[5, 5, 7] = "E";
        textArray1[5, 5, 8] = "V";
        textArray1[5, 5, 9] = "";
        textArray1[5, 5, 10] = "";
        textArray1[5, 5, 11] = "";
        textArray1[5, 5, 12] = "";
        textArray1[5, 5, 13] = "";
        textArray1[5, 5, 14] = "";
        textArray1[5, 5, 15] = "";
        textArray1[5, 5, 0x10] = "";
        textArray1[5, 5, 0x11] = "";
        textArray1[5, 5, 0x12] = "";
        textArray1[5, 5, 0x13] = "";
        textArray1[5, 5, 20] = "";
        textArray1[5, 5, 0x15] = "";
        textArray1[5, 6, 0] = "V";
        textArray1[5, 6, 1] = "V";
        textArray1[5, 6, 2] = "V";
        textArray1[5, 6, 3] = "E";
        textArray1[5, 6, 4] = "E";
        textArray1[5, 6, 5] = "";
        textArray1[5, 6, 6] = "";
        textArray1[5, 6, 7] = "";
        textArray1[5, 6, 8] = "";
        textArray1[5, 6, 9] = "";
        textArray1[5, 6, 10] = "";
        textArray1[5, 6, 11] = "";
        textArray1[5, 6, 12] = "";
        textArray1[5, 6, 13] = "";
        textArray1[5, 6, 14] = "";
        textArray1[5, 6, 15] = "";
        textArray1[5, 6, 0x10] = "";
        textArray1[5, 6, 0x11] = "";
        textArray1[5, 6, 0x12] = "";
        textArray1[5, 6, 0x13] = "";
        textArray1[5, 6, 20] = "";
        textArray1[5, 6, 0x15] = "";
        textArray1[6, 0, 0] = "V";
        textArray1[6, 0, 1] = "V";
        textArray1[6, 0, 2] = "V";
        textArray1[6, 0, 3] = "V";
        textArray1[6, 0, 4] = "V";
        textArray1[6, 0, 5] = "E";
        textArray1[6, 0, 6] = "E";
        textArray1[6, 0, 7] = "E";
        textArray1[6, 0, 8] = "E";
        textArray1[6, 0, 9] = "E";
        textArray1[6, 0, 10] = "I";
        textArray1[6, 0, 11] = "I";
        textArray1[6, 0, 12] = "I";
        textArray1[6, 0, 13] = "I";
        textArray1[6, 0, 14] = "V";
        textArray1[6, 0, 15] = "V";
        textArray1[6, 0, 0x10] = "V";
        textArray1[6, 0, 0x11] = "I";
        textArray1[6, 0, 0x12] = "X";
        textArray1[6, 0, 0x13] = "V";
        textArray1[6, 0, 20] = "V";
        textArray1[6, 0, 0x15] = "V";
        textArray1[6, 1, 0] = "V";
        textArray1[6, 1, 1] = "V";
        textArray1[6, 1, 2] = "V";
        textArray1[6, 1, 3] = "E";
        textArray1[6, 1, 4] = "X";
        textArray1[6, 1, 5] = "V";
        textArray1[6, 1, 6] = "V";
        textArray1[6, 1, 7] = "V";
        textArray1[6, 1, 8] = "V";
        textArray1[6, 1, 9] = "V";
        textArray1[6, 1, 10] = "V";
        textArray1[6, 1, 11] = "V";
        textArray1[6, 1, 12] = "I";
        textArray1[6, 1, 13] = "V";
        textArray1[6, 1, 14] = "V";
        textArray1[6, 1, 15] = "";
        textArray1[6, 1, 0x10] = "";
        textArray1[6, 1, 0x11] = "";
        textArray1[6, 1, 0x12] = "";
        textArray1[6, 1, 0x13] = "";
        textArray1[6, 1, 20] = "";
        textArray1[6, 1, 0x15] = "";
        textArray1[6, 2, 0] = "V";
        textArray1[6, 2, 1] = "V";
        textArray1[6, 2, 2] = "V";
        textArray1[6, 2, 3] = "V";
        textArray1[6, 2, 4] = "V";
        textArray1[6, 2, 5] = "V";
        textArray1[6, 2, 6] = "";
        textArray1[6, 2, 7] = "";
        textArray1[6, 2, 8] = "";
        textArray1[6, 2, 9] = "";
        textArray1[6, 2, 10] = "";
        textArray1[6, 2, 11] = "";
        textArray1[6, 2, 12] = "";
        textArray1[6, 2, 13] = "";
        textArray1[6, 2, 14] = "";
        textArray1[6, 2, 15] = "";
        textArray1[6, 2, 0x10] = "";
        textArray1[6, 2, 0x11] = "";
        textArray1[6, 2, 0x12] = "";
        textArray1[6, 2, 0x13] = "";
        textArray1[6, 2, 20] = "";
        textArray1[6, 2, 0x15] = "";
        textArray1[6, 3, 0] = "";
        textArray1[6, 3, 1] = "";
        textArray1[6, 3, 2] = "";
        textArray1[6, 3, 3] = "";
        textArray1[6, 3, 4] = "";
        textArray1[6, 3, 5] = "";
        textArray1[6, 3, 6] = "";
        textArray1[6, 3, 7] = "";
        textArray1[6, 3, 8] = "";
        textArray1[6, 3, 9] = "";
        textArray1[6, 3, 10] = "";
        textArray1[6, 3, 11] = "";
        textArray1[6, 3, 12] = "";
        textArray1[6, 3, 13] = "";
        textArray1[6, 3, 14] = "";
        textArray1[6, 3, 15] = "";
        textArray1[6, 3, 0x10] = "";
        textArray1[6, 3, 0x11] = "";
        textArray1[6, 3, 0x12] = "";
        textArray1[6, 3, 0x13] = "";
        textArray1[6, 3, 20] = "";
        textArray1[6, 3, 0x15] = "";
        textArray1[6, 4, 0] = "V";
        textArray1[6, 4, 1] = "V";
        textArray1[6, 4, 2] = "I";
        textArray1[6, 4, 3] = "V";
        textArray1[6, 4, 4] = "I";
        textArray1[6, 4, 5] = "I";
        textArray1[6, 4, 6] = "L";
        textArray1[6, 4, 7] = "V";
        textArray1[6, 4, 8] = "X";
        textArray1[6, 4, 9] = "L";
        textArray1[6, 4, 10] = "X";
        textArray1[6, 4, 11] = "E";
        textArray1[6, 4, 12] = "E";
        textArray1[6, 4, 13] = "";
        textArray1[6, 4, 14] = "";
        textArray1[6, 4, 15] = "";
        textArray1[6, 4, 0x10] = "";
        textArray1[6, 4, 0x11] = "";
        textArray1[6, 4, 0x12] = "";
        textArray1[6, 4, 0x13] = "";
        textArray1[6, 4, 20] = "";
        textArray1[6, 4, 0x15] = "";
        textArray1[6, 5, 0] = "V";
        textArray1[6, 5, 1] = "V";
        textArray1[6, 5, 2] = "V";
        textArray1[6, 5, 3] = "V";
        textArray1[6, 5, 4] = "E";
        textArray1[6, 5, 5] = "V";
        textArray1[6, 5, 6] = "V";
        textArray1[6, 5, 7] = "E";
        textArray1[6, 5, 8] = "V";
        textArray1[6, 5, 9] = "";
        textArray1[6, 5, 10] = "";
        textArray1[6, 5, 11] = "";
        textArray1[6, 5, 12] = "";
        textArray1[6, 5, 13] = "";
        textArray1[6, 5, 14] = "";
        textArray1[6, 5, 15] = "";
        textArray1[6, 5, 0x10] = "";
        textArray1[6, 5, 0x11] = "";
        textArray1[6, 5, 0x12] = "";
        textArray1[6, 5, 0x13] = "";
        textArray1[6, 5, 20] = "";
        textArray1[6, 5, 0x15] = "";
        textArray1[6, 6, 0] = "V";
        textArray1[6, 6, 1] = "V";
        textArray1[6, 6, 2] = "V";
        textArray1[6, 6, 3] = "E";
        textArray1[6, 6, 4] = "E";
        textArray1[6, 6, 5] = "";
        textArray1[6, 6, 6] = "";
        textArray1[6, 6, 7] = "";
        textArray1[6, 6, 8] = "";
        textArray1[6, 6, 9] = "";
        textArray1[6, 6, 10] = "";
        textArray1[6, 6, 11] = "";
        textArray1[6, 6, 12] = "";
        textArray1[6, 6, 13] = "";
        textArray1[6, 6, 14] = "";
        textArray1[6, 6, 15] = "";
        textArray1[6, 6, 0x10] = "";
        textArray1[6, 6, 0x11] = "";
        textArray1[6, 6, 0x12] = "";
        textArray1[6, 6, 0x13] = "";
        textArray1[6, 6, 20] = "";
        textArray1[6, 6, 0x15] = "";
        textArray1[7, 0, 0] = "V";
        textArray1[7, 0, 1] = "V";
        textArray1[7, 0, 2] = "V";
        textArray1[7, 0, 3] = "V";
        textArray1[7, 0, 4] = "V";
        textArray1[7, 0, 5] = "E";
        textArray1[7, 0, 6] = "E";
        textArray1[7, 0, 7] = "I";
        textArray1[7, 0, 8] = "I";
        textArray1[7, 0, 9] = "I";
        textArray1[7, 0, 10] = "E";
        textArray1[7, 0, 11] = "E";
        textArray1[7, 0, 12] = "E";
        textArray1[7, 0, 13] = "E";
        textArray1[7, 0, 14] = "V";
        textArray1[7, 0, 15] = "V";
        textArray1[7, 0, 0x10] = "V";
        textArray1[7, 0, 0x11] = "X";
        textArray1[7, 0, 0x12] = "X";
        textArray1[7, 0, 0x13] = "V";
        textArray1[7, 0, 20] = "V";
        textArray1[7, 0, 0x15] = "V";
        textArray1[7, 1, 0] = "V";
        textArray1[7, 1, 1] = "V";
        textArray1[7, 1, 2] = "V";
        textArray1[7, 1, 3] = "E";
        textArray1[7, 1, 4] = "X";
        textArray1[7, 1, 5] = "V";
        textArray1[7, 1, 6] = "V";
        textArray1[7, 1, 7] = "V";
        textArray1[7, 1, 8] = "V";
        textArray1[7, 1, 9] = "V";
        textArray1[7, 1, 10] = "V";
        textArray1[7, 1, 11] = "V";
        textArray1[7, 1, 12] = "I";
        textArray1[7, 1, 13] = "I";
        textArray1[7, 1, 14] = "V";
        textArray1[7, 1, 15] = "";
        textArray1[7, 1, 0x10] = "";
        textArray1[7, 1, 0x11] = "";
        textArray1[7, 1, 0x12] = "";
        textArray1[7, 1, 0x13] = "";
        textArray1[7, 1, 20] = "";
        textArray1[7, 1, 0x15] = "";
        textArray1[7, 2, 0] = "V";
        textArray1[7, 2, 1] = "V";
        textArray1[7, 2, 2] = "V";
        textArray1[7, 2, 3] = "V";
        textArray1[7, 2, 4] = "V";
        textArray1[7, 2, 5] = "V";
        textArray1[7, 2, 6] = "";
        textArray1[7, 2, 7] = "";
        textArray1[7, 2, 8] = "";
        textArray1[7, 2, 9] = "";
        textArray1[7, 2, 10] = "";
        textArray1[7, 2, 11] = "";
        textArray1[7, 2, 12] = "";
        textArray1[7, 2, 13] = "";
        textArray1[7, 2, 14] = "";
        textArray1[7, 2, 15] = "";
        textArray1[7, 2, 0x10] = "";
        textArray1[7, 2, 0x11] = "";
        textArray1[7, 2, 0x12] = "";
        textArray1[7, 2, 0x13] = "";
        textArray1[7, 2, 20] = "";
        textArray1[7, 2, 0x15] = "";
        textArray1[7, 3, 0] = "V";
        textArray1[7, 3, 1] = "V";
        textArray1[7, 3, 2] = "V";
        textArray1[7, 3, 3] = "";
        textArray1[7, 3, 4] = "";
        textArray1[7, 3, 5] = "";
        textArray1[7, 3, 6] = "";
        textArray1[7, 3, 7] = "";
        textArray1[7, 3, 8] = "";
        textArray1[7, 3, 9] = "";
        textArray1[7, 3, 10] = "";
        textArray1[7, 3, 11] = "";
        textArray1[7, 3, 12] = "";
        textArray1[7, 3, 13] = "";
        textArray1[7, 3, 14] = "";
        textArray1[7, 3, 15] = "";
        textArray1[7, 3, 0x10] = "";
        textArray1[7, 3, 0x11] = "";
        textArray1[7, 3, 0x12] = "";
        textArray1[7, 3, 0x13] = "";
        textArray1[7, 3, 20] = "";
        textArray1[7, 3, 0x15] = "";
        textArray1[7, 4, 0] = "V";
        textArray1[7, 4, 1] = "V";
        textArray1[7, 4, 2] = "V";
        textArray1[7, 4, 3] = "V";
        textArray1[7, 4, 4] = "I";
        textArray1[7, 4, 5] = "I";
        textArray1[7, 4, 6] = "L";
        textArray1[7, 4, 7] = "V";
        textArray1[7, 4, 8] = "X";
        textArray1[7, 4, 9] = "L";
        textArray1[7, 4, 10] = "X";
        textArray1[7, 4, 11] = "I";
        textArray1[7, 4, 12] = "I";
        textArray1[7, 4, 13] = "";
        textArray1[7, 4, 14] = "";
        textArray1[7, 4, 15] = "";
        textArray1[7, 4, 0x10] = "";
        textArray1[7, 4, 0x11] = "";
        textArray1[7, 4, 0x12] = "";
        textArray1[7, 4, 0x13] = "";
        textArray1[7, 4, 20] = "";
        textArray1[7, 4, 0x15] = "";
        textArray1[7, 5, 0] = "V";
        textArray1[7, 5, 1] = "V";
        textArray1[7, 5, 2] = "V";
        textArray1[7, 5, 3] = "V";
        textArray1[7, 5, 4] = "E";
        textArray1[7, 5, 5] = "V";
        textArray1[7, 5, 6] = "E";
        textArray1[7, 5, 7] = "E";
        textArray1[7, 5, 8] = "V";
        textArray1[7, 5, 9] = "";
        textArray1[7, 5, 10] = "";
        textArray1[7, 5, 11] = "";
        textArray1[7, 5, 12] = "";
        textArray1[7, 5, 13] = "";
        textArray1[7, 5, 14] = "";
        textArray1[7, 5, 15] = "";
        textArray1[7, 5, 0x10] = "";
        textArray1[7, 5, 0x11] = "";
        textArray1[7, 5, 0x12] = "";
        textArray1[7, 5, 0x13] = "";
        textArray1[7, 5, 20] = "";
        textArray1[7, 5, 0x15] = "";
        textArray1[7, 6, 0] = "V";
        textArray1[7, 6, 1] = "V";
        textArray1[7, 6, 2] = "V";
        textArray1[7, 6, 3] = "E";
        textArray1[7, 6, 4] = "E";
        textArray1[7, 6, 5] = "V";
        textArray1[7, 6, 6] = "";
        textArray1[7, 6, 7] = "";
        textArray1[7, 6, 8] = "";
        textArray1[7, 6, 9] = "";
        textArray1[7, 6, 10] = "";
        textArray1[7, 6, 11] = "";
        textArray1[7, 6, 12] = "";
        textArray1[7, 6, 13] = "";
        textArray1[7, 6, 14] = "";
        textArray1[7, 6, 15] = "";
        textArray1[7, 6, 0x10] = "";
        textArray1[7, 6, 0x11] = "";
        textArray1[7, 6, 0x12] = "";
        textArray1[7, 6, 0x13] = "";
        textArray1[7, 6, 20] = "";
        textArray1[7, 6, 0x15] = "";
        return textArray1;
    }

    public void showError(string message, string title, bool exit)
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        if (exit)
        {
            Environment.Exit(1);
        }
    }

    private void switchGame()
    {
        string str = "savegame";
        this.Text = "TotalWar SaveParser 'Celso'";
        if ((this.gameTag == 0) || ((this.gameTag == 1) || ((this.gameTag == 5) || ((this.gameTag == 6) || ((this.gameTag == 7) || ((this.gameTag == 3) || (this.gameTag == 2)))))))
        {
            this.tsmi_Multiplayer.Visible = true;
        }
        else
        {
            this.tsmi_Multiplayer.Visible = false;
            GlobalData.multiplayerSavegame = false;
        }
        this.tsmi_FileType.Visible = this.gameTag != 2;
        if (this.savegameFileType != 1)
        {
            this.setSavegamePaths();
        }
        else
        {
            string str2 = Path.Combine(this.dic_properties[GlobalData.gamePathProperty[this.gameTag]], @"data\campaigns\");
            this.openFileDialog_Savegame.InitialDirectory = str2;
            this.openFileDialog_Savegame.FileName = "*.esf";
            this.saveFileDialog_Savegame.InitialDirectory = str2;
            this.saveFileDialog_Savegame.FileName = "*.esf";
            str = "startpos";
        }
        this.tsmi_OpenFile.Text = "open " + str;
        this.tsmi_SaveFile.Text = "save " + str;
        this.tsmi_SaveFileAs.Text = "save " + str + " as";
        this.l_Status_GameTag.Text = GlobalData.gameTag[this.gameTag];
        if (this.cb_DisplayTable.SelectedIndex == -1)
        {
            this.cb_DisplayTable.SelectedIndex = 0;
        }
        this.dgv_ParserResult.DataSource = null;
        this.dgv_ParserResult.Refresh();
        SaveParser_Utils.gameTag = this.gameTag;
        if (SaveParser_Utils.nodes != null)
        {
            SaveParser_Utils.nodes.esfFile = null;
        }
        this.cb_DisplayTable_SelectedIndexChanged(null, null);
        this.tb_ParseFaction.Text = "0";
        this.setButtonsStatus(false);
        this.manageComponentsTabPageEdit();
        this.l_Status_Text.Text = "no savegame file loaded";
        GC.Collect();
        this.debug("Memory: " + $"{GC.GetTotalMemory(false):0,0}" + " / " + $"{GC.GetTotalMemory(false):0,0}", false);
    }

    private void tsmi_DebugWindow_Click(object sender, EventArgs e)
    {
        if ((this.windowDebug == null) || this.windowDebug.IsDisposed)
        {
            this.windowDebug = new Window_Debug("SaveParser debug window");
            this.windowDebug.Show();
            this.debugging = true;
        }
    }

    private void tsmi_EditConfigFile_Click(object sender, EventArgs e)
    {
        Process.Start("SaveParser.ini" ?? "");
    }

    private void tsmi_EditSF_Click(object sender, EventArgs e)
    {
        string str = this.dic_properties["EditSF_Path"];
        if (!File.Exists(Path.Combine(str, "EditSF.exe")))
        {
            this.l_Status_Text.Text = "EditSF is not in the directory as defined in SaveParser.ini.";
        }
        else
        {
            ProcessStartInfo startInfo = new ProcessStartInfo {
                FileName = Path.Combine(str, "EditSF.exe")
            };
            if (this.saveFileName != null)
            {
                startInfo.Arguments = "-f:\"" + this.saveFileName + "\"";
            }
            Process.Start(startInfo);
        }
    }

    private void tsmi_EditSFDialog_Click(object sender, EventArgs e)
    {
        this.openEditSFDialog(null, false);
    }

    private void tsmi_ExportInternalDBTables_Click(object sender, EventArgs e)
    {
        this.debug("Exporting internal DB tables...", false);
        string str = GlobalData.gameTag[this.gameTag] + ((this.mod != null) ? ("-" + this.mod) : "");
        this.exportDBTable<Data_Name>(DataBase.dbt_Names, "dbt_" + str + "_Names");
        this.exportDBTable<Data_Technology>(DataBase.dbt_Technologies, "dbt_" + str + "_Technologies");
        this.exportDBTable<Data_Portrait>(DataBase.dbt_Portraits, "dbt_" + str + "_Portraits");
        this.exportDBTable<Data_Trait>(DataBase.dbt_Traits, "dbt_" + str + "_Traits");
        this.exportDBTable<Data_TraitEffect>(DataBase.dbt_TraitEffects, "dbt_" + str + "_TraitEffects");
        this.exportDBTable<Data_Ancillary>(DataBase.dbt_Ancillaries, "dbt_" + str + "_Ancillaries");
        if ((this.gameTag == 5) || (this.gameTag == 6))
        {
            this.exportDBTable<Data_Faction>(DataBase.dbt_Factions, "dbt_" + str + "_Factions");
            this.exportDBTable<Data_Building>(DataBase.dbt_Buildings, "dbt_" + str + "_Buildings");
            this.exportDBTable<Data_Skill>(DataBase.dbt_Skills, "dbt_" + str + "_Skills");
            this.exportDBTable<Data_SkillEffect>(DataBase.dbt_SkillEffects, "dbt_" + str + "_SkillEffects");
            this.exportDBTable<Data_AncillaryEffect>(DataBase.dbt_AncillaryEffects, "dbt_" + str + "_AncillaryEffects");
        }
        else if ((this.gameTag != 0) && (this.gameTag != 1))
        {
            if (this.gameTag == 7)
            {
                this.exportDBTable<Data_Faction>(DataBase.dbt_Factions, "dbt_" + str + "_Factions");
                this.exportDBTable<Data_Skill>(DataBase.dbt_Skills, "dbt_" + str + "_Skills");
                this.exportDBTable<Data_SkillEffect>(DataBase.dbt_SkillEffects, "dbt_" + str + "_SkillEffects");
            }
        }
        else
        {
            this.exportDBTable<Data_Skill>(DataBase.dbt_Skills, "dbt_" + str + "_Skills");
            this.exportDBTable<Data_Faction>(DataBase.dbt_Factions, "dbt_" + str + "_Factions");
            this.exportDBTable<Data_Building>(DataBase.dbt_Buildings, "dbt_" + str + "_Buildings");
            this.exportDBTable<Data_Culture>(DataBase.dbt_Cultures, "dbt_" + str + "_Cultures");
            this.exportDBTable<Data_SkillEffect>(DataBase.dbt_SkillEffects, "dbt_" + str + "_SkillEffects");
            this.exportDBTable<Data_AncillaryEffect>(DataBase.dbt_AncillaryEffects, "dbt_" + str + "_AncillaryEffects");
            this.exportDBDictionary<string, string>(DataBase.dic_ArmyNames, "dic_" + str + "_ArmyNames");
        }
        this.debug("Exporting internal DB tables done.", false);
        this.l_Status_Text.Text = "Internal data tables exported.";
    }

    private void tsmi_ExportSavegame_Click(object sender, EventArgs e)
    {
        this.stopwatch = Stopwatch.StartNew();
        this.l_Status_Text.Text = "Exporting savegame...";
        string str = Path.GetFileNameWithoutExtension(this.saveFileName) + ".txt";
        TextWriter writer = new StreamWriter(Path.Combine(this.dic_properties["exportPath"], str));
        List<string> list = new List<string>();
        foreach (string str2 in Nodes.parseNodes(SaveParser_Utils.nodes.esfFile.RootNode as ParentNode, list, "", null))
        {
            writer.WriteLine(str2);
        }
        writer.Flush();
        writer.Close();
        this.l_Status_Text.Text = "Current savegame exported to:" + this.dic_properties["exportPath"] + @"\" + str;
        GC.Collect();
        this.stopwatch.Stop();
        this.debug("Savegame exported. (" + this.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s") + ")", false);
    }

    // EXPORT TABLE - importatant 
    private void tsmi_ExportTable_Click(object sender, EventArgs e)
    {
        string[] textArray1 = new string[] { "SaveParser_TableExport_", Path.GetFileNameWithoutExtension(this.saveFileName), "_", this.cb_DisplayTable.Text, ".txt" };
        string str = string.Concat(textArray1);
        TextWriter writer = new StreamWriter(Path.Combine(this.dic_properties["exportPath"], str));
        int num = -1;
        while (num < this.dgv_ParserResult.RowCount)
        {
            int num2 = 0;
            while (true)
            {
                if (num2 >= this.dgv_ParserResult.ColumnCount)
                {
                    writer.WriteLine();
                    num++;
                    break;
                }
                string headerText = null;
                if (num == -1)
                {
                    headerText = this.dgv_ParserResult.Columns[num2].HeaderText;
                }
                else
                {
                    object obj2 = this.dgv_ParserResult.Rows[num].Cells[num2].Value;
                    headerText = (obj2 != null) ? obj2.ToString() : "";
                }
                writer.Write(headerText + "\t");
                num2++;
            }
        }
        writer.Flush();
        writer.Close();
        this.l_Status_Text.Text = "Current table exported to:" + this.dic_properties["exportPath"] + @"\" + str;
    }

    private void tsmi_FileType_Click(object sender, EventArgs e)
    {
        this.savegameFileType = (this.savegameFileType == 0) ? 1 : 0;
        this.tsmi_FileType.Text = (this.savegameFileType == 0) ? "switch to Startpos" : "switch to savegame";
        this.switchGame();
    }

    private void tsmi_Game_Click(object sender, EventArgs e)
    {
        ToolStripMenuItem item = (ToolStripMenuItem) sender;
        char[] separator = new char[] { '|' };
        string[] strArray = item.ToolTipText.Split(separator);
        string str = (strArray.Length == 2) ? strArray[1] : null;
        this.tsmi_Rome2.Checked = false;
        this.tsmi_Shogun2.Checked = false;
        this.tsmi_Napoleon.Checked = false;
        this.tsmi_Empire.Checked = false;
        this.tsmi_Attila.Checked = false;
        this.tsmi_Warhammer.Checked = false;
        this.tsmi_Warhammer2.Checked = false;
        this.tsmi_Britannia.Checked = false;
        this.tsmi_ThreeKingdoms.Checked = false;
        foreach (ToolStripMenuItem item2 in this.tsmi_Mods)
        {
            if (item2 != null)
            {
                item2.Checked = false;
            }
        }
        int gameTag = this.gameTag;
        string s = strArray[0];
        uint num3 = <PrivateImplementationDetails>.ComputeStringHash(s);
        if (num3 <= 0x626d_61d2)
        {
            if (num3 <= 0x45f9_9e85)
            {
                if (num3 != 0x2cb_e230)
                {
                    if ((num3 == 0x45f9_9e85) && (s == "Empire"))
                    {
                        this.gameTag = 4;
                    }
                }
                else if (s == "Attila")
                {
                    this.gameTag = 0;
                }
            }
            else if (num3 != 0x5ebe_3a2b)
            {
                if ((num3 == 0x626d_61d2) && (s == "Rome2"))
                {
                    this.gameTag = 1;
                }
            }
            else if (s == "Napoleon")
            {
                this.gameTag = 3;
            }
        }
        else if (num3 <= 0xa6c4_68a7)
        {
            if (num3 != 0x6f5d_4c0f)
            {
                if ((num3 == 0xa6c4_68a7) && (s == "Shogun2"))
                {
                    this.gameTag = 2;
                }
            }
            else if (s == "Warhammer2")
            {
                // good to know. 
                this.gameTag = 6;
            }
        }
        else if (num3 == 0xa9d5_0927)
        {
            if (s == "Warhammer")
            {
                this.gameTag = 5;
            }
        }
        else if (num3 != 0xba7f_d085)
        {
            if ((num3 == 0xdc2b_d94d) && (s == "ThreeKingdoms"))
            {
                this.gameTag = 8;
            }
        }
        else if (s == "Britannia")
        {
            this.gameTag = 7;
        }
        item.Checked = true;
        if ((this.gameTag.ToString() + ((str == null) ? "" : str)) == (gameTag.ToString() + ((this.mod == null) ? "" : this.mod)))
        {
            this.debug("No game switch needed.", LogLevelType.info, false);
        }
        else
        {
            this.mod = str;
            DataBase.resetDatabase();
            if (this.mod == "Ancient Empires")
            {
                this.loadPackDataFromGame();
            }
            else if ((this.gameTag != 1) && ((this.gameTag != 0) && ((this.gameTag != 2) && ((this.gameTag != 5) && ((this.gameTag != 6) && ((this.gameTag != 7) && (this.gameTag != 8)))))))
            {
                DataLoader loader1 = new DataLoader(this);
                DataBase.dbt_Names = loader1.readData_Names(Path.Combine(this.config_AppDataPath, "Data_Names.txt"), GlobalData.gameTag[this.gameTag]);
                DataBase.dic_CharacterNames = loader1.getDictionary_CharacterNames(DataBase.dbt_Names);
            }
            else if (!this.usePackFiles)
            {
                this.loadPackDataFromFiles();
            }
            else
            {
                this.loadPackDataFromGame();
            }
            this.cb_DisplayTable.Items.Remove("province");
            this.cb_DisplayTable.Items.Remove("army");
            this.cb_DisplayTable.Items.Remove("unit");
            this.cb_DisplayTable.Items.Remove("diplomacy");
            if (this.cb_DisplayTable.Items.Contains("-"))
            {
                this.cb_DisplayTable.Items.Remove("-");
            }
            if ((this.gameTag == 1) || ((this.gameTag == 0) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
            {
                if (!this.tabControl_Main.TabPages.Contains(this.tabPage_Edit))
                {
                    this.tabControl_Main.TabPages.Insert(1, this.tabPage_Edit);
                }
                if (!this.tabControl_Main.TabPages.Contains(this.tabPage_Edit_Global))
                {
                    this.tabControl_Main.TabPages.Insert(2, this.tabPage_Edit_Global);
                }
                this.cb_DisplayTable.Items.Add("province");
                this.cb_DisplayTable.Items.Add("army");
                this.cb_DisplayTable.Items.Add("unit");
                this.cb_DisplayTable.Items.Add("diplomacy");
            }
            else if (this.gameTag != 8)
            {
                if (this.gameTag != 2)
                {
                    this.tabControl_Main.TabPages.Remove(this.tabPage_Edit);
                    this.tabControl_Main.TabPages.Remove(this.tabPage_Edit_Global);
                }
                else
                {
                    if (!this.tabControl_Main.TabPages.Contains(this.tabPage_Edit))
                    {
                        this.tabControl_Main.TabPages.Insert(1, this.tabPage_Edit);
                    }
                    if (!this.tabControl_Main.TabPages.Contains(this.tabPage_Edit_Global))
                    {
                        this.tabControl_Main.TabPages.Insert(2, this.tabPage_Edit_Global);
                    }
                    this.cb_DisplayTable.Items.Add("-");
                    this.cb_DisplayTable.Items.Add("army");
                    this.cb_DisplayTable.Items.Add("unit");
                }
            }
            this.switchGame();
        }
    }

    private void tsmi_InternalDBViewer_Click(object sender, EventArgs e)
    {
        if ((this.windowTable == null) || this.windowTable.IsDisposed)
        {
            List<KeyValuePair<string, string>> list1;
            List<KeyValuePair<string, Data_Ancillary>> list3;
            List<KeyValuePair<string, string>> list4;
            List<KeyValuePair<uint, string>> list5;
            List<KeyValuePair<int, string>> list6;
            List<KeyValuePair<int, string>> list7;
            List<KeyValuePair<int, string>> list8;
            List<KeyValuePair<int, string>> list9;
            List<KeyValuePair<uint, int>> list10;
            List<KeyValuePair<int, string>> list11;
            List<KeyValuePair<int, int>> list12;
            List<KeyValuePair<uint, string>> list13;
            List<KeyValuePair<uint, Table_Data_Army>> list14;
            List<KeyValuePair<uint, int>> list15;
            List<KeyValuePair<uint, int>> list16;
            ArrayList lists = new ArrayList();
            string[] textArray1 = new string[0x1b];
            textArray1[0] = "Table.CharacterNames";
            textArray1[1] = "Table.Ancillaries";
            textArray1[2] = "Table.Skills";
            textArray1[3] = "Table.Traits";
            textArray1[4] = "Table.Technologies";
            textArray1[5] = "Table.Portraits";
            textArray1[6] = "Table.Factions";
            textArray1[7] = "Table.Buildings";
            textArray1[8] = "Table.Cultures";
            textArray1[9] = "Table.SkillEffects";
            textArray1[10] = "Table.TraitEffects";
            textArray1[11] = "Table.AncillaryEffects";
            textArray1[12] = "Dictionary.CharacterNames";
            textArray1[13] = "Dictionary.Ancillaries";
            textArray1[14] = "Dictionary.ArmyNames";
            textArray1[15] = "Dictionary.CharacterIdName";
            textArray1[0x10] = "Dictionary.FactionIndexName";
            textArray1[0x11] = "Dictionary.RegionIndexName";
            textArray1[0x12] = "Dictionary.ProvinceIndexName";
            textArray1[0x13] = "Dictionary.FactionIdName";
            textArray1[20] = "Dictionary.CharacterIdIndex";
            textArray1[0x15] = "Dictionary.FactionIndexNameActive";
            textArray1[0x16] = "Dictionary.FactionIndexId";
            textArray1[0x17] = "Dictionary.Offices";
            textArray1[0x18] = "Dictionary.Armies";
            textArray1[0x19] = "Dictionary.ArmyIdRegion";
            textArray1[0x1a] = "Dictionary.Governors";
            string[] strArray = textArray1;
            string[] textArray2 = new string[0x1b];
            textArray2[0] = "Pack";
            textArray2[1] = "Pack";
            textArray2[2] = "Pack";
            textArray2[3] = "Pack";
            textArray2[4] = "Pack";
            textArray2[5] = "Pack";
            textArray2[6] = "Pack";
            textArray2[7] = "Pack";
            textArray2[8] = "Pack";
            textArray2[9] = "Pack";
            textArray2[10] = "Pack";
            textArray2[11] = "Pack";
            textArray2[12] = "Pack";
            textArray2[13] = "Pack";
            textArray2[14] = "Pack";
            textArray2[15] = "Game";
            textArray2[0x10] = "Game";
            textArray2[0x11] = "Game";
            textArray2[0x12] = "Game";
            textArray2[0x13] = "Game";
            textArray2[20] = "Game";
            textArray2[0x15] = "Game";
            textArray2[0x16] = "Game";
            textArray2[0x17] = "Game";
            textArray2[0x18] = "Game";
            textArray2[0x19] = "Game";
            textArray2[0x1a] = "Game";
            string[] strArray2 = textArray2;
            IList[] listArray1 = new IList[0x1b];
            listArray1[0] = DataBase.dbt_Names;
            listArray1[1] = DataBase.dbt_Ancillaries;
            listArray1[2] = DataBase.dbt_Skills;
            listArray1[3] = DataBase.dbt_Traits;
            listArray1[4] = DataBase.dbt_Technologies;
            listArray1[5] = DataBase.dbt_Portraits;
            listArray1[6] = DataBase.dbt_Factions;
            listArray1[7] = DataBase.dbt_Buildings;
            listArray1[8] = DataBase.dbt_Cultures;
            listArray1[9] = DataBase.dbt_SkillEffects;
            listArray1[10] = DataBase.dbt_TraitEffects;
            listArray1[11] = DataBase.dbt_AncillaryEffects;
            IList[] listArray2 = listArray1;
            if (DataBase.dic_CharacterNames != null)
            {
                list1 = DataBase.dic_CharacterNames.ToList<KeyValuePair<string, string>>();
            }
            else
            {
                Dictionary<string, string> local1 = DataBase.dic_CharacterNames;
                list1 = null;
            }
            listArray1[12] = list1;
            IList[] listArray3 = listArray1;
            IList[] listArray4 = listArray1;
            if (DataBase.dic_Ancillaries != null)
            {
                list3 = DataBase.dic_Ancillaries.ToList<KeyValuePair<string, Data_Ancillary>>();
            }
            else
            {
                Dictionary<string, Data_Ancillary> local2 = DataBase.dic_Ancillaries;
                list3 = null;
            }
            listArray1[13] = list3;
            IList[] listArray5 = listArray1;
            IList[] listArray6 = listArray1;
            if (DataBase.dic_ArmyNames != null)
            {
                list4 = DataBase.dic_ArmyNames.ToList<KeyValuePair<string, string>>();
            }
            else
            {
                Dictionary<string, string> local3 = DataBase.dic_ArmyNames;
                list4 = null;
            }
            listArray1[14] = list4;
            IList[] listArray7 = listArray1;
            IList[] listArray8 = listArray1;
            if (DataBase.dic_CharacterIdName != null)
            {
                list5 = DataBase.dic_CharacterIdName.ToList<KeyValuePair<uint, string>>();
            }
            else
            {
                Dictionary<uint, string> local4 = DataBase.dic_CharacterIdName;
                list5 = null;
            }
            listArray1[15] = list5;
            IList[] listArray9 = listArray1;
            IList[] listArray10 = listArray1;
            if (DataBase.dic_FactionIndexName != null)
            {
                list6 = DataBase.dic_FactionIndexName.ToList<KeyValuePair<int, string>>();
            }
            else
            {
                Dictionary<int, string> local5 = DataBase.dic_FactionIndexName;
                list6 = null;
            }
            listArray1[0x10] = list6;
            IList[] listArray11 = listArray1;
            IList[] listArray12 = listArray1;
            if (DataBase.dic_RegionIndexName != null)
            {
                list7 = DataBase.dic_RegionIndexName.ToList<KeyValuePair<int, string>>();
            }
            else
            {
                Dictionary<int, string> local6 = DataBase.dic_RegionIndexName;
                list7 = null;
            }
            listArray1[0x11] = list7;
            IList[] listArray13 = listArray1;
            IList[] listArray14 = listArray1;
            if (DataBase.dic_ProvinceIndexName != null)
            {
                list8 = DataBase.dic_ProvinceIndexName.ToList<KeyValuePair<int, string>>();
            }
            else
            {
                Dictionary<int, string> local7 = DataBase.dic_ProvinceIndexName;
                list8 = null;
            }
            listArray1[0x12] = list8;
            IList[] listArray15 = listArray1;
            IList[] listArray16 = listArray1;
            if (DataBase.dic_FactionIdName != null)
            {
                list9 = DataBase.dic_FactionIdName.ToList<KeyValuePair<int, string>>();
            }
            else
            {
                Dictionary<int, string> local8 = DataBase.dic_FactionIdName;
                list9 = null;
            }
            listArray1[0x13] = list9;
            IList[] listArray17 = listArray1;
            IList[] listArray18 = listArray1;
            if (DataBase.dic_CharacterIdIndex != null)
            {
                list10 = DataBase.dic_CharacterIdIndex.ToList<KeyValuePair<uint, int>>();
            }
            else
            {
                Dictionary<uint, int> local9 = DataBase.dic_CharacterIdIndex;
                list10 = null;
            }
            listArray1[20] = list10;
            IList[] listArray19 = listArray1;
            IList[] listArray20 = listArray1;
            if (DataBase.dic_FactionIndexNameActive != null)
            {
                list11 = DataBase.dic_FactionIndexNameActive.ToList<KeyValuePair<int, string>>();
            }
            else
            {
                Dictionary<int, string> local10 = DataBase.dic_FactionIndexNameActive;
                list11 = null;
            }
            listArray1[0x15] = list11;
            IList[] listArray21 = listArray1;
            IList[] listArray22 = listArray1;
            if (DataBase.dic_FactionIndexId != null)
            {
                list12 = DataBase.dic_FactionIndexId.ToList<KeyValuePair<int, int>>();
            }
            else
            {
                Dictionary<int, int> local11 = DataBase.dic_FactionIndexId;
                list12 = null;
            }
            listArray1[0x16] = list12;
            IList[] listArray23 = listArray1;
            IList[] listArray24 = listArray1;
            if (DataBase.dic_Offices != null)
            {
                list13 = DataBase.dic_Offices.ToList<KeyValuePair<uint, string>>();
            }
            else
            {
                Dictionary<uint, string> local12 = DataBase.dic_Offices;
                list13 = null;
            }
            listArray1[0x17] = list13;
            IList[] listArray25 = listArray1;
            IList[] listArray26 = listArray1;
            if (DataBase.dic_Armies != null)
            {
                list14 = DataBase.dic_Armies.ToList<KeyValuePair<uint, Table_Data_Army>>();
            }
            else
            {
                Dictionary<uint, Table_Data_Army> local13 = DataBase.dic_Armies;
                list14 = null;
            }
            listArray1[0x18] = list14;
            IList[] listArray27 = listArray1;
            IList[] listArray28 = listArray1;
            if (DataBase.dic_ArmyIdRegion != null)
            {
                list15 = DataBase.dic_ArmyIdRegion.ToList<KeyValuePair<uint, int>>();
            }
            else
            {
                Dictionary<uint, int> local14 = DataBase.dic_ArmyIdRegion;
                list15 = null;
            }
            listArray1[0x19] = list15;
            IList[] listArray29 = listArray1;
            IList[] listArray30 = listArray1;
            if (DataBase.dic_Governors != null)
            {
                list16 = DataBase.dic_Governors.ToList<KeyValuePair<uint, int>>();
            }
            else
            {
                Dictionary<uint, int> local15 = DataBase.dic_Governors;
                list16 = null;
            }
            listArray1[0x1a] = list16;
            IList[] listArray = listArray1;
            int index = 0;
            while (true)
            {
                if (index >= strArray.Length)
                {
                    Window_DB_Viewer viewer1 = new Window_DB_Viewer();
                    viewer1.setTableData(lists);
                    viewer1.Show();
                    break;
                }
                ArrayList list2 = new ArrayList {
                    strArray[index],
                    strArray2[index],
                    listArray[index]
                };
                lists.Add(list2);
                index++;
            }
        }
    }

    private void tsmi_JumpToEditSF_Click(object sender, EventArgs e)
    {
        string str = "CAMPAIGN_SAVE_GAME/" + (((this.gameTag == 4) || (this.gameTag == 3)) ? "" : "COMPRESSED_DATA/") + "CAMPAIGN_ENV/CAMPAIGN_MODEL/WORLD/";
        int num = 0;
        int num2 = (this.cb_DisplayTable.SelectedIndex == 0) ? 2 : 1;
        int num3 = -1;
        num = Convert.ToInt32(this.dgv_ParserResult.Rows[this.clickedRowIndex].Cells[0].Value);
        if ((this.cb_DisplayTable.SelectedIndex != 1) && ((this.cb_DisplayTable.SelectedIndex != 2) && (this.cb_DisplayTable.SelectedIndex != 3)))
        {
            num2 = Convert.ToInt32(this.dgv_ParserResult.Rows[this.clickedRowIndex].Cells[num2].Value);
        }
        if (this.cb_DisplayTable.SelectedIndex == 5)
        {
            num3 = Convert.ToInt32(this.dgv_ParserResult.Rows[this.clickedRowIndex].Cells[2].Value);
        }
        if (this.cb_DisplayTable.SelectedIndex == 6)
        {
            num = Convert.ToInt32(this.tb_ParseFaction.Text);
            num2 = Convert.ToInt32(this.dgv_ParserResult.Rows[this.clickedRowIndex].Cells[0].Value);
        }
        string str2 = str + "FACTION_ARRAY/FACTION_ARRAY - " + num.ToString() + "/FACTION";
        string treeNodeJumpToPath = null;
        if (this.cb_DisplayTable.SelectedIndex == 0)
        {
            treeNodeJumpToPath = str2 + "/CHARACTER_ARRAY/CHARACTER_ARRAY - " + num2.ToString() + "/CHARACTER";
        }
        if (this.cb_DisplayTable.SelectedIndex == 1)
        {
            treeNodeJumpToPath = str2;
        }
        if (this.cb_DisplayTable.SelectedIndex == 4)
        {
            treeNodeJumpToPath = str2 + "/ARMY_ARRAY/ARMY_ARRAY - " + num2.ToString();
        }
        if (this.cb_DisplayTable.SelectedIndex == 5)
        {
            string[] textArray1 = new string[] { str2, "/ARMY_ARRAY/ARMY_ARRAY - ", num2.ToString(), "/MILITARY_FORCE/UNIT_CONTAINER/UNITS_ARRAY/UNITS_ARRAY - ", num3.ToString(), "/UNIT" };
            treeNodeJumpToPath = string.Concat(textArray1);
        }
        if (this.cb_DisplayTable.SelectedIndex == 6)
        {
            treeNodeJumpToPath = str2 + ((this.gameTag == 1) ? "/DIPLOMACY_MANAGER" : "/OLD_DIPLOMACY_MANAGER") + "/DIPLOMACY_RELATIONSHIPS_ARRAY/DIPLOMACY_RELATIONSHIPS_ARRAY - " + num2.ToString();
        }
        if (this.cb_DisplayTable.SelectedIndex == 2)
        {
            treeNodeJumpToPath = str + "/REGION_MANAGER/REGIONS_ARRAY/REGIONS_ARRAY - " + num.ToString() + "/REGION";
        }
        if (this.cb_DisplayTable.SelectedIndex == 3)
        {
            treeNodeJumpToPath = str + "/PROVINCE_MANAGER/PROVINCE_ARRAY/PROVINCE_ARRAY - " + num.ToString();
        }
        this.openEditSFDialog(treeNodeJumpToPath, sender.ToString().Contains("view"));
    }

    private void tsmi_Mulitplayer_Click(object sender, EventArgs e)
    {
        GlobalData.multiplayerSavegame = this.tsmi_Multiplayer.Checked;
        this.setSavegamePaths();
    }

    private void tsmi_OpenBatchController_Click(object sender, EventArgs e)
    {
        Window_Batch batch = new Window_Batch(this, this.dic_properties["scriptPath"], SaveParser_Utils.nodes.esfFile);
        batch.ShowDialog();
        if (batch.DialogResult == DialogResult.OK)
        {
            SaveParser_Utils.nodes.esfFile = batch.file;
        }
        batch.Dispose();
    }

    private void tsmi_OpenFile_Click(object sender, EventArgs e)
    {
        if (this.openFileDialog_Savegame.ShowDialog() == DialogResult.OK)
        {
            DateTime now = DateTime.Now;
            Cursor.Current = Cursors.WaitCursor;
            this.l_Status_Text.Text = "Loading savegame file '" + this.openFileDialog_Savegame.FileName + "'... ";
            this.l_Status_Text.Refresh();
            string[] textArray1 = new string[9];
            textArray1[0] = "Reading savegame file:\n- type          : ";
            textArray1[1] = this.savegameFileType.ToString();
            textArray1[2] = "\n- multiplayer   : ";
            textArray1[3] = GlobalData.multiplayerSavegame.ToString();
            textArray1[4] = "\n- game          : ";
            textArray1[5] = GlobalData.gameTag[this.gameTag];
            textArray1[6] = "\n- file          : '";
            textArray1[7] = this.openFileDialog_Savegame.FileName;
            textArray1[8] = "' ...";
            this.debug(string.Concat(textArray1), false);
            this.saveFileName = this.openFileDialog_Savegame.FileName;
            SaveParser_Utils.nodes = new Nodes(EsfCodecUtil.LoadEsfFile(this.openFileDialog_Savegame.FileName), this.gameTag);
            bool flag = (SaveParser_Utils.nodes.esfFile.RootNode as ParentNode).ToString().Contains("MULTI");
            this.debug("- isMultiplayer : " + flag.ToString(), false);
            bool flag2 = false;
            if (GlobalData.multiplayerSavegame != flag)
            {
                this.showError("Savegame loading mismatch:\na) tried to load a multiplayer-savegame although SaveParser is set to singleplayer\nb) tried to load a singleplayer-savegame although SaveParser is set to mulitplayer\nPlease verfiy setting in 'File' menu.", "Setting Error", false);
                flag2 = true;
                this.l_Status_Text.Text = "savegame file NOT loaded.";
                SaveParser_Utils.nodes = null;
            }
            if ((this.gameTag == 2) && !SaveParser_Utils.nodes.checkCompressed())
            {
                this.l_Status_Text.Text = "no savegame file loaded.";
                MessageBox.Show("ERROR: 'Shogun 2' savegame was saved in uncompressed format. Please re-save this savegame in the game.", "Error Reading Savegame File", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                flag2 = true;
                SaveParser_Utils.nodes = null;
            }
            if (!flag2)
            {
                string fileName = Path.GetFileName(this.openFileDialog_Savegame.FileName);
                this.Text = "SaveParser (" + fileName + ")";
                this.parserFunctions.initDBs();
                this.nup_Global_TPY.Text = GlobalData.tpy.ToString();
                this.setButtonsStatus(true);
                this.dgv_ParserResult.DataSource = null;
                this.dgv_ParserResult.Refresh();
                this.setContent_FactionArrayCombobox();
                this.cb_DisplayTable_SelectedIndexChanged(null, null);
                this.tb_Global_CampaignTag.Text = GlobalData.campaignTag;
                if (this.gameTag == 0)
                {
                    int num2 = this.parserFunctions.getClimate();
                    this.nup_Global_Climate.Value = num2;
                }
                if ((this.gameTag == 0) || ((this.gameTag == 1) || ((this.gameTag == 5) || ((this.gameTag == 6) || ((this.gameTag == 2) || (this.gameTag == 7))))))
                {
                    int num3 = this.parserFunctions.getMaxUnits();
                    this.cb_Global_MaxUnits.Items.Clear();
                    this.cb_Global_MaxUnits.Items.Add("20");
                    this.cb_Global_MaxUnits.Items.Add("40");
                    string item = Convert.ToString(num3);
                    if ((num3 != 20) || (num3 != 40))
                    {
                        this.cb_Global_MaxUnits.Items.Add(item);
                    }
                    this.cb_Global_MaxUnits.Text = Convert.ToString(num3);
                }
                if ((this.gameTag == 0) || ((this.gameTag == 1) || (this.gameTag == 7)))
                {
                    string str3 = this.parserFunctions.getUnitSizeScale();
                    this.cb_Global_UnitSizeScale.Items.Clear();
                    this.cb_Global_UnitSizeScale.Items.Add("0.35");
                    this.cb_Global_UnitSizeScale.Items.Add("0.50");
                    this.cb_Global_UnitSizeScale.Items.Add("0.75");
                    this.cb_Global_UnitSizeScale.Items.Add("1");
                    this.cb_Global_UnitSizeScale.Items.Add("2");
                    this.cb_Global_UnitSizeScale.Items.Add("4");
                    this.cb_Global_UnitSizeScale.Text = str3;
                }
                if ((this.gameTag == 0) || ((this.gameTag == 1) || ((this.gameTag == 5) || ((this.gameTag == 6) || (this.gameTag == 7)))))
                {
                    if (!this.loadCampaignMap())
                    {
                        this.b_Map.Enabled = false;
                    }
                    if ((this.gameTag == 5) && (GlobalData.campaignTag == "wh_wh"))
                    {
                        this.b_Map.Enabled = false;
                    }
                }
                this.l_Status_Text.Text = "savegame file loaded.";
            }
            Cursor.Current = Cursors.Default;
            double num = Math.Round(DateTime.Now.Subtract(now).TotalSeconds, 2);
            this.debug("- campaign    : " + GlobalData.campaignTag, false);
            this.debug("- duration    : " + num.ToString() + " s", false);
            GC.Collect();
            this.debug("- memory      : " + $"{GC.GetTotalMemory(false):0,0}" + " / " + $"{GC.GetTotalMemory(false):0,0}", false);
        }
    }

    private void tsmi_OpenFile_CustomFile_Click(object sender, EventArgs e)
    {
        string local1 = this.dic_properties["customFileCommand"];
        ProcessStartInfo info1 = new ProcessStartInfo();
        string path = local1;
        if (local1 == null)
        {
            string local2 = local1;
            path = "";
        }
        info1.FileName = Path.GetFileName(path);
        ProcessStartInfo startInfo = info1;
        startInfo.Arguments = "\"" + this.dic_properties["customFileArg"] + "\"";
        Process.Start(startInfo);
    }

    private void tsmi_OpenFile_Data_Click(object sender, EventArgs e)
    {
        string str = sender.ToString();
        str = str.Substring(0, 1).ToUpper() + str.Substring(1);
        Process.Start(@"Data\Data_Names.txt" ?? "");
    }

    private void tsmi_Options_ActiveFactions_Click(object sender, EventArgs e)
    {
        this.tsmi_Options_ActiveFactions.Checked = !this.tsmi_Options_ActiveFactions.Checked;
        this.setContent_FactionArrayCombobox();
    }

    private void tsmi_PayPal_Click(object sender, EventArgs e)
    {
        Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NCTVHVT2L6HUJ");
    }

    private void tsmi_PFM_Click(object sender, EventArgs e)
    {
        string str = this.dic_properties["PFM_Path"];
        if (File.Exists(Path.Combine(str, "PackFileManager.exe")))
        {
            Process.Start(Path.Combine(str, "PackFileManager.exe"));
        }
        else
        {
            this.l_Status_Text.Text = "PFM is not in the directory as defined in SaveParser.ini.";
        }
    }

    private void tsmi_ReadMe_Click(object sender, EventArgs e)
    {
        Process.Start("ReadMe.txt" ?? "");
    }

    private void tsmi_RestartConfig_Click(object sender, EventArgs e)
    {
        this.readINIFile();
    }

    private void tsmi_SaveFile_Click(object sender, EventArgs e)
    {
        string str2;
        str2 = str2 = Path.GetFileNameWithoutExtension(this.saveFileName) + "_SP" + Path.GetExtension(this.saveFileName);
        this.saveGame(str2, Path.GetDirectoryName(this.saveFileName) + @"\");
    }

    private void tsmi_SaveFileAs_Click(object sender, EventArgs e)
    {
        this.saveFileDialog_Savegame.FileName = "*." + GlobalData.savefileExtension[this.gameTag] + (GlobalData.multiplayerSavegame ? "_multiplayer" : "");
        if (this.saveFileDialog_Savegame.ShowDialog() == DialogResult.OK)
        {
            string fileName = Path.GetFileName(this.saveFileDialog_Savegame.FileName);
            this.saveGame(fileName, Path.GetDirectoryName(this.saveFileDialog_Savegame.FileName) + @"\");
        }
    }

    private void tsmi_SavegameFiles_Click(object sender, EventArgs e)
    {
        if ((this.windowTable == null) || this.windowTable.IsDisposed)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"The Creative Assembly\" + GlobalData.savefileDirectory[this.gameTag] + @"\save_games\");
            ArrayList lists = new ArrayList();
            string[] files = Directory.GetFiles(path, "*." + GlobalData.savefileExtension[this.gameTag]);
            int index = 0;
            while (true)
            {
                if (index >= files.Length)
                {
                    this.windowTable = new Window_Table(GlobalData.savefileDirectory[this.gameTag] + " Savegames");
                    string[] headers = new string[] { "Campaign", "FileName", "Faction", "Time", "Turn" };
                    this.windowTable.setTableData(headers, lists);
                    this.windowTable.Show();
                    GC.Collect();
                    break;
                }
                string filename = files[index];
                EsfFile esfFile = EsfCodecUtil.LoadEsfFile(filename);
                List<object> list2 = this.parserFunctions.getSavegameHeader(esfFile);
                ArrayList list3 = new ArrayList();
                string str3 = filename.Substring(path.Length);
                list3.Add(list2[0]);
                list3.Add(str3.Substring(0, (str3.Length - GlobalData.savefileExtension[this.gameTag].Length) - 1));
                list3.Add(list2[1]);
                list3.Add(list2[2]);
                list3.Add(list2[3]);
                lists.Add(list3);
                index++;
            }
        }
    }

    private void tsmi_SaveViewer_Click(object sender, EventArgs e)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo {
            FileName = "SaveViewer.exe"
        };
        if (this.saveFileName != null)
        {
            startInfo.Arguments = "-f:\"" + this.saveFileName + "\"";
        }
        try
        {
            Process.Start(startInfo);
        }
        catch (Exception)
        {
        }
    }

    // Nested Types
    [CompilerGenerated]
    private struct <loadPackDataFromGame>d__27 : IAsyncStateMachine
    {
        // Fields
        public int <>1__state;
        public AsyncVoidMethodBuilder <>t__builder;
        public SaveParser.SaveParser <>4__this;
        private TaskAwaiter <>u__1;

        // Methods
        private void MoveNext()
        {
            int num = this.<>1__state;
            SaveParser.SaveParser gui = this.<>4__this;
            try
            {
                string str;
                string str2;
                TaskAwaiter awaiter;
                if (num == 0)
                {
                    awaiter = this.<>u__1;
                    this.<>u__1 = new TaskAwaiter();
                    this.<>1__state = num = -1;
                    goto TR_0011;
                }
                else
                {
                    gui.Enabled = false;
                    gui.stopwatch = Stopwatch.StartNew();
                    gui.debug("Loading " + GlobalData.gameName[gui.gameTag] + ((gui.mod != null) ? ("|" + gui.mod) : "") + " pack data from game packs...", false);
                    gui.l_Status_Text.Text = "Loading pack data from game packs...";
                    gui.l_Status_Text.Refresh();
                    Cursor.Current = Cursors.WaitCursor;
                    str = gui.dic_properties[GlobalData.gamePathProperty[gui.gameTag]];
                    try
                    {
                        BinaryReader reader1 = new BinaryReader(new FileStream(Path.Combine(str, "data", (gui.gameTag == 1) ? "data_rome2.pack" : "data.pack"), FileMode.Open), Encoding.ASCII);
                        goto TR_0013;
                    }
                    catch (IOException)
                    {
                        gui.debug("Warning: " + GlobalData.gameName[gui.gameTag] + " is already running. Pack files cannot be accessed.", LogLevelType.warning, false);
                        string path = @"data\dbt_" + GlobalData.gameTag[gui.gameTag] + "_Names.xml";
                        bool flag = File.Exists(path);
                        gui.debug("Try to switching to XMLPackData mode: checking for file '" + path + "': " + flag.ToString(), LogLevelType.info, false);
                        gui.showError(GlobalData.gameName[gui.gameTag] + " is already running and data pack files are locked by the game. " + (flag ? "It seems XMLPackData files (NoGameDataPack) are installed, loading these files instead." : "Start SaveParser first and if necessary switch to the desired game in the menu 'Game' before starting the game."), "File access problem", !flag);
                        if (flag)
                        {
                            gui.loadPackDataFromFiles();
                        }
                        gui.Enabled = true;
                    }
                }
                goto TR_0002;
            TR_0011:
                awaiter.GetResult();
                gui.debug("- PackParser successfully executed.", false);
                IPackParser parser1 = PackParser.getInstance();
                Dictionary<string, string> dictionaryArmyName = parser1.DictionaryArmyName;
                Dictionary<string, PackParser.GenericPackItem> dic = parser1.Skills.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary8 = parser1.Factions.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary9 = parser1.Buildings.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary10 = parser1.Cultures.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary11 = parser1.SkillEffects.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary12 = parser1.TraitEffects.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary13 = parser1.TraitLevels.getBindableList();
                Dictionary<string, PackParser.GenericPackItem> dictionary14 = parser1.AncillaryEffects.getBindableList();
                DataLoader loader = new DataLoader(gui);
                DataBase.dbt_Portraits = loader.loadData_Portraits(parser1.Portraits);
                DataBase.dbt_Technologies = loader.loadData_Technologies(parser1.Technologies);
                DataBase.dbt_Names = loader.updateData_Names(parser1.DictionaryCharacterName, parser1.CharacterNames.getBindableList(), parser1.CharacterNameOrders.getBindableList(), gui.gameTag);
                DataBase.dic_CharacterNames = loader.getDictionary_CharacterNames(DataBase.dbt_Names);
                DataBase.dbt_Traits = loader.loadData_Traits(parser1.Traits.getBindableList());
                DataBase.dbt_Ancillaries = loader.loadData_Ancillaries(parser1.Ancillaries.getBindableList());
                if (gui.gameTag == 6)
                {
                    DataBase.dbt_Skills = loader.loadData_Skills(dic);
                    DataBase.dbt_SkillEffects = loader.loadData_SkillEffects(dictionary11);
                    DataBase.dbt_Factions = loader.loadData_Factions(dictionary8);
                    DataBase.dbt_Buildings = loader.loadData_Buildings(dictionary9);
                    DataBase.dbt_TraitEffects = loader.loadData_TraitEffects(dictionary12, dictionary13);
                    DataBase.dbt_AncillaryEffects = loader.loadData_AncillaryEffects(dictionary14);
                }
                else if (gui.gameTag == 5)
                {
                    DataBase.dbt_Skills = loader.loadData_Skills(dic);
                    DataBase.dbt_SkillEffects = loader.loadData_SkillEffects(dictionary11);
                    DataBase.dbt_Factions = loader.loadData_Factions(dictionary8);
                    DataBase.dbt_Buildings = loader.loadData_Buildings(dictionary9);
                    DataBase.dbt_TraitEffects = loader.loadData_TraitEffects(dictionary12, dictionary13);
                    DataBase.dbt_AncillaryEffects = loader.loadData_AncillaryEffects(dictionary14);
                }
                else if (gui.gameTag == 7)
                {
                    DataBase.dbt_Factions = loader.loadData_Factions(dictionary8);
                    DataBase.dbt_Skills = loader.loadData_Skills(dic);
                    DataBase.dbt_SkillEffects = loader.loadData_SkillEffects(dictionary11);
                    DataBase.dbt_TraitEffects = loader.loadData_TraitEffects(dictionary12, dictionary13);
                }
                else if (gui.gameTag == 2)
                {
                    DataBase.dbt_TraitEffects = loader.loadData_TraitEffects(dictionary12);
                }
                else
                {
                    DataBase.dbt_Skills = loader.loadData_Skills(dic);
                    DataBase.dbt_SkillEffects = loader.loadData_SkillEffects(dictionary11);
                    DataBase.dbt_Factions = loader.loadData_Factions(dictionary8);
                    DataBase.dbt_Buildings = loader.loadData_Buildings(dictionary9);
                    DataBase.dbt_Cultures = loader.loadData_Cultures(dictionary10);
                    DataBase.dic_Ancillaries = loader.getDictionary_Ancillaries(DataBase.dbt_Ancillaries);
                    DataBase.dbt_AncillaryEffects = loader.loadData_AncillaryEffects(dictionary14);
                    DataBase.dic_ArmyNames = dictionaryArmyName;
                    DataBase.dbt_TraitEffects = loader.loadData_TraitEffects(dictionary12, dictionary13);
                }
                Cursor.Current = Cursors.Default;
                gui.stopwatch.Stop();
                gui.debug(GlobalData.gameName[gui.gameTag] + " pack data processed. (" + gui.stopwatch.Elapsed.TotalSeconds.ToString("0.00 s") + ")", false);
                GC.Collect();
                gui.Enabled = true;
                goto TR_0002;
            TR_0013:
                str2 = (gui.mod != null) ? gui.dic_ModShortPackFile[gui.mod] : null;
                awaiter = PackParser.initialise(str, gui.debugging ? gui : null, gui.gameTag, str2).GetAwaiter();
                if (awaiter.IsCompleted)
                {
                    goto TR_0011;
                }
                else
                {
                    this.<>1__state = num = 0;
                    this.<>u__1 = awaiter;
                    this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SaveParser.SaveParser.<loadPackDataFromGame>d__27>(ref awaiter, ref this);
                }
            }
            catch (Exception exception)
            {
                this.<>1__state = -2;
                this.<>t__builder.SetException(exception);
            }
            return;
        TR_0002:
            this.<>1__state = -2;
            this.<>t__builder.SetResult();
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.<>t__builder.SetStateMachine(stateMachine);
        }
    }

    private class SelectedTable
    {
        // Fields
        public const int character = 0;
        public const int faction = 1;
        public const int region = 2;
        public const int province = 3;
        public const int army = 4;
        public const int unit = 5;
        public const int diplomacy = 6;
    }
}

 
Collapse Methods
 
