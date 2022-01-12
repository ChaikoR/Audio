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

                const soundClips = document.querySelector('#sound-clip');
                
                //const audio = new Audio();

                
                
                const clipContainer = document.createElement('article');
                const audio = document.createElement('audio');
                audio.setAttribute("controls", "");
                

                //const deleteButton = document.createElement('button');
                clipContainer.classList.add('clip');
                clipContainer.classList.add('plaer');
                clipContainer.setAttribute('id', 'plaer');

                //deleteButton.textContent = 'Delete';
               // deleteButton.className = 'delete';
                
                soundClips.append(audio);
               // soundClips.append(deleteButton);

                clipContainer.appendChild(audio);

               // clipContainer.appendChild(deleteButton);
                soundClips.appendChild(clipContainer);
                

                // Combine the audio chunks into a blob, then point the empty audio clip to that blob.
                const blob = new Blob(chunks,
                    {
                        "type": "audio/ogg; codecs=opus"
                    });
                audio.src = window.URL.createObjectURL(blob);

                document.getElementById('safeFile').value = 1;

                ////добавляем скрытое поле в форму
                //const inputTxt = document.createElement('input');
                //inputTxt.setAttribute("type", "hidden");
                //inputTxt.setAttribute("id", "BinaryData");
                //inputTxt.setAttribute("name", "BinaryData");
                //inputTxt.setAttribute("value", window.URL.createObjectURL(blob));
                //soundClips.append(inputTxt);

                //Отправляем файл на API
                var filename = "sound";
                let fd = new FormData();

                fd.append("file", blob, filename);
                var xhr = new XMLHttpRequest();
                //xhr.addEventListener("load", transferComplete);
                //xhr.addEventListener("error", transferFailed)
                //xhr.addEventListener("abort", transferFailed)
                xhr.open("POST", "https://localhost:7029/api/Messages/Save", true);
                xhr.send(fd);


                //var fd=new FormData();
                //fd.append("audio_data",blob, "filename.wav");
                //xhr.open("POST","upload.php",true);
                //xhr.send(fd);






                // Очищаем буфер для новых записей.
                chunks = [];

                //deleteButton.onclick = function (event) {
                    
                //    let evtTgt = event.target;
                //    evtTgt.parentNode.parentNode.removeChild(evtTgt.parentNode);
                //    document.getElementById("buttons").style.display = "block";
                //    document.getElementById('safeFile').value = 0;
                    
                //}

            };

            window.SoundJSMethods = {

                //startRecording: function () {
                //    mediaRecorder.start();
                //    recording = true;
                //},

                //stopRecording: function (element) {
                //    mediaRecorder.stop();
                //    recording = false;
                //    document.getElementById("buttons").style.display = "none";
                //},

                //delRecording: function () {
                //    document.getElementById('plaer').remove();
                //    document.getElementById("buttons").style.display = "block";
                //},

                startRecording: function () {
                    mediaRecorder.start();
                    recording = true;
                },

                stopRecording: function (element) {
                    mediaRecorder.stop();
                    recording = false;
                    document.getElementById("buttons").style.display = "none";
                    document.getElementById("delRecord").style.display = "block";
                },

                delRecording: function () {
                    if (document.getElementsByClassName('plaer').length > 0) {
                        document.getElementById('plaer').remove();
                        //document.getElementById('plaer').style.display = "none";
                    }

                    //if (document.getElementsByClassName('plaer').length > 0) { document.getElementById('plaer').remove() }

                    document.getElementById("buttons").style.display = "block";
                    document.getElementById("delRecord").style.display = "none";
                },
                saveMessege: function () {
                    if (document.getElementById('plaer') != null) {
                        //document.getElementById('plaer').remove();
                        document.getElementById('plaer').style.display = "none";
                    }
                    document.getElementById("buttons").style.display = "block";
                    document.getElementById("delRecord").style.display = "none";
                },
                openModalAdd: function () {
                    //if (document.getElementById('plaer')!=null) {
                    //    document.getElementById('plaer').style.display = "none";
                    //}

                    if (document.getElementsByClassName('plaer').length > 0) { document.getElementById('plaer').remove() }
                    
                    document.getElementById("buttons").style.display = "block";
                    document.getElementById("delRecord").style.display = "none";
                },
                openModalEdit: function () {
                    if (document.getElementsByClassName('plaer').length > 0) {
                        document.getElementById('plaer').style.display = "none";
                    }
                    document.getElementById("buttons").style.display = "block";
                    document.getElementById("delRecord").style.display = "none";
                },
                openModalEditAudio: function () {
                    if (document.getElementsByClassName('plaer').length >0) {
                        document.getElementById('plaer').style.display = "block";
                    }
                    
                    document.getElementById("buttons").style.display = "none";
                    document.getElementById("delRecord").style.display = "block";
                },
                close: function () {
                    if (document.getElementById('plaer') != null) {
                        //document.getElementById('plaer').remove();
                        document.getElementById('plaer').style.display = "none";
                    }
                    if (document.getElementsByClassName('plaer').length >= 2) {
                        document.getElementById('plaer').remove();
                        alert('del id-plaeer');
                    }
                    document.getElementById("buttons").style.display = "block";
                    document.getElementById("delRecord").style.display = "none";
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