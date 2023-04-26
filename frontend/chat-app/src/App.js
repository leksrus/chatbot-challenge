import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import ChatContainer from "./Containers/ChatContainer";
import { BrowserRouter } from 'react-router-dom'

function App() {
  return (
      <BrowserRouter basename={process.env.PUBLIC_URL}>
            <div>
              <ChatContainer/>
            </div>
      </BrowserRouter>
  );
}

export default App;
