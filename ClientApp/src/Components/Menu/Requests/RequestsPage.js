import React from "react";
import GenericPage from "../../Shared/GenericPage";
import { Redirect } from "react-router";
import RequestsList from "./RequestsList";
import './Requests.css';

class RequestsPage extends React.Component {
  render() {
    return(
      <GenericPage>
        { this.props.location.user === undefined ? 
          <Redirect to="/" /> 
          : 
          <div className="Requests-page">
            <h1>Prekių ženklų užklausos</h1>
            <RequestsList token={ this.props.location.user.token }/>
          </div>
        }
      </GenericPage>
    )
  }
}

export default RequestsPage;