# ITU-CS-464-LAB-07 - Orb Rush Mobile Menu System

**Student Name:** Muhammad Hamza  
**Roll Number:** BSSE23001  
**Course:** CS464 - Lab 7: Mobile UI & Menu Systems  
**Instructor:** Muhammad Raza Butt  
**Demo Video Link:** https://drive.google.com/file/d/1FbkeUZQTevmNqozFcB0iEaFRN3ysFXMJ/view?usp=sharing

---

## Screens

### Screen 1: Splash Screen

| Screenshots                                                                                                                          | Design & Technical Notes                                                                                                                                                                                                                           |
| :----------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Splash Color](Docs/01-splash-colored.png)<br><br>**Greyscale:**<br>![Splash Greyscale](Docs/01-splash-greyscale.png) | • This is initial launch screen.<br>• Logo scaling is animated with `OutQuad` in 0.35s and progress bar fills untill 100%.<br>• In greyscale the loading fill can be seen somewhat brighter than dark track, so progress is visible without color. |

---

### Screen 2: Main Menu

| Screenshots                                                                                                                                    | Design & Technical Notes                                                                                                                                                                                                                                                           |
| :--------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Main Menu Color](Docs/02-mainmenu-colored.png)<br><br>**Greyscale:**<br>![Main Menu Greyscale](Docs/02-mainmenu-greyscale.png) | • Visuals are simple here and made large so that main action button which is play look prominent.<br>• Top bar groups star count and gem chip seperately from main action buttons.<br>• In greyscale, the cyan PLAY button converts to bright light value, standing out instantly. |

---

### Screen 3: Shop Screen

| Screenshots                                                                                                                  | Design & Technical Notes                                                                                                                                                                                                                                                          |
| :--------------------------------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Shop Color](Docs/03-shop-colored.png)<br><br>**Greyscale:**<br>![Shop Greyscale](Docs/03-shop-greyscale.png) | • New skins and upgrades can be purchased from here and both are separated in different tabs.<br>• Switching tabs also animates the highlighter of title.<br>• Greyscale test shows active tab text is sharp white on light pill, while inactive tab stays readable on dark card. |

---

### Screen 4: Settings Screen

| Screenshots                                                                                                                                  | Design & Technical Notes                                                                                                                                                                                                                                                             |
| :------------------------------------------------------------------------------------------------------------------------------------------- | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Settings Color](Docs/04-settings-colored.png)<br><br>**Greyscale:**<br>![Settings Greyscale](Docs/04-settings-greyscale.png) | • Simple settings for music loudness and just a effect slider and radio buttons.<br>• Reset progress requires 2 times click confirmation to prevent accidental loss.<br>• In greyscale, slider fills and toggle knobs stay bright white against dark tracks, keeping states obvious. |

---

### Screen 5: Credits Screen

| Screenshots                                                                                                                              | Design & Technical Notes                                                                                                                                                          |
| :--------------------------------------------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Credits Color](Docs/05-credits-colored.png)<br><br>**Greyscale:**<br>![Credits Greyscale](Docs/05-credits-greyscale.png) | • Displays project details, and font licenses.<br>• It is animated vertically upwards.<br>• Heading sizes (40px) vs body (36px) maintain clear text hierarchy in black and white. |

---

### Screen 6: Level Select Screen

| Screenshots                                                                                                                                                | Design & Technical Notes                                                                                                                                                                                                                                                                                                                                                                 |
| :--------------------------------------------------------------------------------------------------------------------------------------------------------- | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Level Select Color](Docs/06-levelSelect-colored.png)<br><br>**Greyscale:**<br>![Level Select Greyscale](Docs/06-levelSelect-greyscale.png) | • Shows all 6 levels in a 2 column grid with level details in cards.<br>• Locked levels have dark overlay with lock icon, while newly unlocked levels (which are not played yet) show NEW badge.<br>• Tapping locked card gives quick toast message without opening screen.<br>• There is a sharp contrast between Locked and unlocked cards in greyscale, and star counts remain clear. |

---

### Screen 7: Loadout Screen

| Screenshots                                                                                                                              | Design & Technical Notes                                                                                                                                                                                                                                                                                                    |
| :--------------------------------------------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Loadout Color](Docs/07-loadout-colored.png)<br><br>**Greyscale:**<br>![Loadout Greyscale](Docs/07-loadout-greyscale.png) | • Upper section shows large orb preview with effect description along with selectable orb tiles.<br>• Tapping booster toggles state between ON and OFF and if no booster is present then tapping routes to the shop.<br>• In greyscale, thick selection border clearly distinguishes active orb without needing color cues. |

---

### Screen 8: Gameplay HUD Screen

| Screenshots                                                                                                                                            | Design & Technical Notes                                                                                                                                                                                                                                                                                  |
| :----------------------------------------------------------------------------------------------------------------------------------------------------- | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Game Screen Color](Docs/08-gameScreen-colored.png)<br><br>**Greyscale:**<br>![Game Screen Greyscale](Docs/08-gameScreen-greyscale.png) | • A simulated gameplay screen with "Game will be played here" message and 2 buttons to simulate the completion or failure of level.<br>• A pause button is in top right corner.<br>• Greyscale check confirms COMPLETE LEVEL has solid bright fill while FAIL LEVEL is outline, creating distinct values. |

---

### Screen 9: Pause Screen

| Screenshots                                                                                                                      | Design & Technical Notes                                                                                                                                                                                                                                                                                                               |
| :------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Pause Color](Docs/09-pause-colored.png)<br><br>**Greyscale:**<br>![Pause Greyscale](Docs/09-pause-greyscale.png) | • A modal is shown own pause with back overlay.<br>• RESUME is biggest button, secondary options are RESTART and SETTINGS.<br>• QUIT TO MENU requires second tap confirmation ("TAP AGAIN TO QUIT") so progress is not lost.<br>• In greyscale, RESUME has brightest fill while QUIT has outline, maintaining proper visual hierarchy. |

---

### Screen 10: Results Screen

| Screenshots                                                                                                                          | Design & Technical Notes                                                                                                                                                         |
| :----------------------------------------------------------------------------------------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Color:**<br>![Result Color](Docs/10-result-colored.png)<br><br>**Greyscale:**<br>![Result Greyscale](Docs/10-result-greyscale.png) | • Shows summary of completed run with earned stars, and awarded gems.<br>• Greyscale test shows earned stars filled with bright white while unearned stars remain dark outlines. |
