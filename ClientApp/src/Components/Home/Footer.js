import React from "react";
import './Home.css';
import { Link } from 'react-router-dom';

class Footer extends React.Component {
  render() {
    return(
      <footer>
        Nerandate prekės ženklo?
        <Link to="/request">Pasiūlykite jį įvertinti</Link>
      </footer>
    )
  }
}

export default Footer;