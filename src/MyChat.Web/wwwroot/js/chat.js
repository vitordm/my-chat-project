(function () {
    "use strict";
    const connection = new signalR.HubConnectionBuilder().withUrl("/chat").build();

    let allowToSend = false;

    const scrollDownMessages = () => {
        const divChatMessages = $('#chat-messages-div');
        divChatMessages.animate({ scrollTop: divChatMessages[0].scrollHeight }, 'slow');
    }

    const createMessage = () => {
        const formArray = $('#formSendChat').serializeArray();
        const objForm = {};
        formArray.forEach(v => {
            objForm[v.name] = v.value;
        });

        return objForm;
    }

    const formatMessage = (chatMessage) => {

        const msg = chatMessage.message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
        const formattedMessage = `<li class="item-message mt-1 px-2">
                            <div class="d-flex flex-column">
                                <span><i class="fas fa-user-astronaut"></i> <b>${chatMessage.userName}</b> <i>says:</i> ${msg}</span>
                                <span class="text-muted text-sm-right font-10px">${chatMessage.createdAt}</span>
                            </div>
                        </li>`;
        return formattedMessage;


    };

    connection.on("ReceiveMessage", (chatMessage) => {
        console.log(chatMessage);

        const encodedMsg = formatMessage(chatMessage);
        //document.getElementById("chat-messages-list").append(encodedMsg);
        $('#chat-messages-list').append(encodedMsg);
        scrollDownMessages();
    });

    connection.start()
        .then(() => {
            console.info("SignalR is running!");
            allowToSend = true;
        }).catch((err) => {
            return console.error(err.toString());
        });


    $('#formSendChat').on('submit', (event) => {
        console.log('submit');

        event.preventDefault();

        if (!allowToSend)
            return false;
        const message = createMessage();

        console.log("Pre-Send => ", message);

        connection.invoke("SendMessage", message)
            .then(() => {
                $('#message').val('');
            })
            .catch((err) => {
                return console.error(err.toString());
            });

        return false;
    })

})();