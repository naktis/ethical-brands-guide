import React from "react";
import BrandForm from "../Create/BrandForm";
import GenericPage from "../Shared/GenericPage";
import { Redirect } from "react-router";
import axios from "axios";

class EditBrandPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
			successMessage: "",
			duplicateMessage: "",
			key: 0,
      brand: {
        brandId: this.props.location.brand.brandId,
        name: this.props.location.brand.name,
        description: this.props.location.brand.description,
        companyId: this.props.location.brand.company.companyId,
        categories: this.props.location.brand.categories
      }
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

		axios.put(`https://localhost:5001/api/Brand/${id}`, newBrand, config)
		.then(function (response) {
      let categories = [];
      if (brand["categories"] !== null)
        brand["categories"].forEach(o => {
          categories.push({
            categoryId: o.value, 
            name: o.label
          });
      });

      let updatedBrand = {
        name: brand["name"],
        description: brand["description"],
        companyId: brand["companyId"],
        categories: categories,
        brandId: _this.state.brand.brandId
      }

			_this.setState({ 
				successMessage: "Prekės ženklas sėkmingai atnaujintas", 
				duplicateMessage: "",
				key: _this.state.key+1,
        brand: updatedBrand
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

	render() {
		return(
			<GenericPage>
				{ this.props.location.user === undefined ? 
				<Redirect to="/" /> 
				: 
				<BrandForm 
					brand={ this.state.brand } 
					title="Prekės ženklo atnaujinimas"
					user={ this.props.location.user}
          submitMessage="ATNAUJINTI"
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

export default EditBrandPage;