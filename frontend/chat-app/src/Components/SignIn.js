import {useState} from "react";
import {Alert, Button, Col, Container, Form, Row} from "react-bootstrap";

function SignIn({ onLogin, message }) {
    const [data, setData] = useState({
        name: '',
        password: ''
    });

    const handleInputChange = (event) => {
        setData({
            ...data,
            [event.target.name] : event.target.value
        });
    }

    const sendData = (event) => {
        event.preventDefault();
        onLogin(data);
    }

    return (
        <Container>
            <Row className={"justify-content-md-center"}>
                <Col xs lg="6">
                    <Form className={"g-3 p-5"} onSubmit={sendData}>
                        <Form.Group className="mb-3" controlId="formBasicEmail">
                            <Form.Label>User Name</Form.Label>
                            <Form.Control name="name" type="text" placeholder="Enter username" onChange={handleInputChange} />
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicPassword">
                            <Form.Label>Password</Form.Label>
                            <Form.Control name="password" type="password" placeholder="Password" onChange={handleInputChange} />
                        </Form.Group>
                        {
                            message && <Alert variant="danger">
                                <p>{ message }</p>
                            </Alert>
                        }
                        <Button variant="primary" type="submit">
                            Login
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default SignIn;