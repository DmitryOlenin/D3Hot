
namespace D3Hot
{
    public class ClassLang
    {
        public string
            LbTrig1, LbTrig2, LbTrig3, LbTrig4, LbTrig5, LbTrig6,
            LbKey1, LbKey2, LbKey3, LbKey4, LbKey5, LbKey6,
            LbTmrSec, LbProf, LbProfSave, LbRand, LbCoold,
            LbArea, LbProc, LbPressType, LbReturndelay, LbAuth, LangSec, LbHelp,
            TtStart, TtStop, TtKey, TtDelay, TtTrig,
            LbStartstop, LbTp, LbTpdelay, TbProfName,
            ChbTray, ChbMult, ChbUsers, BOpt, LbKeyDelay, LbKeyDelayMs,
            LbHold, LbHoldHot, LbHotProf, LbHoldTrig, LbHoldDelay, LbHoldKey, LbHoldCross,
            ChbHold, ChbMpress, ChbLog, ChbVerCheck,
            LbMap, LbMapdelay,
            CbKeysChoose, LbKeyPrevUsed, LbKeyPrevErr, LbKeyDesc,
            Download, NewVer, NoNew, VerErrNover, VerErrOpen, VerCap,
            CbTmrDef, CbTmrCdr, CbTmrHold, CbTmrCdrtime,

            TtProc, TtCbPressType, TtProfname, TtOpt, TtProf, TtOpStart, TtOpEnter, TtOpTele, TtOpTelePause,
            TtOpMap, TtOpMapPause, TtOpAlt, TtOpAltPause, TtOpRand, TtOpCdr, TtOpProf,
            TtOpMult, TtOpTray, TtOpAdv, TtOpMultpress, TtOpSaveload, TtOpPers, TtOpNewver,
            TtForum, TtNum, TtCaps, TtScroll, TtLang, TtHold, TtMode, TtHelp, TtOpLog,

            TrigSettings
            ;


        public void Lang_rus()
        {
            LbTrig1 = "Триггер 1";
            LbTrig2 = "Триггер 2";
            LbTrig3 = "Триггер 3";
            LbTrig4 = "Триггер 4";
            LbTrig5 = "Триггер 5";
            LbTrig6 = "Триггер 6";

            LbKey1 = "Клавиша 1";
            LbKey2 = "Клавиша 2";
            LbKey3 = "Клавиша 3";
            LbKey4 = "Клавиша 4";
            LbKey5 = "Клавиша 5";
            LbKey6 = "Клавиша 6";

            LbTmrSec = "Пауза..мс";
            LbStartstop = "Хоткей старт";
            LbTp = "Хоткей тп";
            LbTpdelay = "Пауза тп";
            LbMap = "Хоткей карта";
            LbMapdelay = "Пауза карта";

            TtDelay = "Задержка в миллисекундах (1s = 1000ms)";
            LbArea = "Область действия";
            LbProc = "Процесс";
            LbPressType = "Тип прожатия";
            LbReturndelay = "Пауза Enter";
            LbAuth = "Автор: Dmitry Olenin";
            LbHelp = "Помощь";
            LangSec = "сек.";
            LbProf = "Профиль";
            LbProfSave = "Профиль из файла";

            TtStart = "Запуск";
            TtStop = "Остановка";
            TtKey = "Выберите кнопку, которая будет прожиматься";
            TtTrig = "Выберите кнопку для активации прожима";
            TtHold = "Отмечено, если триггер необходимо зажимать для активации"; 
            TtProc = "Выберите процесс для работы";
            TtCbPressType = "Выберите режим прожатия кнопок";
            TtMode = "Выберите режим прожатия кнопок"; 
            TtProfname = "Название текущего профиля";
            TtForum = "Ссылка на форум с текущей версией";
            TtNum = "Состояние NumLock";
            TtCaps = "Состояние CapsLock";
            TtScroll = "Состояние ScrollLock";
            TtLang = "Переключение языка программы";
            TtOpt = "Настройки программы";
            TtProf = "Выберите профиль";
            TtOpStart = "Выберите кнопку для запуска программы";
            TtOpEnter = "Пауза в прожатии после нажатия Enter в секундах";
            TtOpTele = "Выберите кнопку телепорта в город из D3";
            TtOpTelePause = "Пауза в прожатии после нажатия Телепорта в секундах";
            TtOpMap = "Выберите кнопку открытия карты из D3";
            TtOpMapPause = "Пауза в прожатии после нажатия Карты в секундах";
            TtOpAlt = "Выберите кнопку, которая будет вызывать паузу в прожатии";
            TtOpAltPause = "Пауза в прожатии после нажатия произвольной кнопки";
            TtOpRand = "Случайная задержка между прожатиями, для параноиков";
            TtOpCdr = "Настройка задержки куладуна. Варьируется точность/задвоенность прожатия";
            TtOpProf = "Выберите кнопки для быстрого переключения трёх профилей";
            TtOpMult = "Позволять программе запускаться в нескольких экземплярах";
            TtOpTray = "Позволять программе сворачиваться в трей";
            TtOpAdv = "Включение дополнительных функций с необходимостью выбора процесса игры";
            TtOpMultpress = "Многократное прожатие одной клавиши";
            TtOpSaveload = "Позволяет сохрнять и загружать профили из файлов (вместо стандартного списка)";
            TtOpPers = "Возможность подкладывать ico, jpg, txt файлы для персонализации вида программы";
            TtOpNewver = "Автопроверка новой версии при запуске (или разовая, при щелчке)";
            TtHelp = "Документация по программе";
            TtOpLog = "Логирование некоторых событий в файл";

            ChbTray = "Сворачивать в трей";
            ChbMult = "Мультизапуск";
            ChbUsers = "Персонализация";
            //ChbProconly = "Только процесс";
            BOpt = "Настройки";

            LbKeyDelay = "Клавиша с паузой";
            LbKeyDelayMs = "Задержка клавиши";

            LbHold = "Выберите процесс для запуска.";
            LbHoldHot = "Пересечение кнопок глобальных хоткеев.";
            LbHoldTrig = "Не выбрано ни одного триггера для активации.";
            LbHoldDelay = "Не выставленаы паузы для триггеров.";
            LbHoldKey = "Не выбраны клавиши для триггеров.";
            LbHoldCross = "Пересечение хоткеев кнопок и триггеров.";

            ChbHold = "Процесс/Зажатие";
            ChbMpress = "Мультинажатие";
            ChbLog = "Логирование";
            LbRand = "Случайная задержка";
            LbCoold = "Задержка кулдауна";
            LbHotProf = "Хоткеи профилей";
            TbProfName = "Наименование профиля";

            CbKeysChoose = "Выбор...";
            LbKeyDesc = "Клавиша нажата:";
            LbKeyPrevUsed = "Клавиша занята!";
            LbKeyPrevErr = "Недопустимая клавиша!";

            Download = "Скачать?";
            NewVer = "Новая версия: ";
            ChbVerCheck = "Проверка версии";
            NoNew = "Новой версии нет!";
            VerErrNover = "Не удаётся распознать номер версии!";
            VerErrOpen = "Не могу открыть ";
            VerCap = "Обновление версии";

            CbTmrDef = "Стандарт";
            CbTmrCdr = "Кулдаун";
            CbTmrHold = "Зажатие";
            CbTmrCdrtime = "Кдр+сек";
            
            TrigSettings = "Настройка работы триггера";
        }
        public void Lang_eng()
        {
            LbTrig1 = "Trigger 1";
            LbTrig2 = "Trigger 2";
            LbTrig3 = "Trigger 3";
            LbTrig4 = "Trigger 4";
            LbTrig5 = "Trigger 5";
            LbTrig6 = "Trigger 6";

            LbKey1 = "Key 1";
            LbKey2 = "Key 2";
            LbKey3 = "Key 3";
            LbKey4 = "Key 4";
            LbKey5 = "Key 5";
            LbKey6 = "Key 6";

            LbTmrSec = "Delay..ms";
            LbStartstop = "Start key";
            LbTp = "Teleport key";
            LbTpdelay = "Teleport delay";
            LbMap = "Map key";
            LbMapdelay = "Map delay";

            TtDelay = "Delay in milliseconds (1s = 1000ms)";
            LbArea = "Using area";
            LbProc = "Using process";
            LbPressType = "Key pressing type";
            LbReturndelay = "Pause Enter";
            LbAuth = "Author: Dmitry Olenin";
            LbHelp = "Help";
            LangSec = "sec.";
            LbProf = "Profile";
            LbProfSave = "Profile from file";

            TtStart = "Start";
            TtStop = "Stop";
            TtKey = "Choose a key to press";
            TtTrig = "Choose a key to activate process";
            TtHold = "Checked, if trigger need to be holded for working"; 
            TtProc = "Choose a process for working";
            TtCbPressType = "Choose a type of keypressing";
            TtMode = "Choose keypressing mode"; 
            TtProfname = "Name of current profile";
            TtForum = "Link to forum with current version of program";
            TtNum = "NumLock state";
            TtCaps = "CapsLock state";
            TtScroll = "ScrollLock state";
            TtLang = "Switching program language";
            TtOpt = "Options";
            TtProf = "Choose a profile";
            TtOpStart = "Choose Start-key for program";
            TtOpEnter = "Pause in a seconds after Enter (chat)";
            TtOpTele = "Choose a Teleport key from D3";
            TtOpTelePause = "Pause in working after Teleport-key was pressed";
            TtOpMap = "Choose a Map-key from D3";
            TtOpMapPause = "Pause in working after Map key was pressed";
            TtOpAlt = "Choose Advanced key for delaying (Crusader Horse, by example)";
            TtOpAltPause = "Pause in working after Advanced key was pressed";
            TtOpRand = "Random delay within pressing, for paranoid";
            TtOpCdr = "Cooldown tuning for more accuracy";
            TtOpProf = "Choose a keys for profiles switching";
            TtOpMult = "Allow program to start in many copies";
            TtOpTray = "Allow program to hide in tray";
            TtOpAdv = "Enable advanced functions with process choosing";
            TtOpMultpress = "Enable multipressing of keys (3 times insted of 1)";
            TtOpSaveload = "Enable Save/Load function for profiles";
            TtOpPers = "Enable using of ico, jpg, txt files for personalization";
            TtOpNewver = "Auto version check after start (or instantly after click)";
            TtHelp = "Program documentation";
            TtOpLog = "Logging some events in file";

            ChbTray = "Hide minimized";
            ChbMult = "Multiprocess";
            ChbUsers = "Personalization";
            //ChbProconly = "Process only";
            BOpt = "Settings";

            LbKeyDelay = "Key with delay";
            LbKeyDelayMs = "Key delay time";

            LbHold = "Choose a process for start.";
            LbHoldHot = "Global hotkeys conflict.";
            LbHoldTrig = "There are no triggers.";
            LbHoldDelay = "There are no delays for active triggers.";
            LbHoldKey = "There are no keys for active triggers.";
            LbHoldCross = "Hotkeys triggers/keys conflict.";

            ChbHold = "Process/Holding";
            ChbMpress = "Multi keypress";
            ChbLog = "Logging";
            LbRand = "Random delay";
            LbCoold = "Cooldown delay";
            LbHotProf = "Profiles hotkeys";
            TbProfName = "Profile name";

            CbKeysChoose = "Select...";
            LbKeyDesc = "Key pressed:";
            LbKeyPrevUsed = "Key isn't free!";
            LbKeyPrevErr = "Wrong key!";

            Download = "Download?";
            NewVer = "New version: ";
            ChbVerCheck = "Version check";
            NoNew = "No new version!";
            VerErrNover = "Can't recognize the version number!";
            VerErrOpen = "Can't open ";
            VerCap = "Version check";

            CbTmrDef = "Default";
            CbTmrCdr = "Cooldown";
            CbTmrHold = "Holding";
            CbTmrCdrtime = "Cdr+sec";

            TrigSettings = "Settings of trigger";
        }

    }
}
