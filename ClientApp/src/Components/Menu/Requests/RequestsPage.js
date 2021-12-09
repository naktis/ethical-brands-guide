import React from "react";
import GenericPage from "../../Shared/GenericPage";
import { Redirect } from "react-router";
import RequestsList from "./RequestsList";
import './Requests.css';

class RequestsPage extends React.Component {
  render() {
    return(
      <GenericPage>
        { 
          this.props.location.user === undefined ? 
            <Redirect to="/" /> 
            : 
            <div className="Requests-page">
              <h1>Prekių ženklų užklausos</h1>
              <RequestsList user={this.props.location.user} />
            </div>
        }
      </GenericPage>
    )
  }
}

export default RequestsPage;