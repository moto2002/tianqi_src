using LuaInterface;
using System;
using UnityEngine;

public class UnityEngine_KeyCodeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(KeyCode));
		L.RegVar("None", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_None), null);
		L.RegVar("Backspace", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Backspace), null);
		L.RegVar("Delete", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Delete), null);
		L.RegVar("Tab", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Tab), null);
		L.RegVar("Clear", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Clear), null);
		L.RegVar("Return", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Return), null);
		L.RegVar("Pause", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Pause), null);
		L.RegVar("Escape", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Escape), null);
		L.RegVar("Space", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Space), null);
		L.RegVar("Keypad0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad0), null);
		L.RegVar("Keypad1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad1), null);
		L.RegVar("Keypad2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad2), null);
		L.RegVar("Keypad3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad3), null);
		L.RegVar("Keypad4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad4), null);
		L.RegVar("Keypad5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad5), null);
		L.RegVar("Keypad6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad6), null);
		L.RegVar("Keypad7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad7), null);
		L.RegVar("Keypad8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad8), null);
		L.RegVar("Keypad9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Keypad9), null);
		L.RegVar("KeypadPeriod", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadPeriod), null);
		L.RegVar("KeypadDivide", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadDivide), null);
		L.RegVar("KeypadMultiply", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadMultiply), null);
		L.RegVar("KeypadMinus", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadMinus), null);
		L.RegVar("KeypadPlus", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadPlus), null);
		L.RegVar("KeypadEnter", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadEnter), null);
		L.RegVar("KeypadEquals", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_KeypadEquals), null);
		L.RegVar("UpArrow", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_UpArrow), null);
		L.RegVar("DownArrow", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_DownArrow), null);
		L.RegVar("RightArrow", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightArrow), null);
		L.RegVar("LeftArrow", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftArrow), null);
		L.RegVar("Insert", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Insert), null);
		L.RegVar("Home", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Home), null);
		L.RegVar("End", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_End), null);
		L.RegVar("PageUp", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_PageUp), null);
		L.RegVar("PageDown", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_PageDown), null);
		L.RegVar("F1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F1), null);
		L.RegVar("F2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F2), null);
		L.RegVar("F3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F3), null);
		L.RegVar("F4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F4), null);
		L.RegVar("F5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F5), null);
		L.RegVar("F6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F6), null);
		L.RegVar("F7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F7), null);
		L.RegVar("F8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F8), null);
		L.RegVar("F9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F9), null);
		L.RegVar("F10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F10), null);
		L.RegVar("F11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F11), null);
		L.RegVar("F12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F12), null);
		L.RegVar("F13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F13), null);
		L.RegVar("F14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F14), null);
		L.RegVar("F15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F15), null);
		L.RegVar("Alpha0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha0), null);
		L.RegVar("Alpha1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha1), null);
		L.RegVar("Alpha2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha2), null);
		L.RegVar("Alpha3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha3), null);
		L.RegVar("Alpha4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha4), null);
		L.RegVar("Alpha5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha5), null);
		L.RegVar("Alpha6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha6), null);
		L.RegVar("Alpha7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha7), null);
		L.RegVar("Alpha8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha8), null);
		L.RegVar("Alpha9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Alpha9), null);
		L.RegVar("Exclaim", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Exclaim), null);
		L.RegVar("DoubleQuote", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_DoubleQuote), null);
		L.RegVar("Hash", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Hash), null);
		L.RegVar("Dollar", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Dollar), null);
		L.RegVar("Ampersand", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Ampersand), null);
		L.RegVar("Quote", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Quote), null);
		L.RegVar("LeftParen", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftParen), null);
		L.RegVar("RightParen", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightParen), null);
		L.RegVar("Asterisk", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Asterisk), null);
		L.RegVar("Plus", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Plus), null);
		L.RegVar("Comma", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Comma), null);
		L.RegVar("Minus", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Minus), null);
		L.RegVar("Period", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Period), null);
		L.RegVar("Slash", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Slash), null);
		L.RegVar("Colon", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Colon), null);
		L.RegVar("Semicolon", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Semicolon), null);
		L.RegVar("Less", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Less), null);
		L.RegVar("Equals", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Equals), null);
		L.RegVar("Greater", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Greater), null);
		L.RegVar("Question", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Question), null);
		L.RegVar("At", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_At), null);
		L.RegVar("LeftBracket", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftBracket), null);
		L.RegVar("Backslash", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Backslash), null);
		L.RegVar("RightBracket", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightBracket), null);
		L.RegVar("Caret", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Caret), null);
		L.RegVar("Underscore", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Underscore), null);
		L.RegVar("BackQuote", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_BackQuote), null);
		L.RegVar("A", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_A), null);
		L.RegVar("B", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_B), null);
		L.RegVar("C", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_C), null);
		L.RegVar("D", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_D), null);
		L.RegVar("E", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_E), null);
		L.RegVar("F", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_F), null);
		L.RegVar("G", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_G), null);
		L.RegVar("H", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_H), null);
		L.RegVar("I", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_I), null);
		L.RegVar("J", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_J), null);
		L.RegVar("K", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_K), null);
		L.RegVar("L", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_L), null);
		L.RegVar("M", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_M), null);
		L.RegVar("N", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_N), null);
		L.RegVar("O", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_O), null);
		L.RegVar("P", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_P), null);
		L.RegVar("Q", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Q), null);
		L.RegVar("R", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_R), null);
		L.RegVar("S", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_S), null);
		L.RegVar("T", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_T), null);
		L.RegVar("U", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_U), null);
		L.RegVar("V", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_V), null);
		L.RegVar("W", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_W), null);
		L.RegVar("X", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_X), null);
		L.RegVar("Y", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Y), null);
		L.RegVar("Z", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Z), null);
		L.RegVar("Numlock", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Numlock), null);
		L.RegVar("CapsLock", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_CapsLock), null);
		L.RegVar("ScrollLock", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_ScrollLock), null);
		L.RegVar("RightShift", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightShift), null);
		L.RegVar("LeftShift", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftShift), null);
		L.RegVar("RightControl", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightControl), null);
		L.RegVar("LeftControl", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftControl), null);
		L.RegVar("RightAlt", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightAlt), null);
		L.RegVar("LeftAlt", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftAlt), null);
		L.RegVar("LeftCommand", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftCommand), null);
		L.RegVar("LeftApple", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftApple), null);
		L.RegVar("LeftWindows", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_LeftWindows), null);
		L.RegVar("RightCommand", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightCommand), null);
		L.RegVar("RightApple", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightApple), null);
		L.RegVar("RightWindows", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_RightWindows), null);
		L.RegVar("AltGr", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_AltGr), null);
		L.RegVar("Help", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Help), null);
		L.RegVar("Print", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Print), null);
		L.RegVar("SysReq", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_SysReq), null);
		L.RegVar("Break", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Break), null);
		L.RegVar("Menu", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Menu), null);
		L.RegVar("Mouse0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse0), null);
		L.RegVar("Mouse1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse1), null);
		L.RegVar("Mouse2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse2), null);
		L.RegVar("Mouse3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse3), null);
		L.RegVar("Mouse4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse4), null);
		L.RegVar("Mouse5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse5), null);
		L.RegVar("Mouse6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Mouse6), null);
		L.RegVar("JoystickButton0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton0), null);
		L.RegVar("JoystickButton1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton1), null);
		L.RegVar("JoystickButton2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton2), null);
		L.RegVar("JoystickButton3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton3), null);
		L.RegVar("JoystickButton4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton4), null);
		L.RegVar("JoystickButton5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton5), null);
		L.RegVar("JoystickButton6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton6), null);
		L.RegVar("JoystickButton7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton7), null);
		L.RegVar("JoystickButton8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton8), null);
		L.RegVar("JoystickButton9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton9), null);
		L.RegVar("JoystickButton10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton10), null);
		L.RegVar("JoystickButton11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton11), null);
		L.RegVar("JoystickButton12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton12), null);
		L.RegVar("JoystickButton13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton13), null);
		L.RegVar("JoystickButton14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton14), null);
		L.RegVar("JoystickButton15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton15), null);
		L.RegVar("JoystickButton16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton16), null);
		L.RegVar("JoystickButton17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton17), null);
		L.RegVar("JoystickButton18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton18), null);
		L.RegVar("JoystickButton19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_JoystickButton19), null);
		L.RegVar("Joystick1Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button0), null);
		L.RegVar("Joystick1Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button1), null);
		L.RegVar("Joystick1Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button2), null);
		L.RegVar("Joystick1Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button3), null);
		L.RegVar("Joystick1Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button4), null);
		L.RegVar("Joystick1Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button5), null);
		L.RegVar("Joystick1Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button6), null);
		L.RegVar("Joystick1Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button7), null);
		L.RegVar("Joystick1Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button8), null);
		L.RegVar("Joystick1Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button9), null);
		L.RegVar("Joystick1Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button10), null);
		L.RegVar("Joystick1Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button11), null);
		L.RegVar("Joystick1Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button12), null);
		L.RegVar("Joystick1Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button13), null);
		L.RegVar("Joystick1Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button14), null);
		L.RegVar("Joystick1Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button15), null);
		L.RegVar("Joystick1Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button16), null);
		L.RegVar("Joystick1Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button17), null);
		L.RegVar("Joystick1Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button18), null);
		L.RegVar("Joystick1Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick1Button19), null);
		L.RegVar("Joystick2Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button0), null);
		L.RegVar("Joystick2Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button1), null);
		L.RegVar("Joystick2Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button2), null);
		L.RegVar("Joystick2Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button3), null);
		L.RegVar("Joystick2Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button4), null);
		L.RegVar("Joystick2Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button5), null);
		L.RegVar("Joystick2Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button6), null);
		L.RegVar("Joystick2Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button7), null);
		L.RegVar("Joystick2Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button8), null);
		L.RegVar("Joystick2Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button9), null);
		L.RegVar("Joystick2Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button10), null);
		L.RegVar("Joystick2Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button11), null);
		L.RegVar("Joystick2Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button12), null);
		L.RegVar("Joystick2Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button13), null);
		L.RegVar("Joystick2Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button14), null);
		L.RegVar("Joystick2Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button15), null);
		L.RegVar("Joystick2Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button16), null);
		L.RegVar("Joystick2Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button17), null);
		L.RegVar("Joystick2Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button18), null);
		L.RegVar("Joystick2Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick2Button19), null);
		L.RegVar("Joystick3Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button0), null);
		L.RegVar("Joystick3Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button1), null);
		L.RegVar("Joystick3Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button2), null);
		L.RegVar("Joystick3Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button3), null);
		L.RegVar("Joystick3Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button4), null);
		L.RegVar("Joystick3Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button5), null);
		L.RegVar("Joystick3Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button6), null);
		L.RegVar("Joystick3Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button7), null);
		L.RegVar("Joystick3Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button8), null);
		L.RegVar("Joystick3Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button9), null);
		L.RegVar("Joystick3Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button10), null);
		L.RegVar("Joystick3Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button11), null);
		L.RegVar("Joystick3Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button12), null);
		L.RegVar("Joystick3Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button13), null);
		L.RegVar("Joystick3Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button14), null);
		L.RegVar("Joystick3Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button15), null);
		L.RegVar("Joystick3Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button16), null);
		L.RegVar("Joystick3Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button17), null);
		L.RegVar("Joystick3Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button18), null);
		L.RegVar("Joystick3Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick3Button19), null);
		L.RegVar("Joystick4Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button0), null);
		L.RegVar("Joystick4Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button1), null);
		L.RegVar("Joystick4Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button2), null);
		L.RegVar("Joystick4Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button3), null);
		L.RegVar("Joystick4Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button4), null);
		L.RegVar("Joystick4Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button5), null);
		L.RegVar("Joystick4Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button6), null);
		L.RegVar("Joystick4Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button7), null);
		L.RegVar("Joystick4Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button8), null);
		L.RegVar("Joystick4Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button9), null);
		L.RegVar("Joystick4Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button10), null);
		L.RegVar("Joystick4Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button11), null);
		L.RegVar("Joystick4Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button12), null);
		L.RegVar("Joystick4Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button13), null);
		L.RegVar("Joystick4Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button14), null);
		L.RegVar("Joystick4Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button15), null);
		L.RegVar("Joystick4Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button16), null);
		L.RegVar("Joystick4Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button17), null);
		L.RegVar("Joystick4Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button18), null);
		L.RegVar("Joystick4Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick4Button19), null);
		L.RegVar("Joystick5Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button0), null);
		L.RegVar("Joystick5Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button1), null);
		L.RegVar("Joystick5Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button2), null);
		L.RegVar("Joystick5Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button3), null);
		L.RegVar("Joystick5Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button4), null);
		L.RegVar("Joystick5Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button5), null);
		L.RegVar("Joystick5Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button6), null);
		L.RegVar("Joystick5Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button7), null);
		L.RegVar("Joystick5Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button8), null);
		L.RegVar("Joystick5Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button9), null);
		L.RegVar("Joystick5Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button10), null);
		L.RegVar("Joystick5Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button11), null);
		L.RegVar("Joystick5Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button12), null);
		L.RegVar("Joystick5Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button13), null);
		L.RegVar("Joystick5Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button14), null);
		L.RegVar("Joystick5Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button15), null);
		L.RegVar("Joystick5Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button16), null);
		L.RegVar("Joystick5Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button17), null);
		L.RegVar("Joystick5Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button18), null);
		L.RegVar("Joystick5Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick5Button19), null);
		L.RegVar("Joystick6Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button0), null);
		L.RegVar("Joystick6Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button1), null);
		L.RegVar("Joystick6Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button2), null);
		L.RegVar("Joystick6Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button3), null);
		L.RegVar("Joystick6Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button4), null);
		L.RegVar("Joystick6Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button5), null);
		L.RegVar("Joystick6Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button6), null);
		L.RegVar("Joystick6Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button7), null);
		L.RegVar("Joystick6Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button8), null);
		L.RegVar("Joystick6Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button9), null);
		L.RegVar("Joystick6Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button10), null);
		L.RegVar("Joystick6Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button11), null);
		L.RegVar("Joystick6Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button12), null);
		L.RegVar("Joystick6Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button13), null);
		L.RegVar("Joystick6Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button14), null);
		L.RegVar("Joystick6Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button15), null);
		L.RegVar("Joystick6Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button16), null);
		L.RegVar("Joystick6Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button17), null);
		L.RegVar("Joystick6Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button18), null);
		L.RegVar("Joystick6Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick6Button19), null);
		L.RegVar("Joystick7Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button0), null);
		L.RegVar("Joystick7Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button1), null);
		L.RegVar("Joystick7Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button2), null);
		L.RegVar("Joystick7Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button3), null);
		L.RegVar("Joystick7Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button4), null);
		L.RegVar("Joystick7Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button5), null);
		L.RegVar("Joystick7Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button6), null);
		L.RegVar("Joystick7Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button7), null);
		L.RegVar("Joystick7Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button8), null);
		L.RegVar("Joystick7Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button9), null);
		L.RegVar("Joystick7Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button10), null);
		L.RegVar("Joystick7Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button11), null);
		L.RegVar("Joystick7Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button12), null);
		L.RegVar("Joystick7Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button13), null);
		L.RegVar("Joystick7Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button14), null);
		L.RegVar("Joystick7Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button15), null);
		L.RegVar("Joystick7Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button16), null);
		L.RegVar("Joystick7Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button17), null);
		L.RegVar("Joystick7Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button18), null);
		L.RegVar("Joystick7Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick7Button19), null);
		L.RegVar("Joystick8Button0", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button0), null);
		L.RegVar("Joystick8Button1", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button1), null);
		L.RegVar("Joystick8Button2", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button2), null);
		L.RegVar("Joystick8Button3", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button3), null);
		L.RegVar("Joystick8Button4", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button4), null);
		L.RegVar("Joystick8Button5", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button5), null);
		L.RegVar("Joystick8Button6", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button6), null);
		L.RegVar("Joystick8Button7", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button7), null);
		L.RegVar("Joystick8Button8", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button8), null);
		L.RegVar("Joystick8Button9", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button9), null);
		L.RegVar("Joystick8Button10", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button10), null);
		L.RegVar("Joystick8Button11", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button11), null);
		L.RegVar("Joystick8Button12", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button12), null);
		L.RegVar("Joystick8Button13", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button13), null);
		L.RegVar("Joystick8Button14", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button14), null);
		L.RegVar("Joystick8Button15", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button15), null);
		L.RegVar("Joystick8Button16", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button16), null);
		L.RegVar("Joystick8Button17", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button17), null);
		L.RegVar("Joystick8Button18", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button18), null);
		L.RegVar("Joystick8Button19", new LuaCSFunction(UnityEngine_KeyCodeWrap.get_Joystick8Button19), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(UnityEngine_KeyCodeWrap.IntToEnum));
		L.EndEnum();
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_None(IntPtr L)
	{
		ToLua.Push(L, 0);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Backspace(IntPtr L)
	{
		ToLua.Push(L, 8);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Delete(IntPtr L)
	{
		ToLua.Push(L, 127);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Tab(IntPtr L)
	{
		ToLua.Push(L, 9);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Clear(IntPtr L)
	{
		ToLua.Push(L, 12);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Return(IntPtr L)
	{
		ToLua.Push(L, 13);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Pause(IntPtr L)
	{
		ToLua.Push(L, 19);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Escape(IntPtr L)
	{
		ToLua.Push(L, 27);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Space(IntPtr L)
	{
		ToLua.Push(L, 32);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad0(IntPtr L)
	{
		ToLua.Push(L, 256);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad1(IntPtr L)
	{
		ToLua.Push(L, 257);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad2(IntPtr L)
	{
		ToLua.Push(L, 258);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad3(IntPtr L)
	{
		ToLua.Push(L, 259);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad4(IntPtr L)
	{
		ToLua.Push(L, 260);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad5(IntPtr L)
	{
		ToLua.Push(L, 261);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad6(IntPtr L)
	{
		ToLua.Push(L, 262);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad7(IntPtr L)
	{
		ToLua.Push(L, 263);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad8(IntPtr L)
	{
		ToLua.Push(L, 264);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Keypad9(IntPtr L)
	{
		ToLua.Push(L, 265);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadPeriod(IntPtr L)
	{
		ToLua.Push(L, 266);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadDivide(IntPtr L)
	{
		ToLua.Push(L, 267);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadMultiply(IntPtr L)
	{
		ToLua.Push(L, 268);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadMinus(IntPtr L)
	{
		ToLua.Push(L, 269);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadPlus(IntPtr L)
	{
		ToLua.Push(L, 270);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadEnter(IntPtr L)
	{
		ToLua.Push(L, 271);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_KeypadEquals(IntPtr L)
	{
		ToLua.Push(L, 272);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_UpArrow(IntPtr L)
	{
		ToLua.Push(L, 273);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_DownArrow(IntPtr L)
	{
		ToLua.Push(L, 274);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightArrow(IntPtr L)
	{
		ToLua.Push(L, 275);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftArrow(IntPtr L)
	{
		ToLua.Push(L, 276);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Insert(IntPtr L)
	{
		ToLua.Push(L, 277);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Home(IntPtr L)
	{
		ToLua.Push(L, 278);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_End(IntPtr L)
	{
		ToLua.Push(L, 279);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_PageUp(IntPtr L)
	{
		ToLua.Push(L, 280);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_PageDown(IntPtr L)
	{
		ToLua.Push(L, 281);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F1(IntPtr L)
	{
		ToLua.Push(L, 282);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F2(IntPtr L)
	{
		ToLua.Push(L, 283);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F3(IntPtr L)
	{
		ToLua.Push(L, 284);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F4(IntPtr L)
	{
		ToLua.Push(L, 285);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F5(IntPtr L)
	{
		ToLua.Push(L, 286);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F6(IntPtr L)
	{
		ToLua.Push(L, 287);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F7(IntPtr L)
	{
		ToLua.Push(L, 288);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F8(IntPtr L)
	{
		ToLua.Push(L, 289);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F9(IntPtr L)
	{
		ToLua.Push(L, 290);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F10(IntPtr L)
	{
		ToLua.Push(L, 291);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F11(IntPtr L)
	{
		ToLua.Push(L, 292);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F12(IntPtr L)
	{
		ToLua.Push(L, 293);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F13(IntPtr L)
	{
		ToLua.Push(L, 294);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F14(IntPtr L)
	{
		ToLua.Push(L, 295);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F15(IntPtr L)
	{
		ToLua.Push(L, 296);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha0(IntPtr L)
	{
		ToLua.Push(L, 48);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha1(IntPtr L)
	{
		ToLua.Push(L, 49);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha2(IntPtr L)
	{
		ToLua.Push(L, 50);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha3(IntPtr L)
	{
		ToLua.Push(L, 51);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha4(IntPtr L)
	{
		ToLua.Push(L, 52);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha5(IntPtr L)
	{
		ToLua.Push(L, 53);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha6(IntPtr L)
	{
		ToLua.Push(L, 54);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha7(IntPtr L)
	{
		ToLua.Push(L, 55);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha8(IntPtr L)
	{
		ToLua.Push(L, 56);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Alpha9(IntPtr L)
	{
		ToLua.Push(L, 57);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Exclaim(IntPtr L)
	{
		ToLua.Push(L, 33);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_DoubleQuote(IntPtr L)
	{
		ToLua.Push(L, 34);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Hash(IntPtr L)
	{
		ToLua.Push(L, 35);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Dollar(IntPtr L)
	{
		ToLua.Push(L, 36);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Ampersand(IntPtr L)
	{
		ToLua.Push(L, 38);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Quote(IntPtr L)
	{
		ToLua.Push(L, 39);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftParen(IntPtr L)
	{
		ToLua.Push(L, 40);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightParen(IntPtr L)
	{
		ToLua.Push(L, 41);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Asterisk(IntPtr L)
	{
		ToLua.Push(L, 42);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Plus(IntPtr L)
	{
		ToLua.Push(L, 43);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Comma(IntPtr L)
	{
		ToLua.Push(L, 44);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Minus(IntPtr L)
	{
		ToLua.Push(L, 45);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Period(IntPtr L)
	{
		ToLua.Push(L, 46);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Slash(IntPtr L)
	{
		ToLua.Push(L, 47);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Colon(IntPtr L)
	{
		ToLua.Push(L, 58);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Semicolon(IntPtr L)
	{
		ToLua.Push(L, 59);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Less(IntPtr L)
	{
		ToLua.Push(L, 60);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Equals(IntPtr L)
	{
		ToLua.Push(L, 61);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Greater(IntPtr L)
	{
		ToLua.Push(L, 62);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Question(IntPtr L)
	{
		ToLua.Push(L, 63);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_At(IntPtr L)
	{
		ToLua.Push(L, 64);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftBracket(IntPtr L)
	{
		ToLua.Push(L, 91);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Backslash(IntPtr L)
	{
		ToLua.Push(L, 92);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightBracket(IntPtr L)
	{
		ToLua.Push(L, 93);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Caret(IntPtr L)
	{
		ToLua.Push(L, 94);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Underscore(IntPtr L)
	{
		ToLua.Push(L, 95);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_BackQuote(IntPtr L)
	{
		ToLua.Push(L, 96);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_A(IntPtr L)
	{
		ToLua.Push(L, 97);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_B(IntPtr L)
	{
		ToLua.Push(L, 98);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_C(IntPtr L)
	{
		ToLua.Push(L, 99);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_D(IntPtr L)
	{
		ToLua.Push(L, 100);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_E(IntPtr L)
	{
		ToLua.Push(L, 101);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_F(IntPtr L)
	{
		ToLua.Push(L, 102);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_G(IntPtr L)
	{
		ToLua.Push(L, 103);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_H(IntPtr L)
	{
		ToLua.Push(L, 104);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_I(IntPtr L)
	{
		ToLua.Push(L, 105);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_J(IntPtr L)
	{
		ToLua.Push(L, 106);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_K(IntPtr L)
	{
		ToLua.Push(L, 107);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_L(IntPtr L)
	{
		ToLua.Push(L, 108);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_M(IntPtr L)
	{
		ToLua.Push(L, 109);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_N(IntPtr L)
	{
		ToLua.Push(L, 110);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_O(IntPtr L)
	{
		ToLua.Push(L, 111);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_P(IntPtr L)
	{
		ToLua.Push(L, 112);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Q(IntPtr L)
	{
		ToLua.Push(L, 113);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_R(IntPtr L)
	{
		ToLua.Push(L, 114);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_S(IntPtr L)
	{
		ToLua.Push(L, 115);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_T(IntPtr L)
	{
		ToLua.Push(L, 116);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_U(IntPtr L)
	{
		ToLua.Push(L, 117);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_V(IntPtr L)
	{
		ToLua.Push(L, 118);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_W(IntPtr L)
	{
		ToLua.Push(L, 119);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_X(IntPtr L)
	{
		ToLua.Push(L, 120);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Y(IntPtr L)
	{
		ToLua.Push(L, 121);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Z(IntPtr L)
	{
		ToLua.Push(L, 122);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Numlock(IntPtr L)
	{
		ToLua.Push(L, 300);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_CapsLock(IntPtr L)
	{
		ToLua.Push(L, 301);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_ScrollLock(IntPtr L)
	{
		ToLua.Push(L, 302);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightShift(IntPtr L)
	{
		ToLua.Push(L, 303);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftShift(IntPtr L)
	{
		ToLua.Push(L, 304);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightControl(IntPtr L)
	{
		ToLua.Push(L, 305);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftControl(IntPtr L)
	{
		ToLua.Push(L, 306);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightAlt(IntPtr L)
	{
		ToLua.Push(L, 307);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftAlt(IntPtr L)
	{
		ToLua.Push(L, 308);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftCommand(IntPtr L)
	{
		ToLua.Push(L, 310);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftApple(IntPtr L)
	{
		ToLua.Push(L, 310);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_LeftWindows(IntPtr L)
	{
		ToLua.Push(L, 311);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightCommand(IntPtr L)
	{
		ToLua.Push(L, 309);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightApple(IntPtr L)
	{
		ToLua.Push(L, 309);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_RightWindows(IntPtr L)
	{
		ToLua.Push(L, 312);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_AltGr(IntPtr L)
	{
		ToLua.Push(L, 313);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Help(IntPtr L)
	{
		ToLua.Push(L, 315);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Print(IntPtr L)
	{
		ToLua.Push(L, 316);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_SysReq(IntPtr L)
	{
		ToLua.Push(L, 317);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Break(IntPtr L)
	{
		ToLua.Push(L, 318);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Menu(IntPtr L)
	{
		ToLua.Push(L, 319);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse0(IntPtr L)
	{
		ToLua.Push(L, 323);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse1(IntPtr L)
	{
		ToLua.Push(L, 324);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse2(IntPtr L)
	{
		ToLua.Push(L, 325);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse3(IntPtr L)
	{
		ToLua.Push(L, 326);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse4(IntPtr L)
	{
		ToLua.Push(L, 327);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse5(IntPtr L)
	{
		ToLua.Push(L, 328);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Mouse6(IntPtr L)
	{
		ToLua.Push(L, 329);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton0(IntPtr L)
	{
		ToLua.Push(L, 330);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton1(IntPtr L)
	{
		ToLua.Push(L, 331);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton2(IntPtr L)
	{
		ToLua.Push(L, 332);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton3(IntPtr L)
	{
		ToLua.Push(L, 333);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton4(IntPtr L)
	{
		ToLua.Push(L, 334);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton5(IntPtr L)
	{
		ToLua.Push(L, 335);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton6(IntPtr L)
	{
		ToLua.Push(L, 336);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton7(IntPtr L)
	{
		ToLua.Push(L, 337);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton8(IntPtr L)
	{
		ToLua.Push(L, 338);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton9(IntPtr L)
	{
		ToLua.Push(L, 339);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton10(IntPtr L)
	{
		ToLua.Push(L, 340);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton11(IntPtr L)
	{
		ToLua.Push(L, 341);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton12(IntPtr L)
	{
		ToLua.Push(L, 342);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton13(IntPtr L)
	{
		ToLua.Push(L, 343);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton14(IntPtr L)
	{
		ToLua.Push(L, 344);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton15(IntPtr L)
	{
		ToLua.Push(L, 345);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton16(IntPtr L)
	{
		ToLua.Push(L, 346);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton17(IntPtr L)
	{
		ToLua.Push(L, 347);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton18(IntPtr L)
	{
		ToLua.Push(L, 348);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_JoystickButton19(IntPtr L)
	{
		ToLua.Push(L, 349);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button0(IntPtr L)
	{
		ToLua.Push(L, 350);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button1(IntPtr L)
	{
		ToLua.Push(L, 351);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button2(IntPtr L)
	{
		ToLua.Push(L, 352);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button3(IntPtr L)
	{
		ToLua.Push(L, 353);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button4(IntPtr L)
	{
		ToLua.Push(L, 354);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button5(IntPtr L)
	{
		ToLua.Push(L, 355);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button6(IntPtr L)
	{
		ToLua.Push(L, 356);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button7(IntPtr L)
	{
		ToLua.Push(L, 357);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button8(IntPtr L)
	{
		ToLua.Push(L, 358);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button9(IntPtr L)
	{
		ToLua.Push(L, 359);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button10(IntPtr L)
	{
		ToLua.Push(L, 360);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button11(IntPtr L)
	{
		ToLua.Push(L, 361);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button12(IntPtr L)
	{
		ToLua.Push(L, 362);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button13(IntPtr L)
	{
		ToLua.Push(L, 363);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button14(IntPtr L)
	{
		ToLua.Push(L, 364);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button15(IntPtr L)
	{
		ToLua.Push(L, 365);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button16(IntPtr L)
	{
		ToLua.Push(L, 366);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button17(IntPtr L)
	{
		ToLua.Push(L, 367);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button18(IntPtr L)
	{
		ToLua.Push(L, 368);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick1Button19(IntPtr L)
	{
		ToLua.Push(L, 369);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button0(IntPtr L)
	{
		ToLua.Push(L, 370);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button1(IntPtr L)
	{
		ToLua.Push(L, 371);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button2(IntPtr L)
	{
		ToLua.Push(L, 372);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button3(IntPtr L)
	{
		ToLua.Push(L, 373);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button4(IntPtr L)
	{
		ToLua.Push(L, 374);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button5(IntPtr L)
	{
		ToLua.Push(L, 375);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button6(IntPtr L)
	{
		ToLua.Push(L, 376);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button7(IntPtr L)
	{
		ToLua.Push(L, 377);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button8(IntPtr L)
	{
		ToLua.Push(L, 378);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button9(IntPtr L)
	{
		ToLua.Push(L, 379);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button10(IntPtr L)
	{
		ToLua.Push(L, 380);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button11(IntPtr L)
	{
		ToLua.Push(L, 381);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button12(IntPtr L)
	{
		ToLua.Push(L, 382);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button13(IntPtr L)
	{
		ToLua.Push(L, 383);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button14(IntPtr L)
	{
		ToLua.Push(L, 384);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button15(IntPtr L)
	{
		ToLua.Push(L, 385);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button16(IntPtr L)
	{
		ToLua.Push(L, 386);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button17(IntPtr L)
	{
		ToLua.Push(L, 387);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button18(IntPtr L)
	{
		ToLua.Push(L, 388);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick2Button19(IntPtr L)
	{
		ToLua.Push(L, 389);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button0(IntPtr L)
	{
		ToLua.Push(L, 390);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button1(IntPtr L)
	{
		ToLua.Push(L, 391);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button2(IntPtr L)
	{
		ToLua.Push(L, 392);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button3(IntPtr L)
	{
		ToLua.Push(L, 393);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button4(IntPtr L)
	{
		ToLua.Push(L, 394);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button5(IntPtr L)
	{
		ToLua.Push(L, 395);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button6(IntPtr L)
	{
		ToLua.Push(L, 396);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button7(IntPtr L)
	{
		ToLua.Push(L, 397);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button8(IntPtr L)
	{
		ToLua.Push(L, 398);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button9(IntPtr L)
	{
		ToLua.Push(L, 399);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button10(IntPtr L)
	{
		ToLua.Push(L, 400);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button11(IntPtr L)
	{
		ToLua.Push(L, 401);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button12(IntPtr L)
	{
		ToLua.Push(L, 402);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button13(IntPtr L)
	{
		ToLua.Push(L, 403);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button14(IntPtr L)
	{
		ToLua.Push(L, 404);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button15(IntPtr L)
	{
		ToLua.Push(L, 405);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button16(IntPtr L)
	{
		ToLua.Push(L, 406);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button17(IntPtr L)
	{
		ToLua.Push(L, 407);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button18(IntPtr L)
	{
		ToLua.Push(L, 408);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick3Button19(IntPtr L)
	{
		ToLua.Push(L, 409);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button0(IntPtr L)
	{
		ToLua.Push(L, 410);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button1(IntPtr L)
	{
		ToLua.Push(L, 411);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button2(IntPtr L)
	{
		ToLua.Push(L, 412);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button3(IntPtr L)
	{
		ToLua.Push(L, 413);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button4(IntPtr L)
	{
		ToLua.Push(L, 414);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button5(IntPtr L)
	{
		ToLua.Push(L, 415);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button6(IntPtr L)
	{
		ToLua.Push(L, 416);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button7(IntPtr L)
	{
		ToLua.Push(L, 417);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button8(IntPtr L)
	{
		ToLua.Push(L, 418);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button9(IntPtr L)
	{
		ToLua.Push(L, 419);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button10(IntPtr L)
	{
		ToLua.Push(L, 420);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button11(IntPtr L)
	{
		ToLua.Push(L, 421);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button12(IntPtr L)
	{
		ToLua.Push(L, 422);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button13(IntPtr L)
	{
		ToLua.Push(L, 423);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button14(IntPtr L)
	{
		ToLua.Push(L, 424);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button15(IntPtr L)
	{
		ToLua.Push(L, 425);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button16(IntPtr L)
	{
		ToLua.Push(L, 426);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button17(IntPtr L)
	{
		ToLua.Push(L, 427);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button18(IntPtr L)
	{
		ToLua.Push(L, 428);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick4Button19(IntPtr L)
	{
		ToLua.Push(L, 429);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button0(IntPtr L)
	{
		ToLua.Push(L, 430);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button1(IntPtr L)
	{
		ToLua.Push(L, 431);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button2(IntPtr L)
	{
		ToLua.Push(L, 432);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button3(IntPtr L)
	{
		ToLua.Push(L, 433);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button4(IntPtr L)
	{
		ToLua.Push(L, 434);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button5(IntPtr L)
	{
		ToLua.Push(L, 435);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button6(IntPtr L)
	{
		ToLua.Push(L, 436);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button7(IntPtr L)
	{
		ToLua.Push(L, 437);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button8(IntPtr L)
	{
		ToLua.Push(L, 438);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button9(IntPtr L)
	{
		ToLua.Push(L, 439);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button10(IntPtr L)
	{
		ToLua.Push(L, 440);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button11(IntPtr L)
	{
		ToLua.Push(L, 441);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button12(IntPtr L)
	{
		ToLua.Push(L, 442);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button13(IntPtr L)
	{
		ToLua.Push(L, 443);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button14(IntPtr L)
	{
		ToLua.Push(L, 444);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button15(IntPtr L)
	{
		ToLua.Push(L, 445);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button16(IntPtr L)
	{
		ToLua.Push(L, 446);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button17(IntPtr L)
	{
		ToLua.Push(L, 447);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button18(IntPtr L)
	{
		ToLua.Push(L, 448);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick5Button19(IntPtr L)
	{
		ToLua.Push(L, 449);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button0(IntPtr L)
	{
		ToLua.Push(L, 450);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button1(IntPtr L)
	{
		ToLua.Push(L, 451);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button2(IntPtr L)
	{
		ToLua.Push(L, 452);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button3(IntPtr L)
	{
		ToLua.Push(L, 453);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button4(IntPtr L)
	{
		ToLua.Push(L, 454);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button5(IntPtr L)
	{
		ToLua.Push(L, 455);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button6(IntPtr L)
	{
		ToLua.Push(L, 456);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button7(IntPtr L)
	{
		ToLua.Push(L, 457);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button8(IntPtr L)
	{
		ToLua.Push(L, 458);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button9(IntPtr L)
	{
		ToLua.Push(L, 459);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button10(IntPtr L)
	{
		ToLua.Push(L, 460);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button11(IntPtr L)
	{
		ToLua.Push(L, 461);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button12(IntPtr L)
	{
		ToLua.Push(L, 462);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button13(IntPtr L)
	{
		ToLua.Push(L, 463);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button14(IntPtr L)
	{
		ToLua.Push(L, 464);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button15(IntPtr L)
	{
		ToLua.Push(L, 465);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button16(IntPtr L)
	{
		ToLua.Push(L, 466);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button17(IntPtr L)
	{
		ToLua.Push(L, 467);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button18(IntPtr L)
	{
		ToLua.Push(L, 468);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick6Button19(IntPtr L)
	{
		ToLua.Push(L, 469);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button0(IntPtr L)
	{
		ToLua.Push(L, 470);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button1(IntPtr L)
	{
		ToLua.Push(L, 471);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button2(IntPtr L)
	{
		ToLua.Push(L, 472);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button3(IntPtr L)
	{
		ToLua.Push(L, 473);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button4(IntPtr L)
	{
		ToLua.Push(L, 474);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button5(IntPtr L)
	{
		ToLua.Push(L, 475);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button6(IntPtr L)
	{
		ToLua.Push(L, 476);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button7(IntPtr L)
	{
		ToLua.Push(L, 477);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button8(IntPtr L)
	{
		ToLua.Push(L, 478);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button9(IntPtr L)
	{
		ToLua.Push(L, 479);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button10(IntPtr L)
	{
		ToLua.Push(L, 480);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button11(IntPtr L)
	{
		ToLua.Push(L, 481);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button12(IntPtr L)
	{
		ToLua.Push(L, 482);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button13(IntPtr L)
	{
		ToLua.Push(L, 483);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button14(IntPtr L)
	{
		ToLua.Push(L, 484);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button15(IntPtr L)
	{
		ToLua.Push(L, 485);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button16(IntPtr L)
	{
		ToLua.Push(L, 486);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button17(IntPtr L)
	{
		ToLua.Push(L, 487);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button18(IntPtr L)
	{
		ToLua.Push(L, 488);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick7Button19(IntPtr L)
	{
		ToLua.Push(L, 489);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button0(IntPtr L)
	{
		ToLua.Push(L, 490);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button1(IntPtr L)
	{
		ToLua.Push(L, 491);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button2(IntPtr L)
	{
		ToLua.Push(L, 492);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button3(IntPtr L)
	{
		ToLua.Push(L, 493);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button4(IntPtr L)
	{
		ToLua.Push(L, 494);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button5(IntPtr L)
	{
		ToLua.Push(L, 495);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button6(IntPtr L)
	{
		ToLua.Push(L, 496);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button7(IntPtr L)
	{
		ToLua.Push(L, 497);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button8(IntPtr L)
	{
		ToLua.Push(L, 498);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button9(IntPtr L)
	{
		ToLua.Push(L, 499);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button10(IntPtr L)
	{
		ToLua.Push(L, 500);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button11(IntPtr L)
	{
		ToLua.Push(L, 501);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button12(IntPtr L)
	{
		ToLua.Push(L, 502);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button13(IntPtr L)
	{
		ToLua.Push(L, 503);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button14(IntPtr L)
	{
		ToLua.Push(L, 504);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button15(IntPtr L)
	{
		ToLua.Push(L, 505);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button16(IntPtr L)
	{
		ToLua.Push(L, 506);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button17(IntPtr L)
	{
		ToLua.Push(L, 507);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button18(IntPtr L)
	{
		ToLua.Push(L, 508);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int get_Joystick8Button19(IntPtr L)
	{
		ToLua.Push(L, 509);
		return 1;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	private static int IntToEnum(IntPtr L)
	{
		int num = (int)LuaDLL.lua_tonumber(L, 1);
		KeyCode keyCode = num;
		ToLua.Push(L, keyCode);
		return 1;
	}
}
