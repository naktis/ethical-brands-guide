import React from "react";
//import axios from "axios";
import BrandForm from './BrandForm'
import GenericPage from "../Shared/GenericPage";
import './Create.css';
import { Redirect } from "react-router";

class CreatePage extends React.Component {
	/*constructor(props) {
		super(props);
		this.state = {
			successMessage: "",
			duplicateMessage: ""
		};

		this.handleSubmit = this.handleSubmit.bind(this);
	}

	handleSubmit(brand) {
		const _this = this;
		axios.post('https://localhost:5001/api/Brand', brand)
		.then(function (response) {
			_this.setState({ successMessage: "Prekės ženklas sėkmingai sukurtas"});
			console.log(response);
		})
		.catch(function (error) {
			_this.setState({ duplicateMessage: "Tokia prekės ženklo ir įmonės kombinacija jau egzistuoja"});
			console.log(error);
		});
	}*/
	
	emptyBrand() {
		return { 
			name: "",
			description: "",
			categoryId: 0,
			companyId: 0
		}
	}

	render() {
		return(
			<GenericPage>
				{ this.props.location.user === undefined ? 
				<Redirect to="/" /> 
				: 
				<BrandForm 
					brand={ this.emptyBrand() } 
					title="Naujo prekės ženklo kūrimas"
					user={ this.props.location.user}
				/> 
				}
			</GenericPage>
		)
	}
}

export default CreatePage;