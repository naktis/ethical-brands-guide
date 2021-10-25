import React from "react";
import Modal from './Modal.js';
import BrandForm from "./Create/BrandForm";
import axios from "axios";
import { Link } from 'react-router-dom'

class Header extends React.Component {
	constructor() {
		super();
		this.state = {
		  show: false,
		  
		};
		this.showModal = this.showModal.bind(this);
		this.hideModal = this.hideModal.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
	}

	showModal = () => {
		this.setState({ show: true });
	};
	
	hideModal = () => {
	this.setState({ show: false });
	};

	emptyBrand() {
		return { 
			name: "",
			description: "",
			categoryId: 0,
			companyId: 0
		}
	}

	handleSubmit(brand) {
		axios.post('https://localhost:44321/api/Brand', brand)
		.then(function (response) {
			console.log(response);
		})
		.catch(function (error) {
			console.log(error);
		});
		this.hideModal();
	}

	refreshPage() {
    window.location.reload(false);
  }

	render() {
		return(
			<header className="App-header">
				<div>
					<Link to="/" className="Title">
						<h1>Etiškų maisto prekių ženklų gidas</h1>
					</Link>
				</div>
				
				<div id="Header-button-div">
					<Link to="/create">
						<button>SUKURTI NAUJĄ</button>
					</Link>
				</div>

				<Modal show={this.state.show} handleClose={this.hideModal} title="Naujo prekės ženklo kūrimas">
					<BrandForm brand={ this.emptyBrand() } handleSubmit={this.handleSubmit}/>
				</Modal>
			</header>
		)
	}
}

//onClick={this.refreshPage}

export default Header;