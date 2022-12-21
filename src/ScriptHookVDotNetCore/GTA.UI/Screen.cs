//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System.Drawing;
using GTA.Math;

namespace GTA.UI
{
    /// <summary>
    /// Methods to handle UI actions that affect the whole screen.
    /// </summary>
    public static class Screen
    {
        #region Fields

        private static readonly string[] _effects = new string[]
        {
            "SwitchHUDIn",
            "SwitchHUDOut",
            "FocusIn",
            "FocusOut",
            "MinigameEndNeutral",
            "MinigameEndTrevor",
            "MinigameEndFranklin",
            "MinigameEndMichael",
            "MinigameTransitionOut",
            "MinigameTransitionIn",
            "SwitchShortNeutralIn",
            "SwitchShortFranklinIn",
            "SwitchShortTrevorIn",
            "SwitchShortMichaelIn",
            "SwitchOpenMichaelIn",
            "SwitchOpenFranklinIn",
            "SwitchOpenTrevorIn",
            "SwitchHUDMichaelOut",
            "SwitchHUDFranklinOut",
            "SwitchHUDTrevorOut",
            "SwitchShortFranklinMid",
            "SwitchShortMichaelMid",
            "SwitchShortTrevorMid",
            "DeathFailOut",
            "CamPushInNeutral",
            "CamPushInFranklin",
            "CamPushInMichael",
            "CamPushInTrevor",
            "SwitchSceneFranklin",
            "SwitchSceneTrevor",
            "SwitchSceneMichael",
            "SwitchSceneNeutral",
            "MP_Celeb_Win",
            "MP_Celeb_Win_Out",
            "MP_Celeb_Lose",
            "MP_Celeb_Lose_Out",
            "DeathFailNeutralIn",
            "DeathFailMPDark",
            "DeathFailMPIn",
            "MP_Celeb_Preload_Fade",
            "PeyoteEndOut",
            "PeyoteEndIn",
            "PeyoteIn",
            "PeyoteOut",
            "MP_race_crash",
            "SuccessFranklin",
            "SuccessTrevor",
            "SuccessMichael",
            "DrugsMichaelAliensFightIn",
            "DrugsMichaelAliensFight",
            "DrugsMichaelAliensFightOut",
            "DrugsTrevorClownsFightIn",
            "DrugsTrevorClownsFight",
            "DrugsTrevorClownsFightOut",
            "HeistCelebPass",
            "HeistCelebPassBW",
            "HeistCelebEnd",
            "HeistCelebToast",
            "MenuMGHeistIn",
            "MenuMGTournamentIn",
            "MenuMGSelectionIn",
            "ChopVision",
            "DMT_flight_intro",
            "DMT_flight",
            "DrugsDrivingIn",
            "DrugsDrivingOut",
            "SwitchOpenNeutralFIB5",
            "HeistLocate",
            "MP_job_load",
            "RaceTurbo",
            "MP_intro_logo",
            "HeistTripSkipFade",
            "MenuMGHeistOut",
            "MP_corona_switch",
            "MenuMGSelectionTint",
            "SuccessNeutral",
            "ExplosionJosh3",
            "SniperOverlay",
            "RampageOut",
            "Rampage",
            "Dont_tazeme_bro"
        };

        #endregion

        #region Dimensions

        /// <summary>
        /// The base width of the screen used for all UI Calculations, unless ScaledDraw is used
        /// </summary>
        public const float Width = 1280f;

        /// <summary>
        /// The base height of the screen used for all UI Calculations
        /// </summary>
        public const float Height = 720f;

        /// <summary>
        /// Gets the actual screen resolution the game is being rendered at
        /// </summary>
        public static Size Resolution
        {
            get
            {
                int width, height;
                unsafe
                {
                    Call(Hash.GET_ACTUAL_SCREEN_RESOLUTION, &width, &height);
                }

                return new Size(width, height);
            }
        }

        /// <summary>
        /// Gets the current screen aspect ratio
        /// </summary>
        public static float AspectRatio => Call<float>(Hash.GET_ASPECT_RATIO, 0);

        /// <summary>
        /// Gets the screen width scaled against a 720pixel height base.
        /// </summary>
        public static float ScaledWidth => Height * AspectRatio;

        #endregion

        #region Fading

        /// <summary>
        /// Gets a value indicating whether the screen is faded in.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the screen is faded in; otherwise, <see langword="false" />.
        /// </value>
        public static bool IsFadedIn => Call<bool>(Hash.IS_SCREEN_FADED_IN);

        /// <summary>
        /// Gets a value indicating whether the screen is faded out.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the screen is faded out; otherwise, <see langword="false" />.
        /// </value>
        public static bool IsFadedOut => Call<bool>(Hash.IS_SCREEN_FADED_OUT);

        /// <summary>
        /// Gets a value indicating whether the screen is fading in.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the screen is fading in; otherwise, <see langword="false" />.
        /// </value>
        public static bool IsFadingIn => Call<bool>(Hash.IS_SCREEN_FADING_IN);

        /// <summary>
        /// Gets a value indicating whether the screen is fading out.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if the screen is fading out; otherwise, <see langword="false" />.
        /// </value>
        public static bool IsFadingOut => Call<bool>(Hash.IS_SCREEN_FADING_OUT);

        /// <summary>
        /// Fades the screen in over a specific time, useful for transitioning
        /// </summary>
        /// <param name="time">The time for the fade in to take</param>
        public static void FadeIn(int time)
        {
            Call(Hash.DO_SCREEN_FADE_IN, time);
        }

        /// <summary>
        /// Fades the screen out over a specific time, useful for transitioning
        /// </summary>
        /// <param name="time">The time for the fade out to take</param>
        public static void FadeOut(int time)
        {
            Call(Hash.DO_SCREEN_FADE_OUT, time);
        }

        #endregion

        #region Screen Effects

        /// <summary>
        /// Gets a value indicating whether screen kill effects are enabled.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if screen kill effects are enabled; otherwise, <see langword="false" />.
        /// </value>
        public static bool AreScreenKillEffectsEnabled => Game.GetProfileSetting(226) != 0;

        /// <summary>
        /// Gets a value indicating whether the specific screen effect is running.
        /// </summary>
        /// <param name="effectName">The <see cref="ScreenEffect"/> to check.</param>
        /// <returns><see langword="true" /> if the screen effect is active; otherwise, <see langword="false" />.</returns>
        public static bool IsEffectActive(ScreenEffect effectName)
        {
            return Call<bool>(Hash.ANIMPOSTFX_IS_RUNNING, _effects[(int)effectName]);
        }

        /// <summary>
        /// Starts applying the specified effect to the screen.
        /// </summary>
        /// <param name="effectName">The <see cref="ScreenEffect"/> to start playing.</param>
        /// <param name="duration">The duration of the effect in milliseconds or zero to use the default length.</param>
        /// <param name="looped">If <see langword="true" /> the effect won't stop until <see cref="Screen.StopEffect(ScreenEffect)"/> is called.</param>
        public static void StartEffect(ScreenEffect effectName, int duration = 0, bool looped = false)
        {
            Call(Hash.ANIMPOSTFX_PLAY, _effects[(int)effectName], duration, looped);
        }

        /// <summary>
        /// Stops applying the specified effect to the screen.
        /// </summary>
        /// <param name="effectName">The <see cref="ScreenEffect"/> to stop playing.</param>
        public static void StopEffect(ScreenEffect effectName)
        {
            Call(Hash.ANIMPOSTFX_STOP, _effects[(int)effectName]);
        }

        /// <summary>
        /// Stops all currently running effects.
        /// </summary>
        public static void StopEffects()
        {
            Call(Hash.ANIMPOSTFX_STOP_ALL);
        }

        #endregion

        #region Text

        /// <summary>
        /// Gets a value indicating whether a help message is currently displayed.
        /// </summary>
        public static bool IsHelpTextDisplayed => Call<bool>(Hash.IS_HELP_MESSAGE_BEING_DISPLAYED);

        /// <summary>
        /// Shows a subtitle at the bottom of the screen for a given time
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="duration">The duration to display the subtitle in milliseconds.</param>
        public static void ShowSubtitle(string message, int duration = 2500)
        {
            ShowSubtitle(message, duration, true);
        }

        /// <summary>
        /// Shows a subtitle at the bottom of the screen for a given time
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="duration">The duration to display the subtitle in milliseconds.</param>
        /// <param name="drawImmediately">Whether to draw immediately or draw after all the queued subtitles have finished.</param>
        public static void ShowSubtitle(string message, int duration, bool drawImmediately = true)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_PRINT, CellEmailBcon);
            PushLongString(message);
            Call(Hash.END_TEXT_COMMAND_PRINT, duration, drawImmediately);
        }

        /// <summary>
        /// Displays a help message in the top corner of the screen this frame. Beeping sound will be played.
        /// </summary>
        /// <param name="helpText">The text to display.</param>
        public static void ShowHelpTextThisFrame(string helpText)
        {
            ShowHelpText(helpText, 1, true, false); // keeping it DRY :)
        }

        /// <summary>
        /// Displays a help message in the top corner of the screen this frame. Specify whether beeping sound plays.
        /// </summary>
        /// <param name="helpText">The text to display.</param>
        /// <param name="beep">Whether to play beeping sound</param>
        public static void ShowHelpTextThisFrame(string helpText, bool beep)
        {
            ShowHelpText(helpText, 1, beep, false);
        }

        /// <summary>
        /// Displays a help message in the top corner of the screen infinitely.
        /// </summary>
        /// <param name="helpText">The text to display.</param>
        /// <param name="duration">
        /// The duration how long the help text will be displayed in real time (not in game time which is influenced by game speed).
        /// if the value is not positive, the help text will be displayed for 7.5 seconds.
        /// </param>
        /// <param name="beep">Whether to play beeping sound.</param>
        /// <param name="looped">Whether to show this help message forever.</param>
        public static void ShowHelpText(string helpText, int duration = -1, bool beep = true, bool looped = false)
        {
            Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, CellEmailBcon);
            PushLongString(helpText);
            Call(Hash.END_TEXT_COMMAND_DISPLAY_HELP, 0, looped, beep, duration);
        }

        /// <summary>
        /// Clears a help message immediately.
        /// </summary>
        static void ClearHelpText()
        {
            Call(Hash.CLEAR_HELP, true);
        }

        #endregion

        #region Space Conversion

        /// <summary>
        /// Translates a point in WorldSpace to its given Coordinates on the <see cref="Screen"/>
        /// </summary>
        /// <param name="position">The position in the World.</param>
        /// <param name="scaleWidth">if set to <see langword="true" /> Returns the screen position scaled by <see cref="ScaledWidth"/>; otherwise, returns the screen position scaled by <see cref="Width"/>.</param>
        /// <returns></returns>
        public static PointF WorldToScreen(Vector3 position, bool scaleWidth = false)
        {
            float pointX, pointY;

            unsafe
            {
                if (!Call<bool>(Hash.GET_SCREEN_COORD_FROM_WORLD_COORD, position.X, position.Y, position.Z, &pointX,
                        &pointY))
                {
                    return PointF.Empty;
                }
            }

            pointX *= scaleWidth ? ScaledWidth : Width;
            pointY *= Height;

            return new PointF(pointX, pointY);
        }

        #endregion
    }
}