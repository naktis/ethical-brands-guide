import React from "react";
import Modal from './Modal.js';

class Header extends React.Component {
	constructor() {
		super();
		this.state = {
		  show: false,
		  
		};
		this.showModal = this.showModal.bind(this);
		this.hideModal = this.hideModal.bind(this);
	}



	showModal = () => {
		this.setState({ show: true });
	};
	
	hideModal = () => {
	this.setState({ show: false });
	};

	render() {
		return(
			<header className="App-header">
				<div><h1>Etiškų maisto prekių ženklų gidas</h1></div>
				<div id="Header-button-div"><button onClick={this.showModal}>SUKURTI NAUJĄ</button></div>
				<Modal show={this.state.show} handleClose={this.hideModal} title="Naujo prekės ženklo kūrimas">
					<p>Jonas joja ir dainuoja</p>
				</Modal>
			</header>
		)
	}
}

export default Header;