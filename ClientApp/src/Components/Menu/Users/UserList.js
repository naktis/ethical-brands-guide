import React from "react";
import axios from "axios";
import Pager from "../../Shared/Pager";
import "../../User/User.css";

class UserList extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      users: [],
      nextUsers: []
		};
	}

  componentDidMount() {
    this.fetchUsers(1);
  }

  fetchUsers(page) {
    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.token}` }
    };

    axios.get(`https://localhost:5001/api/User?PageNumber=${page}`, config)
    .then(function(response) {
      _this.setState({
        users: response.data
      });
    }).catch((error) => {
      console.log(error);
    });

    axios.get(`https://localhost:5001/api/User?PageNumber=${page + 1}`, config)
    .then(function(response) {
      _this.setState({ nextUsers: response.data });

      if (_this.pager !== undefined)
        _this.pager.handleNextButtonEnable(response.data);
      }).catch((error) => {
        console.log(error);
    });
  }

  render() {
    return(
      <div className="List-of-editables">
        <label>Aktyvūs naudotojai</label>
        <div className="Item-row-container">
          { this.state.users.map(function (user){
            return <div className="Item-Row" key={user.userId}>
              {user.username} ({user.type})
              </div>
            }) 
          }
          <Pager 
            fetch={ this.fetchUsers.bind(this) }
            ref={(p) => this.pager = p}
          />
        </div>
      </div>
    )
  }
}

export default UserList;