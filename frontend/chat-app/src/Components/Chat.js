import { Button, Col, Container, Form, Row} from "react-bootstrap";
import {useState} from "react";


function Chat({ onChatSend }){

    const [data, setData] = useState({
        chat_room: 'general',
        text: ''
    });

    const handleInputChange = (event) => {
        setData({
            ...data,
            [event.target.name] : event.target.value
        });
    }

    const sendData = (event) => {
        event.preventDefault();
        onChatSend(data);
    }
    return (
        <Container>
            <Row className={"justify-content-md-center"}>
                <Col xs lg="6">
                    <Form.Label>Chat:</Form.Label>

                    <Form className={"g-3 p-5"} onSubmit={sendData}>
                        <Form.Group className="mb-3" controlId="formBasicEmail">
                            <Form.Label>Text</Form.Label>
                            <Form.Control name="text" type="text" placeholder="Enter message" onChange={handleInputChange} />
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Send
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default Chat;