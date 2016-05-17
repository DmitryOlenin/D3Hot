
namespace D3Hot
{
    public class Class_lang
    {
        public string
            lb_trig1, lb_trig2, lb_trig3, lb_trig4, lb_trig5, lb_trig6,
            lb_key1, lb_key2, lb_key3, lb_key4, lb_key5, lb_key6,
            lb_tmr_sec, lb_prof, lb_prof_save, lb_rand, lb_coold,
            lb_about, lb_area, lb_proc, lb_press_type, lb_returndelay, lb_auth, lang_sec, lb_help,
            tt_start, tt_stop, tt_key, tt_delay, tt_trig,
            lb_startstop, lb_tp, lb_tpdelay, tb_prof_name,
            chb_tray, chb_mult, chb_users, chb_proconly, b_opt, lb_key_delay, lb_key_delay_ms,
            lb_hold, lb_hold_hot, lb_hot_prof, lb_hold_trig, lb_hold_delay, lb_hold_key, lb_hold_cross,
            chb_hold, chb_mpress, chb_log, chb_ver_check,
            lb_map, lb_mapdelay,
            cb_keys_choose, lb_key_prev_used, lb_key_prev_err, lb_key_desc,
            download, new_ver, no_new, ver_err_nover, ver_err_open, ver_cap,
            cb_tmr_def, cb_tmr_cdr, cb_tmr_hold, cb_tmr_cdrtime,

            tt_proc, tt_cb_press_type, tt_profname, tt_opt, tt_prof, tt_op_start, tt_op_enter, tt_op_tele, tt_op_tele_pause,
            tt_op_map, tt_op_map_pause, tt_op_alt, tt_op_alt_pause, tt_op_rand, tt_op_cdr, tt_op_prof,
            tt_op_mult, tt_op_tray, tt_op_adv, tt_op_multpress, tt_op_saveload, tt_op_pers, tt_op_newver,
            tt_forum, tt_num, tt_caps, tt_scroll, tt_lang, tt_hold, tt_mode, tt_help, tt_op_log,

            trig_settings
            ;


        public void Lang_rus()
        {
            lb_trig1 = "Триггер 1";
            lb_trig2 = "Триггер 2";
            lb_trig3 = "Триггер 3";
            lb_trig4 = "Триггер 4";
            lb_trig5 = "Триггер 5";
            lb_trig6 = "Триггер 6";

            lb_key1 = "Клавиша 1";
            lb_key2 = "Клавиша 2";
            lb_key3 = "Клавиша 3";
            lb_key4 = "Клавиша 4";
            lb_key5 = "Клавиша 5";
            lb_key6 = "Клавиша 6";

            lb_tmr_sec = "Пауза..мс";
            lb_startstop = "Хоткей старт";
            lb_tp = "Хоткей тп";
            lb_tpdelay = "Пауза тп";
            lb_map = "Хоткей карта";
            lb_mapdelay = "Пауза карта";

            tt_delay = "Задержка в миллисекундах (1s = 1000ms)";
            lb_area = "Область действия";
            lb_proc = "Процесс";
            lb_press_type = "Тип прожатия";
            lb_returndelay = "Пауза Enter";
            lb_auth = "Автор: Dmitry Olenin";
            lb_help = "Помощь";
            lang_sec = "сек.";
            lb_prof = "Профиль";
            lb_prof_save = "Профиль из файла";

            tt_start = "Запуск";
            tt_stop = "Остановка";
            tt_key = "Выберите кнопку, которая будет прожиматься";
            tt_trig = "Выберите кнопку для активации прожима";
            tt_hold = "Отмечено, если триггер необходимо зажимать для активации"; 
            tt_proc = "Выберите процесс для работы";
            tt_cb_press_type = "Выберите режим прожатия кнопок";
            tt_mode = "Выберите режим прожатия кнопок"; 
            tt_profname = "Название текущего профиля";
            tt_forum = "Ссылка на форум с текущей версией";
            tt_num = "Состояние NumLock";
            tt_caps = "Состояние CapsLock";
            tt_scroll = "Состояние ScrollLock";
            tt_lang = "Переключение языка программы";
            tt_opt = "Настройки программы";
            tt_prof = "Выберите профиль";
            tt_op_start = "Выберите кнопку для запуска программы";
            tt_op_enter = "Пауза в прожатии после нажатия Enter в секундах";
            tt_op_tele = "Выберите кнопку телепорта в город из D3";
            tt_op_tele_pause = "Пауза в прожатии после нажатия Телепорта в секундах";
            tt_op_map = "Выберите кнопку открытия карты из D3";
            tt_op_map_pause = "Пауза в прожатии после нажатия Карты в секундах";
            tt_op_alt = "Выберите кнопку, которая будет вызывать паузу в прожатии";
            tt_op_alt_pause = "Пауза в прожатии после нажатия произвольной кнопки";
            tt_op_rand = "Случайная задержка между прожатиями, для параноиков";
            tt_op_cdr = "Настройка задержки куладуна. Варьируется точность/задвоенность прожатия";
            tt_op_prof = "Выберите кнопки для быстрого переключения трёх профилей";
            tt_op_mult = "Позволять программе запускаться в нескольких экземплярах";
            tt_op_tray = "Позволять программе сворачиваться в трей";
            tt_op_adv = "Включение дополнительных функций с необходимостью выбора процесса игры";
            tt_op_multpress = "Многократное прожатие одной клавиши";
            tt_op_saveload = "Позволяет сохрнять и загружать профили из файлов (вместо стандартного списка)";
            tt_op_pers = "Возможность подкладывать ico, jpg, txt файлы для персонализации вида программы";
            tt_op_newver = "Автопроверка новой версии при запуске (или разовая, при щелчке)";
            tt_help = "Документация по программе";
            tt_op_log = "Логирование некоторых событий в файл";

            chb_tray = "Сворачивать в трей";
            chb_mult = "Мультизапуск";
            chb_users = "Персонализация";
            chb_proconly = "Только процесс";
            b_opt = "Настройки";

            lb_key_delay = "Клавиша с паузой";
            lb_key_delay_ms = "Задержка клавиши";

            lb_hold = "Выберите процесс для запуска.";
            lb_hold_hot = "Пересечение кнопок глобальных хоткеев.";
            lb_hold_trig = "Не выбрано ни одного триггера для активации.";
            lb_hold_delay = "Не выставленаы паузы для триггеров.";
            lb_hold_key = "Не выбраны клавиши для триггеров.";
            lb_hold_cross = "Пересечение хоткеев кнопок и триггеров.";

            chb_hold = "Процесс/Зажатие";
            chb_mpress = "Мультинажатие";
            chb_log = "Логирование";
            lb_rand = "Случайная задержка";
            lb_coold = "Задержка кулдауна";
            lb_hot_prof = "Хоткеи профилей";
            tb_prof_name = "Наименование профиля";

            cb_keys_choose = "Выбор...";
            lb_key_desc = "Клавиша нажата:";
            lb_key_prev_used = "Клавиша занята!";
            lb_key_prev_err = "Недопустимая клавиша!";

            download = "Скачать?";
            new_ver = "Новая версия: ";
            chb_ver_check = "Проверка версии";
            no_new = "Новой версии нет!";
            ver_err_nover = "Не удаётся распознать номер версии!";
            ver_err_open = "Не могу открыть ";
            ver_cap = "Обновление версии";

            cb_tmr_def = "Стандарт";
            cb_tmr_cdr = "Кулдаун";
            cb_tmr_hold = "Зажатие";
            cb_tmr_cdrtime = "Кдр+сек";
            
            trig_settings = "Настройка работы триггера";
        }
        public void Lang_eng()
        {
            lb_trig1 = "Trigger 1";
            lb_trig2 = "Trigger 2";
            lb_trig3 = "Trigger 3";
            lb_trig4 = "Trigger 4";
            lb_trig5 = "Trigger 5";
            lb_trig6 = "Trigger 6";

            lb_key1 = "Key 1";
            lb_key2 = "Key 2";
            lb_key3 = "Key 3";
            lb_key4 = "Key 4";
            lb_key5 = "Key 5";
            lb_key6 = "Key 6";

            lb_tmr_sec = "Delay..ms";
            lb_startstop = "Start key";
            lb_tp = "Teleport key";
            lb_tpdelay = "Teleport delay";
            lb_map = "Map key";
            lb_mapdelay = "Map delay";

            tt_delay = "Delay in milliseconds (1s = 1000ms)";
            lb_area = "Using area";
            lb_proc = "Using process";
            lb_press_type = "Key pressing type";
            lb_returndelay = "Pause Enter";
            lb_auth = "Author: Dmitry Olenin";
            lb_help = "Help";
            lang_sec = "sec.";
            lb_prof = "Profile";
            lb_prof_save = "Profile from file";

            tt_start = "Start";
            tt_stop = "Stop";
            tt_key = "Choose a key to press";
            tt_trig = "Choose a key to activate process";
            tt_hold = "Checked, if trigger need to be holded for working"; 
            tt_proc = "Choose a process for working";
            tt_cb_press_type = "Choose a type of keypressing";
            tt_mode = "Choose keypressing mode"; 
            tt_profname = "Name of current profile";
            tt_forum = "Link to forum with current version of program";
            tt_num = "NumLock state";
            tt_caps = "CapsLock state";
            tt_scroll = "ScrollLock state";
            tt_lang = "Switching program language";
            tt_opt = "Options";
            tt_prof = "Choose a profile";
            tt_op_start = "Choose Start-key for program";
            tt_op_enter = "Pause in a seconds after Enter (chat)";
            tt_op_tele = "Choose a Teleport key from D3";
            tt_op_tele_pause = "Pause in working after Teleport-key was pressed";
            tt_op_map = "Choose a Map-key from D3";
            tt_op_map_pause = "Pause in working after Map key was pressed";
            tt_op_alt = "Choose Advanced key for delaying (Crusader Horse, by example)";
            tt_op_alt_pause = "Pause in working after Advanced key was pressed";
            tt_op_rand = "Random delay within pressing, for paranoid";
            tt_op_cdr = "Cooldown tuning for more accuracy";
            tt_op_prof = "Choose a keys for profiles switching";
            tt_op_mult = "Allow program to start in many copies";
            tt_op_tray = "Allow program to hide in tray";
            tt_op_adv = "Enable advanced functions with process choosing";
            tt_op_multpress = "Enable multipressing of keys (3 times insted of 1)";
            tt_op_saveload = "Enable Save/Load function for profiles";
            tt_op_pers = "Enable using of ico, jpg, txt files for personalization";
            tt_op_newver = "Auto version check after start (or instantly after click)";
            tt_help = "Program documentation";
            tt_op_log = "Logging some events in file";

            chb_tray = "Hide minimized";
            chb_mult = "Multiprocess";
            chb_users = "Personalization";
            chb_proconly = "Process only";
            b_opt = "Settings";

            lb_key_delay = "Key with delay";
            lb_key_delay_ms = "Key delay time";

            lb_hold = "Choose a process for start.";
            lb_hold_hot = "Global hotkeys conflict.";
            lb_hold_trig = "There are no triggers.";
            lb_hold_delay = "There are no delays for active triggers.";
            lb_hold_key = "There are no keys for active triggers.";
            lb_hold_cross = "Hotkeys triggers/keys conflict.";

            chb_hold = "Process/Holding";
            chb_mpress = "Multi keypress";
            chb_log = "Logging";
            lb_rand = "Random delay";
            lb_coold = "Cooldown delay";
            lb_hot_prof = "Profiles hotkeys";
            tb_prof_name = "Profile name";

            cb_keys_choose = "Select...";
            lb_key_desc = "Key pressed:";
            lb_key_prev_used = "Key isn't free!";
            lb_key_prev_err = "Wrong key!";

            download = "Download?";
            new_ver = "New version: ";
            chb_ver_check = "Version check";
            no_new = "No new version!";
            ver_err_nover = "Can't recognize the version number!";
            ver_err_open = "Can't open ";
            ver_cap = "Version check";

            cb_tmr_def = "Default";
            cb_tmr_cdr = "Cooldown";
            cb_tmr_hold = "Holding";
            cb_tmr_cdrtime = "Cdr+sec";

            trig_settings = "Settings of trigger";
        }

    }
}
