window.global = {
    openModal: function (popupId) {
        popupId = "#" + popupId;
        $(popupId).modal("show");
    },
    closeModal: function (popupId) {
        popupId = "#" + popupId;
        $(popupId).modal("hide");
    },
};

window.AudioContext = window.AudioContext || window.webkitAudioContext;
// Устанавливаем AudioContext.
const audioCtx = new AudioContext();

// Переменная верхнего уровня отслеживает, записываем мы или нет
let recording = false;

// Запрос у пользователя доступ к микрофону.
if (navigator.mediaDevices) {
    navigator.mediaDevices.getUserMedia(
        {
            "audio": true
        }).then((stream) => {

            // Создаем экземпляр медиа-рекордера
            const mediaRecorder = new MediaRecorder(stream);

            // Создаем буфер для хранения входящих данных.
            let chunks = [];
            mediaRecorder.ondataavailable = (event) => {
                chunks.push(event.data);
            }

            // При остановки записи, создаем пустой аудиоклип.
            mediaRecorder.onstop = (event) => {
                const audio = new Audio();
                audio.setAttribute("controls", "");
                $("#sound-clip").append(audio);
                $("#sound-clip").append("<br />");

                // Combine the audio chunks into a blob, then point the empty audio clip to that blob.
                const blob = new Blob(chunks,
                    {
                        "type": "audio/ogg; codecs=opus"
                    });
                audio.src = window.URL.createObjectURL(blob);

                //Отправляем файл на API
                var filename = "sound";
                let fd = new FormData();
                fd.append("file", blob, filename);
                var xhr = new XMLHttpRequest();
                //xhr.addEventListener("load", transferComplete);
                //xhr.addEventListener("error", transferFailed)
                //xhr.addEventListener("abort", transferFailed)
                xhr.open("POST", "https://localhost:7029/api/Messages/Save/1", true);
                xhr.send(fd);


                //var fd=new FormData();
                //fd.append("audio_data",blob, "filename.wav");
                //xhr.open("POST","upload.php",true);
                //xhr.send(fd);






                // Очищаем буфер для новых записей.
                chunks = [];
            };

            window.SoundJSMethods = {

                startRecording: function () {
                    mediaRecorder.start();
                    recording = true;
                },

                stopRecording: function (element) {
                    mediaRecorder.stop();
                    recording = false;
                },


            };
            // Настроить обработчик событий для кнопки «Запись».
            //$("#record").on("click", () => {
            //    if (recording) {
            //        mediaRecorder.stop();
            //        recording = false;
            //        $("#record").html("Record");
            //    }
            //    else {
            //        mediaRecorder.start();
            //        recording = true;
            //        $("#record").html("Stop");
            //    }
            //});

        }).catch((err) => {
            // Бросать оповещение, когда браузер не может получить доступ к микрофону..
            alert("О, нет! Ваш браузер не может получить доступ к микрофону вашего компьютера.");
        });
}
else {
    // Выдавать предупреждение, когда браузер не может получить доступ к каким-либо мультимедийным устройствам.
    alert("О, нет! Ваш браузер не может получить доступ к микрофону вашего компьютера. Пожалуйста, обновите ваш браузер.");
}