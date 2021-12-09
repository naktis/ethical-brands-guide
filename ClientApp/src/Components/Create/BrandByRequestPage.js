import React from "react";
import BrandForm from "../Create/BrandForm";
import GenericPage from "../Shared/GenericPage";
import { Redirect } from "react-router";
import axios from "axios";

class BrandByRequestPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
			successMessage: "",
			duplicateMessage: "",
			key: 0,
			brand: {
				brandId: this.props.location.request.requestId,
				name: this.props.location.request.name,
				description: this.props.location.request.description,
				categories: [],
				companyId: 0
			},
			createdBrandId: 0
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
			categoryIds: categoryIds,
      requestId: id
		}

		axios.post(`https://localhost:5001/api/Brand`, newBrand, config)
		.then(function (response) {
			_this.setState({ 
				successMessage: "Prekės ženklas sėkmingai pridėtas", 
				duplicateMessage: "",
				key: _this.state.key+1,
				createdBrandId: response.data.brandId
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

	title() {
		return `Naujo prekės ženklo kūrimas (Užklausa #${this.props.location.request.requestId})`;
	}

	createdBrandUri() {
		return `/view/${this.state.createdBrandId}`;
	}

	render() {
		return(
			<GenericPage>
				{ 
					this.props.location.user === undefined ? 
						<Redirect to="/" /> 
						: 
						<BrandForm 
							brand={this.state.brand} 
							title={this.title()}
							user={this.props.location.user}
							submitMessage="KURTI"
							successMessage={this.state.successMessage}
							duplicateMessage={this.state.duplicateMessage}
							handleSubmit={this.handleSubmit.bind(this)}
							key={this.state.key}
						/>
				}
				{
					this.state.createdBrandId !== 0 ?
					<Redirect
						to={{
							pathname: this.createdBrandUri(),
							user: this.props.location.user
						}}/>
						:
						null
				}
			</GenericPage>
		)
	}
}

export default BrandByRequestPage;

// to={this.createdBrandUri()