import { useHistory } from "react-router-dom";
import './Shared.css';

function BackButton() {
  let history = useHistory();

  return(
    <button type="button" className="Back-button" onClick={() => history.goBack()}>&#8592;</button>
  )
}

export default BackButton;