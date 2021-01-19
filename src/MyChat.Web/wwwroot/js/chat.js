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

    const changeConnectionStatus = (status) => {
        const obj = $('#text-status');
        switch (status) {
            case 'ON':
                obj.html(`<span id="text-status">
                        <i class="fas fa-circle text-success"></i>
                        Connected
                    </span>`);
                break;
            case 'OFF':
                obj.html(`<span id="text-status">
                        <i class="fas fa-circle text-danger"></i>
                        Connected
                    </span>`);
                break;
            case 'WAITING':
                obj.html(`<span id="text-status">
                        <i class="fas fa-circle text-warning"></i>
                        Connected
                    </span>`);
                break;
        }
    };

    const bindClickListGroups = () => {
        $('.link-groups').on('click', (event) => {
            event.preventDefault();

            if (!allowToSend)
                return;

            const currentGroup = $('#chatGroup').val();

            if (currentGroup) {
                connection.invoke("LeaveGroup", currentGroup)
                    .then(() => { }
                    )
                    .catch((err) => {
                        return console.error(err.toString());
                    });
            }

            const objGroupList = $(event.target);
            const groupValue = objGroupList.attr('data-value');

            if (groupValue) {
                connection.invoke("JoinGroup", groupValue)
                    .then(() => {

                        $('#chatGroup').val(groupValue);

                        $('.badge-secondary').removeClass('badge-secondary');

                        objGroupList.addClass('badge-secondary');

                        $('#chat-messages-list').html('');
                    })
                    .catch((err) => {
                        return console.error(err.toString());
                    });
            }

        });
    }

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
            changeConnectionStatus('ON');
        }).catch((err) => {
            return console.error(err.toString());
        });

    connection.onclose(() => {
        changeConnectionStatus('OFF');
        allowToSend = false;
    });

    connection.onreconnecting(() => {
        changeConnectionStatus('WAITING');
    });

    connection.onreconnected(() => {
        changeConnectionStatus('ON');
    });


    $('#formSendChat').on('submit', (event) => {
        console.log('submit');

        event.preventDefault();

        if (!allowToSend)
            return false;
        const message = createMessage();

        console.log("Pre-Send => ", message);

        if (message.chatGroup) {
            connection.invoke("SendMessageToGroup", message)
                .then(() => {
                    $('#message').val('');
                })
                .catch((err) => {
                    return console.error(err.toString());
                });
        } else {
            connection.invoke("SendMessage", message)
                .then(() => {
                    $('#message').val('');
                })
                .catch((err) => {
                    return console.error(err.toString());
                });
        }



        return false;
    })

    $('#btn-join-group').on('click', (event) => {
        event.preventDefault();

        const newGroupName = $('#new-group-name').val();
        const li = `<li class="mt-1"> <a class="link-groups" data-value="${newGroupName}" href="javascript:void(0)">${newGroupName}</a> </li>`;

        $('#groups').append(li);
        $('.link-groups').off('click');

        $('#new-group-name').val('');

        bindClickListGroups();
    });

    bindClickListGroups();

    scrollDownMessages();

})();