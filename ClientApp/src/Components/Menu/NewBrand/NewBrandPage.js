import React from "react";
import axios from "axios";
import BrandForm from '../../Shared/BrandForm'
import GenericPage from "../../Shared/GenericPage";
import './NewBrandPage.css';
import { Redirect } from "react-router";

class NewBrandPage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
			successMessage: "",
			duplicateMessage: "",
			key: 0
		};
	};

	handleSubmit(brand, id) {
		const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.location.user.token}` }
    };

		let categoryIds = [];
    if (brand["categories"] !== null)
      brand["categories"].forEach(o => {
        categoryIds.push(o.value);
    	});

		let newBrand = {
			name: brand["name"],
			description: brand["description"],
			companyId: brand["companyId"],
			categoryIds: categoryIds
		}

		axios.post('https://localhost:5001/api/Brand', newBrand, config)
		.then(function (response) {
			_this.setState({ 
				successMessage: "Prekės ženklas sėkmingai sukurtas", 
				duplicateMessage: "",
				key: _this.state.key+1
			});
			console.log(response);
		})
		.catch(function (error) {
			_this.setState({ 
				duplicateMessage: "Tokia prekės ženklo ir įmonės kombinacija jau egzistuoja", 
				successMessage: "",
				key: _this.state.key+1
			});
			console.log(error);
		});
	};
	
	emptyBrand() {
		return { 
			name: "",
			description: "",
			id: 0,
			companyId: 0,
			categories: []
		}
	}

	render() {
		return(
			<GenericPage>
				{ 
					this.props.location.user === undefined ? 
						<Redirect to="/" /> 
						: 
						<BrandForm 
							brand={this.emptyBrand()} 
							title="Naujo prekės ženklo kūrimas"
							user={this.props.location.user}
							submitMessage={"KURTI"}
							successMessage={this.state.successMessage}
							duplicateMessage={this.state.duplicateMessage}
							handleSubmit={this.handleSubmit.bind(this)}
							key={this.state.key}
						/> 
				}
			</GenericPage>
		)
	}
}

export default NewBrandPage;