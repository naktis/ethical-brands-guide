import React from "react";
import axios from "axios";
import BrandForm from './BrandForm'
import GenericPage from "../Shared/GenericPage";
import './Create.css';

class CreatePage extends React.Component {
	handleSubmit(brand) {
		axios.post('https://localhost:5001/api/Brand', brand)
		.then(function (response) {
			console.log(response);
		})
		.catch(function (error) {
			console.log(error);
		});
	}
	
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
				<BrandForm brand={ this.emptyBrand() } title="Naujo prekės ženklo kūrimas" handleSubmit={this.handleSubmit}/>
			</GenericPage>
		)
	}
}

export default CreatePage;