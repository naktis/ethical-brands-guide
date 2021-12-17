import React from "react";
import GenericPage from "../../Shared/GenericPage";
import '../../Edit/Edit.css';
import { Redirect } from "react-router";
import UserForm from "./UserForm";
import UserList from "./UserList";

class UserPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      refreshKey: 0
		};
	}

  refreshUserList() {
    this.setState({
      refreshKey: Math.floor(Math.random() * 1000)
    })
  }

	render() {
		return(
			<GenericPage>
        { this.props.location.user === undefined || 
          this.props.location.user.type !== "Admin"? 
				<Redirect to="/" /> 
				: 
        <div>
          <UserForm 
            token={this.props.location.user.token}
            refreshUserList={this.refreshUserList.bind(this)}
          />
          <UserList 
            token={this.props.location.user.token}
            key={this.state.refreshKey}
          />
        </div>}
			</GenericPage>
		)
	}
}

export default UserPage;