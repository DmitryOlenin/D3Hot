using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3Hot
{
    public class Class_lang
    {
        public string
            lb_trig1, lb_trig2, lb_trig3, lb_trig4,
            lb_key1, lb_key2, lb_key3, lb_key4,
            lb_tmr_sec, lb_prof,
            lb_about, lb_area, lb_stop, lb_auth, lang_sec,
            tt_start, tt_stop
            ;


        public void Lang_rus()
        {
            lb_trig1 = "Триггер 1";
            lb_trig2 = "Триггер 2";
            lb_trig3 = "Триггер 3";
            lb_trig4 = "Триггер 4";

            lb_key1 = "Клавиша 1";
            lb_key2 = "Клавиша 2";
            lb_key3 = "Клавиша 3";
            lb_key4 = "Клавиша 4";

            lb_tmr_sec = "Пауза..мс";

            lb_about = "Задержка в миллисекундах (1s = 1000ms)";
            lb_area = "Область действия";
            lb_stop = "Приостановка";
            lb_auth = "Автор: Dmitry Olenin";
            lang_sec = "сек.";
            lb_prof = "Профиль";

            tt_start = "F11 для запуска";
            tt_stop = "F11 для остановки";
        }
        public void Lang_eng()
        {
            lb_trig1 = "Trigger 1";
            lb_trig2 = "Trigger 2";
            lb_trig3 = "Trigger 3";
            lb_trig4 = "Trigger 4";

            lb_key1 = "Key 1";
            lb_key2 = "Key 2";
            lb_key3 = "Key 3";
            lb_key4 = "Key 4";

            lb_tmr_sec = "Delay..ms";

            lb_about = "Delay in milleseconds (1s = 1000ms)";
            lb_area = "Using area";
            lb_stop = "Pause";
            lb_auth = "Author: Dmitry Olenin";
            lang_sec = "sec.";
            lb_prof = "Profile";

            tt_start = "F11 to start";
            tt_stop = "F11 to stop";
        }

    }
}
