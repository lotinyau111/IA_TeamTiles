using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class MainPageManager : MonoBehaviour
{
    // database 
    public GameObject databaseManager;

    public GameObject canvasMainPage, canvasHTP, canvasLeaderboard, canvasSettings;
    public GameObject mpBtnStartGametxt, mpBtnHTP, mpBtnLeaderBoard, mpBtnSettings;
    public GameObject playerName, lblLogout;

    public GameObject lblLeaderboard, lblHeader;
    public GameObject lbOrderBy, lblBtnByScore, lblBtnByRound, togOnlyMe;
    public GameObject BtnTxtByScore, BtnTxtByRound, TxtOnlyMe;

    public GameObject lbIndex, lbPlayerName, lbScore, lbRound;
    public GameObject lbPageNum, lbPreviousPage, lbNextPage;

    public GameObject lblHTP, lblPlayGuideContent;

    public GameObject canvasStsAccount, canvasStsPassword, canvasStsGeneral;
    public GameObject btntxtStsAccount, btntxtStsPassword, btntxtStsGeneral;

    public GameObject stsAcclblUsername, stsAccTxtUsername, stsAcclblpName, stsAccTxtpName;
    public GameObject stsAcclblEmail, stsAccTxtEmail, stsAccBtnVerifyEmail, stsAccBtnTxtVerifyEmail;
    public GameObject stsAccVerifyMsg, stsAccVerifyTxtPin, stsAccBtnPinSubmit, stsAccBtnTxtPinSubmit, stsAccVerifiedTxt;

    public GameObject stsAcclblLanguages, stsAccDDLanguages;
    public GameObject stsAcclbl2FA, stsAccTog2FA, stsAccToglbl2FA;

    public GameObject canvasClearCfm, stsAccClrCfmMsg, stsAccClrCfmBtnYs, stsAccClrCfmBtnTxtYs, stsAccClrCfmBtnCancel, stsAccClrCfmBtnTxtCancel;
    public GameObject stsAccBtnSave, stsAccBtnTxtCSave, stsAccBtnClear, stsAccBtnTxtClear;

    public GameObject canvasSaveCfm, stsAccSCfmMsg, stsAccSCBtnSave, stsAccSCBtnNotSave, stsAccSCBtnCancel;
    public GameObject stsAccSCBtnTxtSave, stsAccSCBtnTxtNotSave, stsAccSCBtnTxtCancel;

    public GameObject changePwdTitle;
    public GameObject stsPwdlblOldPwd, stsPwdlblNewPwd, stsPwdlblRePwd;
    public GameObject stsPwdTxtOldPwd, stsPwdTxtNewPwd, stsPwdTxtRePwd;
    public GameObject stsPwdHint1, stsPwdHint2, stsPwdHint3, stsPwdHint4, stsPwdHint5, stsPwdWarningMsg;
    public GameObject stsPwdBtnClr, stsPwdBtnTxtClr, stsPwdBtnSave, stsPwdBtnTxtSave;

    public GameObject stsGElblSEVolume, stsGEMuteSEVolume, stsGESLSEVolume, stsGElblSETxtVolume;
    public GameObject stsGElblBGMVolume, stsGEMuteBGMVolume, stsGESLBGMVolume, stsGElblBGMTxtVolume;

    LanguageText lt = new();
    Mailing mail = new();
    int leaderboardPage;
    string accCEOTP, newTempEmail;
    DateTime accCEOTPExpiry;
    int wrongAccCEOTP;

    bool settingsEdited = false;
    bool dataChanged;

    // Start is called before the first frame update
    void Start()
    {
        onloadMainPage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set display value while page load
    public async void onloadMainPage()
    {
        string sql = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("username, playerName, lang", "useraccount", ("username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "'"));
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sql);


        playerName.GetComponent<Text>().text = lt.getDisplayValue(lt.welcome) + ", " + databaseManager.GetComponent<DatabaseManager>().getData(0, 1);
        mpBtnStartGametxt.GetComponent<Text>().text = lt.getDisplayValue(lt.startGame);
        mpBtnHTP.GetComponent<Text>().text = lt.getDisplayValue(lt.howToPlay);
        mpBtnSettings.GetComponent<Text>().text = lt.getDisplayValue(lt.settings);
        mpBtnLeaderBoard.GetComponent<Text>().text = lt.getDisplayValue(lt.leaderboard);
        lblLogout.GetComponent<Text>().text = lt.getDisplayValue(lt.signOut);
    }

    // Change to Start Game Scene
    public void mpStartGame() { GetComponent<ChangeScene>().nextScene(); }

    // Go back to Home Page
    public void mpHome()
    {
        canvasHTP.SetActive(false);
        canvasLeaderboard.SetActive(false);
        canvasSettings.SetActive(false);
        canvasMainPage.SetActive(true);
    }

    // Show How to Play
    public void mpHowToPlay()
    {

        canvasMainPage.SetActive(false);
        canvasHTP.SetActive(true);

        // Set display in How to Play page
        lblHTP.GetComponent<Text>().text = lt.getDisplayValue(lt.howToPlay);
        lblPlayGuideContent.GetComponent<Text>().text = lt.howToPlayLang();
    }

    public void mpLeaderboard()
    {
        // Show leaderboard Page
        canvasMainPage.SetActive(false);
        canvasLeaderboard.SetActive(true);
        lblLeaderboard.GetComponent<Text>().text = lt.getDisplayValue(lt.leaderboard);
        lblHeader.GetComponent<Text>().text = lt.getDisplayValue(lt.lbHeader);
        lbOrderBy.GetComponent<Text>().text = lt.getDisplayValue(lt.orderBy);
        BtnTxtByScore.GetComponent<Text>().text = lt.getDisplayValue(lt.byScore);
        BtnTxtByRound.GetComponent<Text>().text = lt.getDisplayValue(lt.byRound);
        TxtOnlyMe.GetComponent<Text>().text = lt.getDisplayValue(lt.onlyme);

        lborderByScore();
    }

    public async void lborderByScore()
    {
        leaderboardPage = 1;

        // Show Leaderboard order by Score.
        lblBtnByScore.GetComponent<Image>().color = lt.getTextColor("#C8C8C8");
        lblBtnByRound.GetComponent<Image>().color = lt.getTextColor("#FFFFFF");
        string lbByScoreSQL;
        lbByScoreSQL = "SELECT useraccount.playerName, leaderboard.score, leaderboard.round FROM useraccount, leaderboard WHERE leaderboard.username = useraccount.username ";
        if (togOnlyMe.GetComponent<Toggle>().isOn) lbByScoreSQL += " AND leaderboard.username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "' ";
        lbByScoreSQL += " ORDER BY leaderboard.score DESC;";

        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(lbByScoreSQL);
        showLeaderboardData();
    }

    public async void lborderByRound()
    {
        leaderboardPage = 1;


        // Show Leaderboard order by Round.
        lblBtnByScore.GetComponent<Image>().color = lt.getTextColor("#FFFFFF");
        lblBtnByRound.GetComponent<Image>().color = lt.getTextColor("#C8C8C8");
        string lbByRoundSQL;
        lbByRoundSQL = "SELECT useraccount.playerName, leaderboard.score, leaderboard.round FROM useraccount, leaderboard WHERE leaderboard.username = useraccount.username ";
        if (togOnlyMe.GetComponent<Toggle>().isOn) lbByRoundSQL += " AND leaderboard.username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "' ";
        lbByRoundSQL += " ORDER BY leaderboard.round DESC;";

        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(lbByRoundSQL);
        showLeaderboardData();
    }

    public void showLeaderboardData()
    {
        lbPageNum.GetComponent<Text>().text = lt.getDisplayValue(lt.page) + leaderboardPage + "/ " + lt.getDisplayValue(lt.page) + 10;

        // set of page controller
        if (leaderboardPage > 9) lbNextPage.SetActive(false);
        else lbNextPage.SetActive(true);
        if (leaderboardPage < 2) lbPreviousPage.SetActive(false);
        else lbPreviousPage.SetActive(true);

        // clear old data from database. 
        lbIndex.GetComponent<Text>().text = string.Empty;
        lbPlayerName.GetComponent<Text>().text = string.Empty;
        lbScore.GetComponent<Text>().text = string.Empty;
        lbRound.GetComponent<Text>().text = string.Empty;

        // Set Index number of Leaderboard. 
        for (int i = 0; i < 10; i++)
        {
            lbIndex.GetComponent<Text>().text += (((leaderboardPage - 1) * 10) + (i + 1)) + ". \n";
        }

        // Set data of Leaderboard
        for (int i = 0; i < 10; i++)
        {
            try
            {
                lbPlayerName.GetComponent<Text>().text += databaseManager.GetComponent<DatabaseManager>().getData(((leaderboardPage - 1) * 10 + i), 0) + "\n";
                lbScore.GetComponent<Text>().text += databaseManager.GetComponent<DatabaseManager>().getData(((leaderboardPage - 1) * 10 + i), 1) + "\n";
                lbRound.GetComponent<Text>().text += databaseManager.GetComponent<DatabaseManager>().getData(((leaderboardPage - 1) * 10 + i), 2) + "\n";
            }
            catch (IndexOutOfRangeException) { }
        }
    }

    public void lbgotoNextPage()
    {
        // Switch Next page of Leaderboard
        leaderboardPage++;
        showLeaderboardData();
    }

    public void lbgotoPreviousPage()
    {
        // Switch Previous page of Leaderboard
        leaderboardPage--;
        showLeaderboardData();
    }

    public void mpLogout()
    {
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Logout", "Logout"));

        // Clear current player details
        PlayerPrefs.DeleteKey(lt.keyCurrentPlayer);
        GetComponent<ChangeScene>().previousScene();
    }

    public void mpSettings()
    {
        // Switch to Settings Page
        canvasMainPage.SetActive(false);
        canvasSettings.SetActive(true);

        // Set Setting Page text
        btntxtStsAccount.GetComponent<Text>().text = lt.getDisplayValue(lt.account);
        btntxtStsPassword.GetComponent<Text>().text = lt.getDisplayValue(lt.password);
        btntxtStsGeneral.GetComponent<Text>().text = lt.getDisplayValue(lt.general);

        // load Settings Account page
        stsAccount();
    }

    public async void stsAccount()
    {
        if (settingsEdited)
        {
            await checkdataChanged();
            if (dataChanged) stsAccSaveConfirm();
        }

        if (!dataChanged)
        {
            // Clear Password Entry
            stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
            stsPwdWarningMsg.SetActive(false);


            // Switch to Account Page
            canvasStsAccount.SetActive(true);
            canvasStsPassword.SetActive(false);
            canvasStsGeneral.SetActive(false);

            // Set Account Display value
            stsAcclblUsername.GetComponent<Text>().text = lt.getDisplayValue(lt.username);
            stsAcclblpName.GetComponent<Text>().text = lt.getDisplayValue(lt.playerName);
            stsAcclblEmail.GetComponent<Text>().text = lt.getDisplayValue(lt.email);
            stsAccBtnVerifyEmail.SetActive(false);
            stsAccVerifyTxtPin.SetActive(false);
            stsAccBtnPinSubmit.SetActive(false);
            stsAcclblLanguages.GetComponent<Text>().text = lt.getDisplayValue(lt.language);
            stsAccTxtUsername.GetComponent<InputField>().text = PlayerPrefs.GetString(lt.keyCurrentPlayer);
            stsAcclbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt._2fa);
            stsAccBtnTxtClear.GetComponent<Text>().text = lt.getDisplayValue(lt.clear);
            stsAccBtnTxtCSave.GetComponent<Text>().text = lt.getDisplayValue(lt.save);

            // Assign language list (i.e., Lanaguge Package stored in Language Text
            List<string> langChoice = new List<string> { };
            for (int i = 0; i < lt.languageAvaliable.Length; i++)
                langChoice.Add(lt.languageAvaliable[i]);


            stsAccDDLanguages.GetComponent<Dropdown>().ClearOptions();
            stsAccDDLanguages.GetComponent<Dropdown>().AddOptions(langChoice);

            // Set player's data
            string accountDataSQL = "SELECT username, playerName, email, lang, 2fa from useraccount where username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";
            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(accountDataSQL);
            stsAccTxtpName.GetComponent<InputField>().text = databaseManager.GetComponent<DatabaseManager>().getData(0, 1);
            stsAccTxtEmail.GetComponent<InputField>().text = databaseManager.GetComponent<DatabaseManager>().getData(0, 2);
            stsAccDDLanguages.GetComponent<Dropdown>().value = int.Parse(databaseManager.GetComponent<DatabaseManager>().getData(0, 3));

            if (databaseManager.GetComponent<DatabaseManager>().getData(0, 4) == "0")
            {
                stsAccTog2FA.GetComponent<Toggle>().isOn = false;
                stsAccToglbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt.disable);
            }
            else if (databaseManager.GetComponent<DatabaseManager>().getData(0, 4) == "1")
            {
                stsAccTog2FA.GetComponent<Toggle>().isOn = true;
                stsAccToglbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt.enable);
            }
        }
    }

    public void stsAcc2FAlblTog()
    {
        if (stsAccTog2FA.GetComponent<Toggle>().isOn) stsAccToglbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt.enable);
        else stsAccToglbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt.disable);
    }

    public async Task checkdataChanged()
    {
        // search player's data
        string accountDataSQL = "SELECT username, playerName, email, lang, 2fa from useraccount where username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(accountDataSQL);

        // check if data changed
        dataChanged = false;
        if (stsAccTxtpName.GetComponent<InputField>().text != databaseManager.GetComponent<DatabaseManager>().getData(0, 1)) dataChanged = true;
        if (stsAccTxtEmail.GetComponent<InputField>().text != databaseManager.GetComponent<DatabaseManager>().getData(0, 2)) dataChanged = true;
        if (stsAccDDLanguages.GetComponent<Dropdown>().value.ToString() != databaseManager.GetComponent<DatabaseManager>().getData(0, 3)) dataChanged = true;
        if (stsAccTog2FA.GetComponent<Toggle>().isOn != (databaseManager.GetComponent<DatabaseManager>().getData(0, 4) == "1")) dataChanged = true;

    }

    public async void stsAccClearCfm()
    {
        if (settingsEdited)
        {
            // check if the data change 
            await checkdataChanged();
            if (dataChanged)
            {
                // Show Clear confirm dialog
                canvasClearCfm.SetActive(true);
                stsAccClrCfmMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.clearCfm);
                stsAccClrCfmBtnTxtCancel.GetComponent<Text>().text = lt.getDisplayValue(lt.cancel);
                stsAccClrCfmBtnTxtYs.GetComponent<Text>().text = lt.getDisplayValue(lt.yes);
            }
        }
    }

    public void stsAccSaveConfirm()
    {
        // Show save confirm dialog.
        canvasSaveCfm.SetActive(true);
        stsAccSCfmMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.saveCfm);
        stsAccSCBtnTxtSave.GetComponent<Text>().text = lt.getDisplayValue(lt.save);
        stsAccSCBtnTxtNotSave.GetComponent<Text>().text = lt.getDisplayValue(lt.notSave);
        stsAccSCBtnTxtCancel.GetComponent<Text>().text = lt.getDisplayValue(lt.cancel);
    }

    public async void stsAccSaveData()
    {
        // Show warning message in update data
        if (stsAccBtnVerifyEmail.activeSelf)
        {
            stsAccVerifyMsg.SetActive(true);
            stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.emailnotVerified);
        }


        if (stsAccVerifiedTxt.activeSelf)
        {
            string accountDataSQL = "SELECT username, playerName, email, lang, 2fa from useraccount where username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";
            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(accountDataSQL);
            // Send email to old email address; 
            string receipient = stsAccTxtpName.GetComponent<InputField>().text;
            string emailAddress = databaseManager.GetComponent<DatabaseManager>().getData(0, 1);
            mail.SendEmail(emailAddress, receipient, "Account Data updated.", lt.emailContentUpdateEmail("name", "Old Email", "new"));
        }

        if (!stsAccBtnVerifyEmail.activeSelf)
        {
            string receipient2 = stsAccTxtpName.GetComponent<InputField>().text;
            string emailAddress2 = stsAccTxtEmail.GetComponent<InputField>().text;
            mail.SendEmail(emailAddress2, receipient2, "Account Data updated.", lt.emailContentUpdateEmail("name", "Old Email", "new"));

            string updateDataSQL = "UPDATE `useraccount` SET ";
            updateDataSQL += " `playerName` = '" + stsAccTxtpName.GetComponent<InputField>().text + "', ";
            updateDataSQL += " `email` = '" + stsAccTxtEmail.GetComponent<InputField>().text + "', ";
            updateDataSQL += " `lang` = '" + stsAccDDLanguages.GetComponent<Dropdown>().value + "', ";

            if (stsAccTog2FA.GetComponent<Toggle>().isOn) updateDataSQL += " `2FA` = '1'";
            else updateDataSQL += " `2FA` = '0'";

            updateDataSQL += " WHERE `username` = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";

            Debug.Log(updateDataSQL);

            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(updateDataSQL);
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Data Update", "Data Update - User data update."));
            PlayerPrefs.SetInt(lt.keyLanguages, stsAccDDLanguages.GetComponent<Dropdown>().value);

            // Reload all data
            settingsEdited = false;
            dataChanged = false;
            stsAccount();
        }
    }

    public async void stsBack()
    {
        if (settingsEdited)
        {
            await checkdataChanged();
            if (dataChanged) stsAccSaveConfirm();
        }

        if (!dataChanged)
        {
            // Clear Password Entry
            stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
            stsPwdWarningMsg.SetActive(false);


            // Back to menu
            canvasMainPage.SetActive(true);
            canvasSettings.SetActive(false);

        }
    }

    public void emailChanged()
    {
        // check if email matched database record or new verified email record
        if (stsAccTxtEmail.GetComponent<InputField>().text != databaseManager.GetComponent<DatabaseManager>().getData(0, 2) && stsAccTxtEmail.GetComponent<InputField>().text != newTempEmail)
        {
            stsAccBtnVerifyEmail.SetActive(true);
            stsAccVerifiedTxt.SetActive(false);
            stsAccBtnTxtVerifyEmail.GetComponent<Text>().text = lt.getDisplayValue(lt.verify);
        }
        // if email equals database record show nothing
        else if (stsAccTxtEmail.GetComponent<InputField>().text == databaseManager.GetComponent<DatabaseManager>().getData(0, 2))
        {
            stsAccBtnVerifyEmail.SetActive(false);
            stsAccVerifiedTxt.SetActive(false);
        }
        // if email is an updated email, show verified
        else if (stsAccTxtEmail.GetComponent<InputField>().text == newTempEmail)
        {
            stsAccBtnVerifyEmail.SetActive(false);
            stsAccVerifiedTxt.SetActive(true);
        }
    }

    public void verifyEmail()
    {
        // Check if email format correct
        Regex rgEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$");

        // Show verify message
        stsAccVerifyMsg.SetActive(true);

        // Check if email format correct
        if (rgEmail.IsMatch(stsAccTxtEmail.GetComponent<InputField>().text)) updateEmail2FA();
        else stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.emailformat);

    }

    public void updateEmail2FA()
    {

        string emailAddress = stsAccTxtEmail.GetComponent<InputField>().text;
        string receipientName = PlayerPrefs.GetString(lt.keyCurrentPlayer);

        // generate OTP and convert to string
        int otp = UnityEngine.Random.Range(0, 999999);
        accCEOTP = otp.ToString("000000");

        // reset wrong number
        wrongAccCEOTP = 0;

        // set layout text 
        stsAccBtnTxtPinSubmit.GetComponent<Text>().text = lt.getDisplayValue(lt.submit);

        // send email and display input box
        mail.SendEmail(emailAddress, receipientName, "Update Email", lt.emailContentFP2FA(receipientName, accCEOTP));
        accCEOTPExpiry = DateTime.Now.AddMinutes(5);
        stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpSent);
        stsAccVerifyTxtPin.SetActive(true);
        stsAccBtnPinSubmit.SetActive(true);
        stsAccBtnTxtVerifyEmail.GetComponent<Text>().text = lt.getDisplayValue(lt.resendOTP);
        stsAccBtnVerifyEmail.GetComponent<Button>().interactable = false;

        // able resend otp after 60 second
        StartCoroutine(Timer(stsAccBtnTxtVerifyEmail, lt.getDisplayValue(lt.resendOTP), 60, "", null, stsAccBtnVerifyEmail.GetComponent<Button>()));
    }

    public void checkupdateEmail2FA()
    {
        // check otp correct 
        if (stsAccVerifyTxtPin.GetComponent<InputField>().text == accCEOTP && wrongAccCEOTP < 5 && DateTime.Now < accCEOTPExpiry)
        {
            // show verified
            stsAccVerifiedTxt.SetActive(true);
            stsAccVerifiedTxt.GetComponent<Text>().text = lt.getDisplayValue(lt.verified);
            stsAccBtnVerifyEmail.SetActive(false);
            stsAccVerifyMsg.SetActive(false);
            stsAccVerifyTxtPin.SetActive(false);
            stsAccBtnPinSubmit.SetActive(false);
            newTempEmail = stsAccTxtEmail.GetComponent<InputField>().text;

            // clear filled otp
            stsAccVerifyTxtPin.GetComponent<InputField>().text = string.Empty;

            // verified password correct otp log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Update Email", ("Update Email - Email Verified.")));

        }
        // wrong otp, with wrong less than 5 time, and otp not expiry
        else if (stsAccVerifyTxtPin.GetComponent<InputField>().text != accCEOTP && wrongAccCEOTP < 5 && DateTime.Now < accCEOTPExpiry)
        {
            // Add number of wrong
            wrongAccCEOTP++;
            stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);

            // clear filled otp
            stsAccVerifyTxtPin.GetComponent<InputField>().text = string.Empty;

            // wrong otp log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Update Email", ("Update Email - Wrong OTP inputed.")));


        }
        // wrong otp more than or equal to 5 time 
        else if (wrongAccCEOTP >= 5)
        {
            // show wrong otp msg
            stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);

            // clear filled otp
            stsAccVerifyTxtPin.GetComponent<InputField>().text = string.Empty;

            // wrong otp excess limit log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Update Email", ("Update Email - Wrong OTP inputed excess limit.")));

        }
        // OTP Expiry
        else if (DateTime.Now < accCEOTPExpiry)
        {
            stsAccVerifyMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);

            // clear filled otp
            stsAccVerifyTxtPin.GetComponent<InputField>().text = string.Empty;

            // otp expiry log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Update Email", ("Update Email - OTP expiry.")));

        }
    }

    public void valueChanged() { settingsEdited = true; }

    private IEnumerator Timer(GameObject displayText, string prefixString, float time, string postfixString, GameObject closeObject, Button interactiveTrue)
    {
        displayText.GetComponent<Text>().text = prefixString + " (" + time.ToString("F0") + " s)" + postfixString; // Update the UI text

        while (time > 0)
        {
            yield return new WaitForSeconds(1); // Wait for one second
            time--; // Decrement the timer
            displayText.GetComponent<Text>().text = prefixString + " (" + time.ToString("F0") + " s)" + postfixString; // Update the UI text
        }

        if (time == 0 && closeObject != null) { closeObject.SetActive(false); }

        if (time == 0 && interactiveTrue != null) { interactiveTrue.interactable = true; }
    }

    public async void stsPwd()
    {
        if (settingsEdited)
        {
            await checkdataChanged();
            if (dataChanged) stsAccSaveConfirm();
        }

        if (!dataChanged)
        {
            // Clear Entry
            stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
            stsPwdWarningMsg.SetActive(false);


            // Show Password settings page
            canvasStsAccount.SetActive(false);
            canvasStsPassword.SetActive(true);
            canvasStsGeneral.SetActive(false);

            // Set layout of change password
            stsPwdlblOldPwd.GetComponent<Text>().text = lt.getDisplayValue(lt.oldpwd);
            stsPwdlblNewPwd.GetComponent<Text>().text = lt.getDisplayValue(lt.newPw);
            stsPwdlblRePwd.GetComponent<Text>().text = lt.getDisplayValue(lt.reNewPw);
            stsPwdBtnTxtClr.GetComponent<Text>().text = lt.getDisplayValue(lt.clear);
            stsPwdBtnTxtSave.GetComponent<Text>().text = lt.getDisplayValue(lt.save);
            changePwdHint();
        }
    }

    public void stsPwdClr()
    {
        // Clear Entry
        stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
        stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
        stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
        stsPwdWarningMsg.SetActive(false);

    }

    public void changePwdHint()
    {
        // set Regualar Expression
        Regex rgLength = new Regex(@"^.{8,}$");
        Regex rgUpper = new Regex(@"[A-Z]");
        Regex rgLower = new Regex(@"[a-z]");
        Regex rgDigit = new Regex(@"(?=.*\d)");
        Regex rgSymbol = new Regex(@"[!@#$%^&*()_++<>.~-]");

        // Check if text matches regular expression
        if (rgLength.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
        {
            stsPwdHint1.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAtleast8);
            stsPwdHint1.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            stsPwdHint1.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAtleast8);
            stsPwdHint1.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgUpper.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
        {
            stsPwdHint2.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Upper);
            stsPwdHint2.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            stsPwdHint2.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Upper);
            stsPwdHint2.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgLower.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
        {
            stsPwdHint3.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Lower);
            stsPwdHint3.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            stsPwdHint3.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Lower);
            stsPwdHint3.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgDigit.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
        {
            stsPwdHint4.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Digit);
            stsPwdHint4.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            stsPwdHint4.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Digit);
            stsPwdHint4.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
        if (rgSymbol.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
        {
            stsPwdHint5.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Symbol);
            stsPwdHint5.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            stsPwdHint5.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Symbol);
            stsPwdHint5.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
    }

    public async void changePassword()
    {
        string checkPwdSQL = "select username, password from useraccount where username = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(checkPwdSQL);
        stsPwdWarningMsg.SetActive(true);
        // check if new password matches requirements
        Regex rgPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_++<>.~-]).{8,}$");
        if (!rgPassword.IsMatch(stsPwdTxtNewPwd.GetComponent<InputField>().text))
            stsPwdWarningMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNotRequirement);

        // check if new password matches re-enter password
        else if (stsPwdTxtNewPwd.GetComponent<InputField>().text != stsPwdTxtRePwd.GetComponent<InputField>().text)
            stsPwdWarningMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNewRenotMatch);

        // check if old password correct
        else if (databaseManager.GetComponent<DatabaseManager>().hashPassword(stsPwdTxtOldPwd.GetComponent<InputField>().text) !=
            databaseManager.GetComponent<DatabaseManager>().getData(0, 1))
            stsPwdWarningMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.pwOldWrong);

        // check if old password equals new password
        else if (stsPwdTxtOldPwd.GetComponent<InputField>().text == stsPwdTxtNewPwd.GetComponent<InputField>().text)
            stsPwdWarningMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNewSameOldPw);

        // Update password
        else
        {
            stsPwdWarningMsg.SetActive(false);
            string UpdatePwdSQL = "UPDATE `useraccount` SET `password` = '" + databaseManager.GetComponent<DatabaseManager>().hashPassword(stsPwdTxtNewPwd.GetComponent<InputField>().text) + "' where `username` = '" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "';";
            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(UpdatePwdSQL);

            // Clear Entry
            stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
        }
    }

    public async void stsGeneral()
    {
        if (settingsEdited)
        {
            await checkdataChanged();
            if (dataChanged) stsAccSaveConfirm();
        }

        if (!dataChanged)
        {
            // Clear Password Entry
            stsPwdTxtOldPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtNewPwd.GetComponent<InputField>().text = string.Empty;
            stsPwdTxtRePwd.GetComponent<InputField>().text = string.Empty;
            stsPwdWarningMsg.SetActive(false);

            // Show General Settings page
            canvasStsAccount.SetActive(false);
            canvasStsPassword.SetActive(false);
            canvasStsGeneral.SetActive(true);
            try { stsGESLSEVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat(lt.keyVolumeSE); }
            catch (NullReferenceException)
            {
                PlayerPrefs.SetFloat(lt.keyVolumeSE, 0.5f);
                stsGESLSEVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat(lt.keyVolumeSE);
            }

            try { stsGESLBGMVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat(lt.keyVolumeBGM); }
            catch (NullReferenceException)
            {
                PlayerPrefs.SetFloat(lt.keyVolumeBGM, 0.5f);
                stsGESLBGMVolume.GetComponent<Slider>().value = PlayerPrefs.GetFloat(lt.keyVolumeBGM);
            }

            stsGElblSEVolume.GetComponent<Text>().text = lt.getDisplayValue(lt.se);
            stsGElblBGMVolume.GetComponent<Text>().text = lt.getDisplayValue(lt.bgm);

        }
    }

    public void soundEffectVolume()
    {
        // set and show volume 
        stsGESLSEVolume.GetComponent<AudioSource>().volume = stsGESLSEVolume.GetComponent<Slider>().value;
        stsGElblSETxtVolume.GetComponent<Text>().text = ((int)(stsGESLSEVolume.GetComponent<Slider>().value * 100)).ToString();
        stsGESLBGMVolume.GetComponent<AudioSource>().volume = stsGESLBGMVolume.GetComponent<Slider>().value;
        stsGElblBGMTxtVolume.GetComponent<Text>().text = ((int)(stsGESLBGMVolume.GetComponent<Slider>().value * 100)).ToString();
    }

    public void stsGenSaveVolume()
    {
        PlayerPrefs.SetFloat(lt.keyVolumeSE, stsGESLSEVolume.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat(lt.keyVolumeBGM, stsGESLBGMVolume.GetComponent<Slider>().value);
    }
}