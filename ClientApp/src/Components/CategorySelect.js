import React from "react";
import axios from "axios";
import SelectOption from "./SelectOption";

class CategorySelect extends React.Component {
    constructor(props) {
		super(props);
		this.state = {
      categories: [],
			setCategory: this.props.setCategory
		};

		this.handleChange = this.handleChange.bind(this);
	}

	componentDidMount() {
    const _this = this;

    axios.get("https://localhost:44321/api/Category").then(function(response) {
      _this.setState({
        categories: response.data
        })
      }).catch((error) => {
        console.log(error);
    })
  }

	handleChange(event) {
		this.setState({
			setCategory: event.target.value
		})
		
	}

	render() {
		return(
			<select name="category" id="category" onChange={this.handleChange}>
				<option value="none">Bet kuri</option>
				{ this.state.categories.map(function (category){
						return <SelectOption category={category} key={category.categoryId}/>
				}) }
      </select>
		)
	}
}

export default CategorySelect;