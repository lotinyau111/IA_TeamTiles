using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    public GameObject databaseManager;

    public GameObject smBtnSignIn, smBtnSignUp;
    public GameObject canvasStartMenu, canvasSignIn, canvasSignUp, canvaForgotPwd;
    public GameObject smLanguage, smDDLanguage;
    public GameObject smBtnSignIntxt, smBtnSignUptxt;

    public GameObject siTitle, silblUserName, silblPwd;
    public GameObject sitxtUsername, sitxtPassword;
    public GameObject sibtntxtBack, sibtntxtSignIn;
    public GameObject siWarning, siforgotPwd;

    public GameObject fpTitle, fpContent, fptxt, fpbtn;
    public GameObject fpbtnEmailSubmittxt, fpbtn2FASubmittxt;
    public GameObject fpUsernotfoundBtnCfmtxt;
    public GameObject canvasfpUsernameNotFound, fplblUserNotFound;

    public GameObject canvassiLogining, siLoginingTxt;

    public GameObject canvasfp2FA, canvasfpWrong2FA, canvasfpChangePwd;
    public GameObject fp2FAmsg, fp2FAPin, fp2FAwWrongtxt, fp2faWrongBtn;
    public GameObject fpResendOTP;

    public GameObject cpMsg, cpNewPwdlbl, cpNewPwdtxt, cpReNewPwdlbl, cpReNewPwdtxt;
    public GameObject cpHint1, cpHint2, cpHint3, cpHint4, cpHint5;
    public GameObject canvascpWarning, cpWarninglbl, cpWarningOK;

    public GameObject canvasSiOTP, canvasWrongSIOTP;
    public GameObject si2FAmsg, si2FAWrongMsg, si2FAWrongBtn, siResendOTP;
    public GameObject sibtn2FASubmittxt, siOTPtxt;

    public GameObject sulblTitle, sulblUsername, sulblPassword, sulblRePw, sulblEmail, sulblPlayerName;
    public GameObject sulblLanguage, sulbl2FA, sulblBtnBack, sulblBtnSignUp;

    public GameObject suUsername, suPassword, suRePw, suEmail, suPlayerName, suLanguage, su2FA;
    public GameObject suWarning, suWarningTxt, suWarningOK;
    public GameObject suAccountCreateCanvas, accountCreateTxt;

    public GameObject canvasSUOTP, canvasWrongSUOTP;
    public GameObject su2FAmsg, su2FAWrongMsg, su2FAWrongBtn, suResendOTP;
    public GameObject subtn2FASubmittxt, suOTPtxt;
    public GameObject suPwHint1, suPwHint2, suPwHint3, suPwHint4, suPwHint5;


    string fpOTP, siOTP, suOTP;
    DateTime fpOTPExpiry, siOTPExpiry, suOTPExpiry;
    int wrongFpOTP, wrongSiOTP, wrongSuOTP;
    LanguageText lt = new();
    Mailing mail = new();



    // Start is called before the first frame update
    void Start()
    {
        onloadStartMenu();
        canvasStartMenu.SetActive(true);
        canvasSignIn.SetActive(false);
        canvasSignUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Action done when StartMenu Opens.
    public void onloadStartMenu()
    {
        // clear saved login details.
        PlayerPrefs.DeleteKey(lt.keyCurrentPlayer);
        PlayerPrefs.SetInt("languages", 0);


        // Assign language list (i.e., Lanaguge Package stored in Language Text
        List<string> langChoice = new List<string> { };
        for (int i = 0; i < lt.languageAvaliable.Length; i++)
            langChoice.Add(lt.languageAvaliable[i]);

        smDDLanguage.GetComponent<Dropdown>().ClearOptions();
        smDDLanguage.GetComponent<Dropdown>().AddOptions(langChoice);
        smDDLanguage.GetComponent<Dropdown>().value = 0;
        setStartMenuLanguage();
    }

    public void setLanguage()
    {
        // change language when language selection changes.
        PlayerPrefs.SetInt("languages", smDDLanguage.GetComponent<Dropdown>().value);
        lt.setlanguageIndex(smDDLanguage.GetComponent<Dropdown>().value);
        setStartMenuLanguage();
    }

    public void setStartMenuLanguage()
    {
        // Apply language set.
        smBtnSignIntxt.GetComponent<Text>().text = lt.getDisplayValue(lt.signin);
        smBtnSignUptxt.GetComponent<Text>().text = lt.getDisplayValue(lt.signUp);
        smLanguage.GetComponent<Text>().text = lt.getDisplayValue(lt.language);
    }

    public void setSignInLanguage()
    {
        // Apply language set. 
        siTitle.GetComponent<Text>().text = lt.getDisplayValue(lt.signin);
        silblUserName.GetComponent<Text>().text = lt.getDisplayValue(lt.usernameEmail);
        silblPwd.GetComponent<Text>().text = lt.getDisplayValue(lt.password);
        siforgotPwd.GetComponent<Text>().text = lt.getDisplayValue(lt.forgotPwd);
        sibtntxtBack.GetComponent<Text>().text = lt.getDisplayValue(lt.back);
        sibtntxtSignIn.GetComponent<Text>().text = lt.getDisplayValue(lt.signin);
    }

    public void setSignUpLanguage()
    {
        sulblUsername.GetComponent<Text>().text = lt.getDisplayValue(lt.username);
        sulblEmail.GetComponent<Text>().text = lt.getDisplayValue(lt.email);
        sulblPassword.GetComponent<Text>().text = lt.getDisplayValue(lt.password);
        sulblRePw.GetComponent<Text>().text = lt.getDisplayValue(lt.reNewPw);
        sulblPlayerName.GetComponent<Text>().text = lt.getDisplayValue(lt.playerName);
        sulblLanguage.GetComponent<Text>().text = lt.getDisplayValue(lt.language);
        sulbl2FA.GetComponent<Text>().text = lt.getDisplayValue(lt._2fa);
        sulblBtnBack.GetComponent<Text>().text = lt.getDisplayValue(lt.back);
        sulblBtnSignUp.GetComponent<Text>().text = lt.getDisplayValue(lt.signUp);
    }

    public void startMenuSignIn()
    {
        // Button Action for when user press SignIn btn at Start Menu.
        canvasStartMenu.SetActive(false);
        canvasSignIn.SetActive(true);
        setSignInLanguage();
    }

    public void startMenuSignUp()
    {
        // Button Action for when user press SignUp btn at Start Menu.
        canvasStartMenu.SetActive(false);
        canvasSignUp.SetActive(true);
        setSignUpLanguage();
        clearSignUpData();
    }

    public void forgotPassword()
    {
        // Active Forgot Password Canvas and set text value.
        canvaForgotPwd.SetActive(true);
        fpTitle.GetComponent<Text>().text = lt.getDisplayValue(lt.forgotPwd);
        fpContent.GetComponent<Text>().text = lt.getDisplayValue(lt.forpwdcontent);
        fpbtnEmailSubmittxt.GetComponent<Text>().text = lt.getDisplayValue(lt.submit);
    }

    public async void searchForgotPassword()
    {

        // search user data from database. 
        string userinput = fptxt.GetComponent<InputField>().text;
        string sql = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("username, email, playerName", "useraccount", (" username = \"" + userinput + "\" or email = \"" + userinput + "\""));
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sql);
        string result = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);
        if (result.Trim() == "")
        {
            // add forgot password log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Forgot Password", ("Forgot password - Fail: " + userinput + " not found")));

            // show user not found
            canvasfpUsernameNotFound.SetActive(true);

            StartCoroutine(Timer(fpUsernotfoundBtnCfmtxt, lt.getDisplayValue(lt.ok), 5, "", canvasfpUsernameNotFound, null));

            // clear filled username or email address 
            fptxt.GetComponent<InputField>().text = string.Empty;
        }
        else
        {
            // save current player details
            PlayerPrefs.SetString("currentPlayer", result);

            // forgot password log - user found 
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Forgot Password", ("Forgot password - Success: " + userinput + " found")));

            // show 2fa diaglog
            fp2FA();

            // clear filled details
            fptxt.GetComponent<InputField>().text = string.Empty;
        }
    }

    public void fp2FA()
    {

        string emailAddress = databaseManager.GetComponent<DatabaseManager>().getData(0, 1);
        string receipientName = databaseManager.GetComponent<DatabaseManager>().getData(0, 2);

        // generate OTP and convert to string
        int otp = UnityEngine.Random.Range(0, 999999);
        fpOTP = otp.ToString("000000");

        // reset wrong number
        wrongFpOTP = 0;

        // set layout text 
        fpbtn2FASubmittxt.GetComponent<Text>().text = lt.getDisplayValue(lt.submit);

        // send email and display input box
        mail.SendEmail(emailAddress, receipientName, "Forgot Password", lt.emailContentFP2FA(receipientName, fpOTP));
        fpOTPExpiry = DateTime.Now.AddMinutes(5);
        canvaForgotPwd.SetActive(false);
        canvasfp2FA.SetActive(true);
        fp2FAmsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpSent);
        fpResendOTP.GetComponent<Button>().interactable = false;

        // able resend otp after 60 second
        StartCoroutine(Timer(fpResendOTP, lt.getDisplayValue(lt.resendOTP), 60, "", null, fpResendOTP.GetComponent<Button>()));

        // forgot password log - otp send success  
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 0), "Forgot Password", ("Forgot password OTP Send - Success user: " + databaseManager.GetComponent<DatabaseManager>().getData(0, 0) + " found")));

    }

    public void checkfp2FA()
    {
        // check otp correct 
        if (fp2FAPin.GetComponent<InputField>().text == fpOTP && wrongFpOTP < 5 && DateTime.Now < fpOTPExpiry)
        {
            // show change password page if otp is correct
            canvasfpChangePwd.SetActive(true);
            cpNewPwdlbl.GetComponent<Text>().text = lt.getDisplayValue(lt.newPw);
            cpReNewPwdlbl.GetComponent<Text>().text = lt.getDisplayValue(lt.reNewPw);
            cpMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.cpMsg);

            // clear filled otp
            fp2FAPin.GetComponent<InputField>().text = string.Empty;

            // forgot password correct otp log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 0), "Forgot Password", ("Forgot password - Correct OTP inputted.")));

        }
        // wrong otp, with wrong less than 5 time, and otp not expiry
        else if (fp2FAPin.GetComponent<InputField>().text != fpOTP && wrongFpOTP < 5 && DateTime.Now < fpOTPExpiry)
        {
            // Add number of wrong
            wrongFpOTP++;
            canvasfpWrong2FA.SetActive(true);
            fp2FAwWrongtxt.GetComponent<Text>().text = lt.getDisplayValue(lt.wrong5OTP);
            StartCoroutine(Timer(fp2faWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasfpWrong2FA, null));

            // clear filled otp
            fp2FAPin.GetComponent<InputField>().text = string.Empty;

            // forgot password wrong otp log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 0), "Forgot Password", ("Forgot password - Wrong OTP inputed.")));


        }
        // wrong otp more than or equal to 5 time 
        else if (wrongFpOTP >= 5)
        {
            canvasfpWrong2FA.SetActive(true);
            fp2FAwWrongtxt.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);
            StartCoroutine(Timer(fp2faWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasfpWrong2FA, fpResendOTP.GetComponent<Button>()));

            // clear filled otp
            fp2FAPin.GetComponent<InputField>().text = string.Empty;

            // forgot password wrong otp excess limit log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 0), "Forgot Password", ("Forgot password - Wrong OTP inputed excess limit.")));

        }
        // OTP Expiry
        else if (DateTime.Now < fpOTPExpiry)
        {
            canvasfpWrong2FA.SetActive(true);
            fp2FAwWrongtxt.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);
            StartCoroutine(Timer(fp2faWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasfpWrong2FA, fpResendOTP.GetComponent<Button>()));

            // clear filled otp
            fp2FAPin.GetComponent<InputField>().text = string.Empty;

            // forgot password otp expiry log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 0), "Forgot Password", ("Forgot password - OTP expiry.")));

        }
    }

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

    public void changePwdHint()
    {
        // set Regualar Expression
        Regex rgLength = new Regex(@"^.{8,}$");
        Regex rgUpper = new Regex(@"[A-Z]");
        Regex rgLower = new Regex(@"[a-z]");
        Regex rgDigit = new Regex(@"(?=.*\d)");
        Regex rgSymbol = new Regex(@"[!@#$%^&*()_++<>.~-]");

        // Check if text matches regular expression
        if (rgLength.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            cpHint1.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAtleast8);
            cpHint1.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            cpHint1.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAtleast8);
            cpHint1.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgUpper.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            cpHint2.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Upper);
            cpHint2.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            cpHint2.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Upper);
            cpHint2.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgLower.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            cpHint3.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Lower);
            cpHint3.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            cpHint3.GetComponent<Text>().text = "×  " + lt.getDisplayValue(lt.pwAt1Lower);
            cpHint3.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgDigit.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            cpHint4.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Digit);
            cpHint4.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            cpHint4.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Digit);
            cpHint4.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
        if (rgSymbol.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            cpHint5.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Symbol);
            cpHint5.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            cpHint5.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Symbol);
            cpHint5.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
    }

    public async void ChangePassword()
    {
        Regex rgPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_++<>.~-]).{8,}$");
        string findPwSql = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("password", "useraccount", "username = \"" + PlayerPrefs.GetString("currentPlayer") + "\"");
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(findPwSql);
        string oldPw = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);
        // check new password equals to re-enter password
        if (cpNewPwdtxt.GetComponent<InputField>().text != cpReNewPwdtxt.GetComponent<InputField>().text)
        {
            canvascpWarning.SetActive(true);
            cpWarninglbl.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNewRenotMatch);
            StartCoroutine(Timer(cpWarningOK, lt.getDisplayValue(lt.ok), 5, "", canvascpWarning, null));
        }
        // check password matched requirements
        else if (!rgPassword.IsMatch(cpNewPwdtxt.GetComponent<InputField>().text))
        {
            canvascpWarning.SetActive(true);
            cpWarninglbl.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNotRequirement);
            StartCoroutine(Timer(cpWarningOK, lt.getDisplayValue(lt.ok), 5, "", canvascpWarning, null));
        }
        // check if new password equals to old password.
        else if (databaseManager.GetComponent<DatabaseManager>().hashPassword(cpNewPwdtxt.GetComponent<InputField>().text) == oldPw)
        {
            canvascpWarning.SetActive(true);
            cpWarninglbl.GetComponent<Text>().text = lt.getDisplayValue(lt.pwNewSameOldPw);
            StartCoroutine(Timer(cpWarningOK, lt.getDisplayValue(lt.ok), 5, "", canvascpWarning, null));
        }
        // update password 
        else
        {
            // update password from database 
            string updateSql = "UPDATE `useraccount` SET `password` = \"" + databaseManager.GetComponent<DatabaseManager>().hashPassword(cpNewPwdtxt.GetComponent<InputField>().text) + "\" where `username` = \"" + PlayerPrefs.GetString(lt.keyCurrentPlayer) + "\"";
            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(updateSql);

            // clear temp save data 
            cpNewPwdtxt.GetComponent<InputField>().text = string.Empty;
            cpReNewPwdtxt.GetComponent<InputField>().text = string.Empty;
            PlayerPrefs.DeleteKey(lt.keyCurrentPlayer);
            canvasfpChangePwd.SetActive(false);

            // Reset password log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString(lt.keyCurrentPlayer), "Reset Password", ("Reset password - Password Changed.")));

        }
    }

    public async void siSignIn()
    {
        canvassiLogining.SetActive(true);
        siLoginingTxt.GetComponent<Text>().text = lt.getDisplayValue(lt.loading);
        // check if login username / email + password correct 
        string loginSql = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("COUNT(id), username, email, 2FA, locked, lang", "useraccount",
            ("(username = \"" + sitxtUsername.GetComponent<InputField>().text + "\" or email = \"" + sitxtUsername.GetComponent<InputField>().text + "\") and password = \"" +
            databaseManager.GetComponent<DatabaseManager>().hashPassword(sitxtPassword.GetComponent<InputField>().text) + "\""));
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(loginSql);

        string result = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);

        // wrong login
        if (result == "0")
        {
            // Login Wrong username / password log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(sitxtUsername.GetComponent<InputField>().text, "Login", ("Login - Wrong username / password. ")));

            siWarning.SetActive(true);
            canvassiLogining.SetActive(false);
            siWarning.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongLoginPw);
            sitxtPassword.GetComponent<InputField>().text = string.Empty;
        }
        // correct login
        else if (result == "1")
        {
            // save current player data 
            PlayerPrefs.SetString("currentPlayer", databaseManager.GetComponent<DatabaseManager>().getData(0, 1));
            
            // check if account is locked
            if (databaseManager.GetComponent<DatabaseManager>().getData(0, 4) == "1")
            {
                siWarning.SetActive(true);
                canvassiLogining.SetActive(false);
                siWarning.GetComponent<Text>().text = lt.getDisplayValue(lt.accountLocked);
                StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(databaseManager.GetComponent<DatabaseManager>().getData(0, 1), "Login Failed", "Login Failed - Account Locked."));
                sitxtPassword.GetComponent<InputField>().text = string.Empty;
            }
            // check if account needed login 2fa
            else if (databaseManager.GetComponent<DatabaseManager>().getData(0, 3) == "1")
            {
                siWarning.SetActive(false);
                // send 2fa
                sendLogin2FA();
            }
            else
            {
                siWarning.SetActive(false);
                // Sign In and Change scence
                siSignInSuccess();
            }
        }
    }

    public void sendLogin2FA()
    {
        string emailAddress = databaseManager.GetComponent<DatabaseManager>().getData(0, 2);
        string receipentName = databaseManager.GetComponent<DatabaseManager>().getData(0, 1);

        // generate OTP and convert to string
        int otp = UnityEngine.Random.Range(0, 999999);
        siOTP = otp.ToString("000000");

        // reset wrong sign in OTP number
        wrongSiOTP = 0;

        // set layout text
        sibtn2FASubmittxt.GetComponent<Text>().text = lt.getDisplayValue(lt.submit);

        // send email, set OTP Expiry and display input box
        mail.SendEmail(emailAddress, receipentName, "Login", lt.emailContentsi2FA(receipentName, siOTP));
        siOTPExpiry = DateTime.Now.AddMinutes(5);
        canvasSiOTP.SetActive(true);
        canvassiLogining.SetActive(false);

        si2FAmsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpSent);
        siResendOTP.GetComponent<Button>().interactable = false;

        // Add Log of sending OTP
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(receipentName, "Login OTP", "Login OTP - OTP Sent"));

        // Able resend OTP After 60 seconds
        StartCoroutine(Timer(siResendOTP, lt.getDisplayValue(lt.resendOTP), 60, "", null, siResendOTP.GetComponent<Button>()));

    }

    public void checkSIOTP()
    {
        // check if OTP is correct
        if (siOTPtxt.GetComponent<InputField>().text == siOTP && wrongSiOTP < 5 && DateTime.Now < siOTPExpiry)
        {
            Debug.Log("OTP Correct");
            siOTPtxt.GetComponent<InputField>().text = string.Empty;
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString("currentPlayer"), "Login", "Login - OTP Correct. "));
            siSignInSuccess();
        }
        // wrong OTP, with less than 5 time, OTP is not expiry 
        else if (siOTPtxt.GetComponent<InputField>().text != siOTP && wrongSiOTP < 5 && DateTime.Now < siOTPExpiry)
        {
            // Add number of wrong OTP
            wrongSiOTP++;

            // Show wrong OTP
            canvasWrongSIOTP.SetActive(true);
            si2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);
            StartCoroutine(Timer(si2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSIOTP, null));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString("currentPlayer"), "Login", "Login - OTP Invalid. "));

            // clear filled OTP
            siOTPtxt.GetComponent<InputField>().text = string.Empty;
        }
        // wrong OTP more than or equal to 5 times
        else if (wrongSiOTP >= 5)
        {
            // Show wrong OTP
            canvasWrongSIOTP.SetActive(true);
            si2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrong5OTP);
            StartCoroutine(Timer(si2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSIOTP, siResendOTP.GetComponent<Button>()));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString("currentPlayer"), "Login", "Login - OTP invalid excessed limitation. "));

            // clear filled OTP
            siOTPtxt.GetComponent<InputField>().text = string.Empty;
        }
        // OTP Expiry
        else if (DateTime.Now < siOTPExpiry)
        {
            // Show wrong OTP
            canvasWrongSIOTP.SetActive(true);
            si2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpExpiry);
            StartCoroutine(Timer(si2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSIOTP, siResendOTP.GetComponent<Button>()));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString("currentPlayer"), "Login", "Login - OTP expiry. "));

            // clear filled otp
            siOTPtxt.GetComponent<InputField>().text = string.Empty;
        }

    }

    public void siSignInSuccess()
    {

        // clear inputfield
        sitxtUsername.GetComponent<InputField>().text = string.Empty;
        sitxtPassword.GetComponent<InputField>().text = string.Empty;

        // Login Log
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(PlayerPrefs.GetString("currentPlayer"), "Login", "Login - Login Success. "));
        canvassiLogining.SetActive(false);
        int langIndex = int.Parse(databaseManager.GetComponent<DatabaseManager>().getData(0, 5));
        print(langIndex);
        PlayerPrefs.SetInt("languages", langIndex);
        // Change Next scene;
        GetComponent<ChangeScene>().nextScene();
    }

    public void siBack()
    {
        sitxtUsername.GetComponent<InputField>().text = string.Empty;
        sitxtPassword.GetComponent<InputField>().text = string.Empty;
        canvasSignIn.SetActive(false);
        canvasStartMenu.SetActive(true);
    }

    public async void suSignUp()
    {
        // show loading page while back-end runing
        suAccountCreateCanvas.SetActive(true);

        // Regualar Expression to check if inputs are corrects and valid
        Regex rgSUusername = new Regex(@"^[a-zA-Z0-9]([._]?[a-zA-Z0-9]){4,}$");
        Regex rgPassword = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_++<>.~-]).{8,}$");
        Regex rgEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,}$");

        // Warning messages after all checking
        string warningmsg = "";

        string username = suUsername.GetComponent<InputField>().text;
        string sqlCheckUsername = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("count(*)", "useraccount", "username = '" + username + "';");
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sqlCheckUsername);
        string response = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);
        if (username != "" && response == "0")
            warningmsg += lt.getDisplayValue(lt.username) + username + lt.getDisplayValue(lt.used) + "\n";

        string email = suEmail.GetComponent<InputField>().text;
        string sqlCheckEmail = databaseManager.GetComponent<DatabaseManager>().selectSQLtoString("count(*)", "useraccount", "email = '" + email + "';");
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sqlCheckEmail);
        response = databaseManager.GetComponent<DatabaseManager>().getData(0, 0);
        if (email != "" && response == "0")
            warningmsg += lt.getDisplayValue(lt.email) + email + lt.getDisplayValue(lt.used) + "\n";

        // Check if all field was filled and match requirement
        if (suUsername.GetComponent<InputField>().text == "") warningmsg += lt.getDisplayValue(lt.nullUsername);
        else if (!rgSUusername.IsMatch(suUsername.GetComponent<InputField>().text)) warningmsg += lt.getDisplayValue(lt.usernameLimit);
        if (suPassword.GetComponent<InputField>().text == "") warningmsg += lt.getDisplayValue(lt.nullPassword);
        else if (!rgPassword.IsMatch(suPassword.GetComponent<InputField>().text)) warningmsg += lt.getDisplayValue(lt.pwNotRequirement);
        if (suRePw.GetComponent<InputField>().text == "") warningmsg += lt.getDisplayValue(lt.nullPassword);
        else if (suPassword.GetComponent<InputField>().text != suRePw.GetComponent<InputField>().text) warningmsg += lt.getDisplayValue(lt.pwNewSameOldPw);
        if (suEmail.GetComponent<InputField>().text == "") warningmsg += lt.getDisplayValue(lt.nullEmail);
        else if (!rgEmail.IsMatch(suEmail.GetComponent<InputField>().text)) warningmsg += lt.getDisplayValue(lt.emailformat);
        if (suPlayerName.GetComponent<InputField>().text == "") warningmsg += lt.getDisplayValue(lt.nullPlayerName);

        // show warning message
        if (warningmsg != "")
        {
            suWarning.SetActive(true);
            suWarningTxt.GetComponent<Text>().text = warningmsg;
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Create Account", ("Create Account - Failed: " + warningmsg)));
            suWarningOK.GetComponent<Text>().text = lt.getDisplayValue(lt.ok);
        }
        // try signup
        else
        {
            string suSQL = "INSERT INTO `useraccount` (`username`, `password`, `email`, `playerName`, `lang`, `2FA`, `locked`) VALUES (";
            suSQL += "'" + suUsername.GetComponent<InputField>().text + "', ";
            suSQL += "'" + databaseManager.GetComponent<DatabaseManager>().hashPassword(suPassword.GetComponent<InputField>().text) + "', ";
            suSQL += "'" + suEmail.GetComponent<InputField>().text + "', ";
            suSQL += "'" + suPlayerName.GetComponent<InputField>().text + "', ";
            suSQL += suLanguage.GetComponent<Dropdown>().value + ", ";

            // set 2fa
            if (su2FA.GetComponent<Toggle>().isOn) suSQL += 1;
            else suSQL += 0;

            // set account locked
            suSQL += 1;
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(username, "Sign Up", "Sign Up - Account Created"));
            await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(suSQL);
            sendSU2FA();
        }
    }

    public void sendSU2FA()
    {
        string emailAddress = suEmail.GetComponent<InputField>().text;
        string receipentName = suUsername.GetComponent<InputField>().text;

        // generate OTP and convert to string
        int otp = UnityEngine.Random.Range(0, 999999);
        suOTP = otp.ToString("000000");

        // reset wrong sign up OTP number
        wrongSuOTP = 0;

        // set layout text
        subtn2FASubmittxt.GetComponent<Text>().text = lt.getDisplayValue(lt.submit);

        // send email, set OTP Expiry and display input box
        mail.SendEmail(emailAddress, receipentName, "Sign Up", lt.emailContentsu2FA(receipentName, suOTP));
        suOTPExpiry = DateTime.Now.AddMinutes(5);
        canvasSUOTP.SetActive(true);
        su2FAmsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpSent);
        suResendOTP.GetComponent<Button>().interactable = false;

        // Add Log of sending OTP
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(receipentName, "Sign Up OTP", "Sign Up OTP - OTP Sent"));

        // Able resend OTP After 60 seconds
        StartCoroutine(Timer(suResendOTP, lt.getDisplayValue(lt.resendOTP), 60, "", null, suResendOTP.GetComponent<Button>()));

    }


    public void checkSUOTP()
    {
        // check if OTP is correct
        if (suOTPtxt.GetComponent<InputField>().text == suOTP && wrongSuOTP < 5 && DateTime.Now < suOTPExpiry)
        {
            suOTPtxt.GetComponent<InputField>().text = string.Empty;
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(suUsername.GetComponent<InputField>().text, "Sign Up", "Sign Up - OTP Correct. "));
            accountCreate();
        }
        // wrong OTP, with less than 5 time, OTP is not expiry 
        else if (suOTPtxt.GetComponent<InputField>().text != suOTP && wrongSuOTP < 5 && DateTime.Now < suOTPExpiry)
        {
            // Add number of wrong OTP
            wrongSuOTP++;

            // Show wrong OTP
            canvasWrongSUOTP.SetActive(true);
            su2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrongOTP);
            StartCoroutine(Timer(su2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSUOTP, null));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Sign Up", "Sign Up - OTP Invalid. "));

            // clear filled OTP
            suOTPtxt.GetComponent<InputField>().text = string.Empty;
        }
        // wrong OTP more than or equal to 5 times
        else if (wrongSiOTP >= 5)
        {
            // Show wrong OTP
            canvasWrongSUOTP.SetActive(true);
            su2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.wrong5OTP);
            StartCoroutine(Timer(su2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSUOTP, suResendOTP.GetComponent<Button>()));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Sign Up", "Sign Up - OTP invalid excessed limitation. "));

            // clear filled OTP
            suOTPtxt.GetComponent<InputField>().text = string.Empty;
        }
        // OTP Expiry
        else if (DateTime.Now < suOTPExpiry)
        {
            // Show wrong OTP
            canvasWrongSIOTP.SetActive(true);
            su2FAWrongMsg.GetComponent<Text>().text = lt.getDisplayValue(lt.otpExpiry);
            StartCoroutine(Timer(su2FAWrongBtn, lt.getDisplayValue(lt.ok), 5, "", canvasWrongSUOTP, suResendOTP.GetComponent<Button>()));

            // Add Log
            StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog(null, "Sign Up", "Sign Up - OTP expiry. "));

            // clear filled otp
            suOTPtxt.GetComponent<InputField>().text = string.Empty;
        }
    }

    public async void accountCreate()
    {
        string sql = "UPDATE `useraccount` SET locked = 0 where username = " + suUsername.GetComponent<InputField>().text;
        await databaseManager.GetComponent<DatabaseManager>().receiveDBdata(sql);
        suAccountCreateCanvas.SetActive(true);
        //****

        canvasSUOTP.SetActive(false);
        clearSignUpData();
    }


    public void suBack()
    {
        canvasSignUp.SetActive(false);
        canvasStartMenu.SetActive(true);
    }

    public void clearSignUpData()
    {
        // clear all data inputed in Sign Up
        suUsername.GetComponent<InputField>().text = string.Empty;
        suPassword.GetComponent<InputField>().text = string.Empty;
        suRePw.GetComponent<InputField>().text = string.Empty;
        suPlayerName.GetComponent<InputField>().text = string.Empty;
        suEmail.GetComponent<InputField>().text = string.Empty;
        su2FA.GetComponent<Toggle>().isOn = false;

        List<string> languageList = new List<string> { };

        // Add language list 
        for (int i = 0; i < lt.languageAvaliable.Length; i++)
        {
            languageList.Add(lt.languageAvaliable[i]);
        }
        suLanguage.GetComponent<Dropdown>().ClearOptions();
        suLanguage.GetComponent<Dropdown>().AddOptions(languageList);
        suLanguage.GetComponent<Dropdown>().value = smDDLanguage.GetComponent<Dropdown>().value;
        StartCoroutine(databaseManager.GetComponent<DatabaseManager>().AddLog("System", "Sign Up", "All data cleared"));
    }


    public void suPasswordHint()
    {
        // set Regualar Expression
        Regex rgLength = new Regex(@"^.{8,}$");
        Regex rgUpper = new Regex(@"[A-Z]");
        Regex rgLower = new Regex(@"[a-z]");
        Regex rgDigit = new Regex(@"(?=.*\d)");
        Regex rgSymbol = new Regex(@"[!@#$%^&*()_++<>.~-]");

        // Check if text matches regular expression
        if (rgLength.IsMatch(suPassword.GetComponent<InputField>().text))
        {
            suPwHint1.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAtleast8);
            suPwHint1.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            suPwHint1.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAtleast8);
            suPwHint1.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgUpper.IsMatch(suPassword.GetComponent<InputField>().text))
        {
            suPwHint2.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Upper);
            suPwHint2.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            suPwHint2.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Upper);
            suPwHint2.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgLower.IsMatch(suPassword.GetComponent<InputField>().text))
        {
            suPwHint3.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Lower);
            suPwHint3.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            suPwHint3.GetComponent<Text>().text = "×  " + lt.getDisplayValue(lt.pwAt1Lower);
            suPwHint3.GetComponent<Text>().color = lt.getTextColor("#F00");
        }

        if (rgDigit.IsMatch(suPassword.GetComponent<InputField>().text))
        {
            suPwHint4.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Digit);
            suPwHint4.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            suPwHint4.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Digit);
            suPwHint4.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
        if (rgSymbol.IsMatch(suPassword.GetComponent<InputField>().text))
        {
            suPwHint5.GetComponent<Text>().text = "✓ - " + lt.getDisplayValue(lt.pwAt1Symbol);
            suPwHint5.GetComponent<Text>().color = lt.getTextColor("#0C0");
        }
        else
        {
            suPwHint5.GetComponent<Text>().text = "×  - " + lt.getDisplayValue(lt.pwAt1Symbol);
            suPwHint5.GetComponent<Text>().color = lt.getTextColor("#F00");
        }
    }
}
