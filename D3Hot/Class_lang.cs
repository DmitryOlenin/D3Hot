
namespace D3Hot
{
    public class Class_lang
    {
        public string
            lb_trig1, lb_trig2, lb_trig3, lb_trig4, lb_trig5, lb_trig6,
            lb_key1, lb_key2, lb_key3, lb_key4, lb_key5, lb_key6,
            lb_tmr_sec, lb_prof, lb_prof_save, lb_rand,
            lb_about, lb_area, lb_proc, lb_stop, lb_auth, lang_sec,
            tt_start, tt_stop, tt_key, tt_delay, tt_trig,
            lb_startstop, lb_tp, lb_tpdelay, tb_prof_name,
            chb_tray, chb_mult, chb_users, chb_proconly, b_opt, lb_key_delay, lb_key_delay_ms,
            lb_hold, lb_hold_hot, lb_hot_prof, lb_hold_trig, lb_hold_delay,
            chb_hold, chb_mpress,
            lb_map, lb_mapdelay
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
            lb_stop = "Тип паузы";
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
            lb_hold_delay = "Не выставлена ни одна пауза для триггеров.";

            chb_hold = "Процесс/Зажатие";
            chb_mpress = "Мультинажатие";
            lb_rand = "Случайная задержка";
            lb_hot_prof = "Хоткеи профилей";
            tb_prof_name = "Наименование профиля";
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
            lb_stop = "Pause type";
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
            chb_hold = "Process/Holding";
            chb_mpress = "Multi keypress";
            lb_rand = "Random delay";
            lb_hot_prof = "Profiles hotkeys";
            tb_prof_name = "Profile name";
        }

    }
}
