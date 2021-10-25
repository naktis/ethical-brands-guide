import { Link } from 'react-router-dom'
import './Shared.css';

function BackButton() {
  return(
    <Link to="/"><button type="button" className="Back-button">&#8592;</button></Link>
  )
}

export default BackButton;