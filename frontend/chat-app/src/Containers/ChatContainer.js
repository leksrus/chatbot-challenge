import {useState} from "react";
import SignIn from "../Components/SignIn";
import Chat from "../Components/Chat";


function ChatContainer() {
    const [message, setMessage] = useState(undefined);
    const [token, setToken] = useState(undefined);

    const onLogin = (data) => {
        setMessage(undefined);
        fetch(' http://localhost:8001/api/auth/token', {

            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            },

        }).then(response => response.json()).then(token => setToken(token));
    }

    const onChatSend = (data) => {
        fetch(' http://localhost:8001/api/messages', {

            method: 'POST',
            mode: 'cors',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token.token}`
            },

        }).then(response => response.json()).then(message => console.log(message));
    }

    return (
        <> { token ?
            <Chat onChatSend={onChatSend} /> : <SignIn onLogin={onLogin} message={message}/>
        }
        </>
    );
}

export default ChatContainer;