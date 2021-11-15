import React from "react";
import axios from "axios";
import { Link } from 'react-router-dom'

class Header extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
		  show: false
		};
		this.handleSubmit = this.handleSubmit.bind(this);
	}

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
				{ this.props.user.token !== "" ? 
				<div id="Header-button-div">
				<Link
					to={{
						pathname: "/create",
						user: this.props.user}}
				>
					<button>SUKURTI ŽENKLĄ</button>
				</Link>
				</div>: <div></div>}
			</header>
		)
	}
}

export default Header;