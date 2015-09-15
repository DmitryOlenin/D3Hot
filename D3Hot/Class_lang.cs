
namespace D3Hot
{
    public class Class_lang
    {
        public string
            lb_trig1, lb_trig2, lb_trig3, lb_trig4, lb_trig5, lb_trig6,
            lb_key1, lb_key2, lb_key3, lb_key4, lb_key5, lb_key6,
            lb_tmr_sec, lb_prof, lb_prof_save, lb_rand, lb_coold,
            lb_about, lb_area, lb_proc, lb_returndelay, lb_auth, lang_sec,
            tt_start, tt_stop, tt_key, tt_delay, tt_trig,
            lb_startstop, lb_tp, lb_tpdelay, tb_prof_name,
            chb_tray, chb_mult, chb_users, chb_proconly, b_opt, lb_key_delay, lb_key_delay_ms,
            lb_hold, lb_hold_hot, lb_hot_prof, lb_hold_trig, lb_hold_delay, lb_hold_key, lb_hold_cross,
            chb_hold, chb_mpress, chb_ver_check,
            lb_map, lb_mapdelay,
            cb_keys_choose, lb_key_prev_used, lb_key_prev_err, lb_key_desc,
            download, new_ver, no_new, ver_err_nover, ver_err_open, ver_cap,
            cb_tmr1, cb_tmr2, cb_tmr3
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
            lb_returndelay = "Пауза Enter";
            lb_auth = "Автор: Dmitry Olenin";
            lang_sec = "сек.";
            lb_prof = "Профиль";
            lb_prof_save = "Профиль из файла";

            tt_start = "Запуск";
            tt_stop = "Остановка";
            tt_key = "Отметьте для зажатия";
            tt_trig = "Выберите кнопку для активации прожима";

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

            cb_tmr1 = "Стандарт";
            cb_tmr2 = "Кулдаун";
            cb_tmr3 = "Зажатие";
            

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

            tt_delay = "Delay in milleseconds (1s = 1000ms)";
            lb_area = "Using area";
            lb_proc = "Using process";
            lb_returndelay = "Pause Enter";
            lb_auth = "Author: Dmitry Olenin";
            lang_sec = "sec.";
            lb_prof = "Profile";
            lb_prof_save = "Profile from file";

            tt_start = "Start";
            tt_stop = "Stop";
            tt_key = "Check for holding";
            tt_trig = "Choose a key to activate process";

            chb_tray = "Hide minimized";
            chb_mult = "Multiprocess";
            chb_users = "Personalisation";
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

            cb_tmr1 = "Default";
            cb_tmr2 = "Cooldown";
            cb_tmr3 = "Holding";
        }

    }
}
