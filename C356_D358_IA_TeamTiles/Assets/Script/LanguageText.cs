﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageText : MonoBehaviour
{
    // All display language can be adjusted here.
    public readonly string[] languageAvaliable = { "English", "中文(繁體)", "中文(简体)" };
    public readonly string[] signin = { "Sign In", "登入", "登入" };
    public readonly string[] signUp = { "Sign Up", "註冊", "注册" };
    public readonly string[] language = { "Language: ", "語言：", "语言：" };
    public readonly string[] usernameEmail = { "Username/Email: ", "用戶名稱/電郵", "用户名称/邮箱：" };
    public readonly string[] password = { "Password: ", "密碼：", "密码：" };
    public readonly string[] wrongLoginPw = { "Username / password invalid. Please try again.", "用戶名稱或密碼不正確，請重新輸入。", "用户名称或密码不正确，请重新输入。" };
    public readonly string[] forgotPwd = { "Forgot Password", "忘記密碼", "忘记密码" };
    public readonly string[] back = { "Back", "返回", "返回" };
    public readonly string[] forpwdcontent = { "Please enter your username / email address.", "請輸入你的用戶名稱或電郵。", "请输入你的用户名称或电邮。" };
    public readonly string[] submit = { "Submit", "提交", "提交" };
    public readonly string[] fpUsernameNotFound = { "Username / email not found. \nPlease check again or sign up. ", "找不到用戶名稱 / 電郵。\n請檢查是否輸入正確或註冊", "找不到用户名称 / 邮箱。\n请检查是否输入正确或注册。" };
    public readonly string[] otpSent = { "An One-time password has been sent to your mailbox.", "一次性驗證碼已傳送到您的電郵。", "一次性验证码已传送到您的邮箱。" };
    public readonly string[] ok = { "OK", "確認", "确认" };
    public readonly string[] wrongOTP = { "Invalid One-Time Password\nPlease input again.", "一次性驗證碼錯誤，\n請重新輸入。", "一次性验证码错误，\n请重新输入。" };
    public readonly string[] resendOTP = { "Resend One-Time Password", "重發一次性驗證碼", "重发一次性验证码" };
    public readonly string[] wrong5OTP = { "Invalid One-Time Password more than 5 times.\nPlease request a new One-Time Password.", "一次性驗證碼輸入錯誤超過五次，\n請重新獲取驗證碼。", "一次性验证码错误超过五次，\n请重新获取验证码。" };
    public readonly string[] otpExpiry = { "One-Time Password Expiried.\nPlease request a new One-Time Password.", "一次性驗證碼已過期，\n", "一次性验证码已过期，\n请重新输入。" };
    public readonly string[] cpMsg = { "Please reset your password. ", "請重設你的密碼。", "请重设你的密码" };
    public readonly string[] newPw = { "New Password: ", "新密碼", "新密码" };
    public readonly string[] reNewPw = { "Re-enter New Password: ", "重新輸入新密碼", "重新输入新密码" };
    public readonly string[] pwAtleast8 = { "Pasword must contains at least 8 characters. ", "密碼長度不得少於8位", "密码长度不得少于8位" };
    public readonly string[] pwAt1Upper = { "Pasword must contains at least 1 UPPER characters. ", "密碼需最少包含1位大寫字母", "密码需最少包含1位大写字母" };
    public readonly string[] pwAt1Lower = { "Pasword must contains at least 1 lower characters. ", "密碼需最少包含1位小寫字母", "密码需最少包含1位小写字母" };
    public readonly string[] pwAt1Digit = { "Pasword must contains at least 1 number. ", "密碼需最少包含1個數字", "密码需最少包含1个数字" };
    public readonly string[] pwAt1Symbol = { "Pasword must contains at least 1 special characters. ", "密碼需最少包含1個特殊字母", "密码需最少包含1个特殊字母" };
    public readonly string[] pwNewRenotMatch = { "New password and Re-enter password did not matched. ", "新密碼與重複輸入新密碼不符", "新密码与重复输入新密码不符" };
    public readonly string[] pwNotRequirement = { "Password must be at least 8 characters long, contains a lower case letter, a upper case letter, a number, and a special character", "密碼長度必須至少8個字符，包含一個小寫字母、一個大寫字母、一個數字和一個特殊字符", "密码长度必须至少为 8 个字符，包含一个小写字母、一个大写字母、一个数字和一个特殊字符" };
    public readonly string[] pwNewSameOldPw = { "New Password should not be same with old password. ", "新密碼不得與舊密碼相同。", "新密码不得与旧密码相同。" };
    public readonly string[] accountLocked = { "Your account has been locked. Please use forget password to reset your account.", "你的帳戶已被鎖定，請以「忘記密碼」功能重啟帳戶。", "你的帐户已被锁定，请以「忘记密码」功能重启帐户。" };
    public readonly string[] username = { "Username ", "用戶名稱 ", "用户名称 " };
    public readonly string[] email = { "Email ", "電郵 ", "邮箱 " };
    public readonly string[] used = { " already been used. ", " 已被使用 ", " 已被使用 " };
    public readonly string[] nullUsername = { "Username should not be null.", "用戶名稱不得為空。", "用户名称不得为空。" };
    public readonly string[] nullPassword = { "Password should not be null.", "密碼不得為空。", "密码不得为空。" };
    public readonly string[] nullRePassword = { "Re-enter password should not be null.", "重新輸入密碼不得為空。", "重新输入密码不得为空。" };
    public readonly string[] nullEmail = { "Email should not be null.", "電郵不得為空。", "邮箱不得为空。" };
    public readonly string[] nullPlayerName = { "Player Name should not be null.", "玩家名稱不得為空。", "玩家昵称不得为空。" };
    public readonly string[] usernameLimit = { "Username must be at least 5 charater long.", "用戶名稱不得少於5位", "用户名称不得少于5位" };
    public readonly string[] emailformat = { "Invalid Email format", "電郵格式不正確", "邮箱格式不正确" };
    public readonly string[] accountCreated = { "Account Created.\nPlease Sign In and enjoy the Game","帳戶已建立，\n請登入並享受遊戲。", "帐户已建立，\n请登入并享受游戏。"};
    public readonly string[] playerName = { "Player Name","玩家名稱", "玩家昵称" };
    public readonly string[] _2fa = { "Enable 2FA", "雙重驗證", "两步验证" };
    public readonly string[] loading = { "Loading......", "載入中......", "载入中......" };

    private int languageIndex;
    // Start is called before the first frame update
    void Start()
    {
        languageIndex = PlayerPrefs.GetInt("languages");
    }

    // Update is called once per frame
    void Update()
    {

    }


    public string getDisplayValue(string[] text, int langIndex) { return text[langIndex]; }

    public string getDisplayValue(string[] text) { return text[languageIndex]; }

    public void setlanguageIndex(int languageIndex) { this.languageIndex = languageIndex; }

    public Color getTextColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }

    public string emailContentFP2FA(string playerName, string code2FA)
    {
        string emailContent = "";

        emailContent = "Hi " + playerName + ", <br><br>";
        emailContent += "Thank you for playing TeamTiles.<br><br>";
        emailContent += "Your one-time password for reset password is <b>" + code2FA + "</b>.<br><br>";
        emailContent += "The one-time password is only valid in for 5 minutes. Please do not share this code with anyone.<br><br>";
        emailContent += "Thank you and enjoy playing TeamTiles. <br><br>";
        emailContent += "Please disregard this email if you have not request for reset password in TeamTiles. <br><br>";
        emailContent += "Regards, <br>TeamTiles<br><br><br>";
        emailContent += "<a style=\"color:#AAA\">--Disclaimer--<br>";
        emailContent += "This email is intended for " + playerName + " and may contain confidential and/or privileged materials for specific purposes.";
        emailContent += "If you are not the addressee, you should not copy, forward, disclose or use any part of it. ";
        emailContent += "If you have received this email in error, please notify us by replying to this email and delete it from your system immediately. </a>";

        return emailContent;


    }

    public string emailContentsi2FA(string playerName, string code2FA)
    {
        string emailContent = "";

        emailContent = "Hi " + playerName + ", <br><br>";
        emailContent += "Thank you for playing TeamTiles.<br><br>";
        emailContent += "Your one-time password for Sign-In is <b>" + code2FA + "</b>.<br><br>";
        emailContent += "The one-time password is only valid in for 5 minutes. Please do not share this code with anyone.<br><br>";
        emailContent += "Thank you and enjoy playing TeamTiles. <br><br>";
        emailContent += "Please disregard this email if you have not Sign-In to TeamTiles. <br><br>";
        emailContent += "Regards, <br>TeamTiles<br><br><br>";
        emailContent += "<a style=\"color:#AAA\">--Disclaimer--<br>";
        emailContent += "This email is intended for " + playerName + " and may contain confidential and/or privileged materials for specific purposes.";
        emailContent += "If you are not the addressee, you should not copy, forward, disclose or use any part of it. ";
        emailContent += "If you have received this email in error, please notify us by replying to this email and delete it from your system immediately. </a>";

        return emailContent;


    }

    public string emailContentsu2FA(string playerName, string code2FA)
    {
        string emailContent = "";

        emailContent = "Hi " + playerName + ", <br><br>";
        emailContent += "Thank you for signing up account in TeamTiles.<br><br>";
        emailContent += "Your one-time password for creating account is <b>" + code2FA + "</b>.<br><br>";
        emailContent += "The one-time password is only valid in for 5 minutes. Please do not share this code with anyone.<br><br>";
        emailContent += "Thank you and enjoy playing TeamTiles. <br><br>";
        emailContent += "Please disregard this email if you have not signup in TeamTiles. <br><br>";
        emailContent += "Regards, <br>TeamTiles<br><br><br>";
        emailContent += "<a style=\"color:#AAA\">--Disclaimer--<br>";
        emailContent += "This email is intended for " + playerName+ " and may contain confidential and/or privileged materials for specific purposes.";
        emailContent += "If you are not the addressee, you should not copy, forward, disclose or use any part of it. ";
        emailContent += "If you have received this email in error, please notify us by replying to this email and delete it from your system immediately. </a>";

        return emailContent;


    }
}