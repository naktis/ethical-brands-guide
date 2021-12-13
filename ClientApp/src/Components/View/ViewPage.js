import React from "react";
import GenericPage from "../Shared/GenericPage";
import BrandDetails from "./BrandDetails";
import './View.css';

class ViewPage extends React.Component {
	render() {
		return (
			<GenericPage>
				<BrandDetails id={this.props.match.params.id} user={this.props.location.user}/>
			</GenericPage>
		);
	}
}

export default ViewPage;