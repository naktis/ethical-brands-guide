import React from "react";
import GenericPage from "../../Shared/GenericPage";
import { Redirect } from "react-router";
import RequestsList from "./RequestsList";
import axios from "axios";
import './Requests.css';

class RequestsPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = ({
      key: 0
    });
  };

  handleDelete(request) {
    if (window.confirm(`Ar tikrai norite ištrinti prekės ženklo ${request.name} užklausą?`)) {
      const _this = this;

      const config = {
        headers: { Authorization: `Bearer ${this.props.location.user.token}` }
      }

      axios.delete(`https://localhost:5001/api/Request/${request.requestId}`, config).then(function(response) {
        _this.setState({
          key: _this.state.key + 1
        })
        }).catch((error) => {
          console.log(error);
      })
    }
  };

  render() {
    return(
      <GenericPage>
        { 
          this.props.location.user === undefined ? 
            <Redirect to="/" /> 
            : 
            <div className="Requests-page">
              <h1>Prekių ženklų užklausos</h1>
              <RequestsList 
                user={this.props.location.user} 
                key={this.state.key}
                handleDelete={this.handleDelete.bind(this)}
              />
            </div>
        }
      </GenericPage>
    )
  }
}

export default RequestsPage;