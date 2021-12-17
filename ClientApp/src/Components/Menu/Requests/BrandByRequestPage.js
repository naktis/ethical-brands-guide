import React from "react";
import BrandForm from "../../Shared/BrandForm";
import GenericPage from "../../Shared/GenericPage";
import { Redirect } from "react-router";
import axios from "axios";

class BrandByRequestPage extends React.Component {
	_isMounted = false;

  constructor(props) {
		super(props);
		this.state = {
			successMessage: "",
			duplicateMessage: "",
			brand: {
				brandId: this.props.location.request.requestId,
				name: this.props.location.request.name,
				description: this.props.location.request.description,
				categories: [],
				companyId: 0
			},
			createdBrandId: 0,
			key: 0
		};
	};

	componentDidMount() {
    this._isMounted = true;
	}

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
		};

		let categories = [];
		if (brand["categories"] !== null)
			brand["categories"].forEach(o => {
				categories.push({
					categoryId: o.value, 
					name: o.label
				});
			});

		let enteredBrand = {
			name: brand["name"],
			description: brand["description"],
			companyId: brand["companyId"],
			categories: categories,
			brandId: _this.state.brand.brandId
		};

		axios.post(`https://localhost:5001/api/Brand`, newBrand, config)
		.then(function (response) {
			_this.setState({ 
				successMessage: "Prekės ženklas sėkmingai pridėtas", 
				duplicateMessage: "",
				key: _this.state.key+1,
				brand: enteredBrand,
				createdBrandId: response.data.brandId
			});

			console.log(response);
		})
		.catch(function (error) {
			_this.setState({ 
				duplicateMessage: "Tokia prekės ženklo ir įmonės kombinacija jau egzistuoja", 
				successMessage: "",
				brand: enteredBrand,
				key: _this.state.key+1
			});
			console.log(error);
		});
	};

	componentWillUnmount() {
    this._isMounted = false;
  }

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
						<div>
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
						</div>
				}
			</GenericPage>
		)
	}
}

export default BrandByRequestPage;