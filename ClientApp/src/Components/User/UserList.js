import React from "react";
import axios from "axios";
import "./User.css";

class UserList extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      users: []
		};
	}
  
  componentDidMount() {
    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.token}` }
    }

    axios.get("https://localhost:5001/api/User", config)
    .then(function(response) {
      _this.setState({
        users: response.data
      })
    }).catch((error) => {
      console.log(error);
    })
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
                }) }
          </div>
      </div>
    )
  }
}

export default UserList;