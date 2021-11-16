import React from 'react';
import { Switch, Route } from 'react-router-dom';

import HomePage from './Home/HomePage';
import CreatePage from './Create/CreatePage';
import ViewPage from './View/ViewPage';
import EditPage from './Edit/EditPage';
import EditCategoryPage from './Edit/EditCategoryPage';
import EditCompanyPage from './Edit/EditCompanyPage';
import LoginPage from './User/LoginPage';
import UserPage from './User/UserPage';

const Main = (props) => {
  return (
    <Switch> {/* The Switch decides which component to show based on the current URL.*/}
      <Route exact path='/' component={HomePage}></Route>
      <Route exact path="/create" component={CreatePage}/>
      <Route exact path="/view/:id" component={ViewPage} />
      <Route exact path="/edit" component={EditPage}/>
      <Route exact path="/categories" component={EditCategoryPage}/>
      <Route exact path="/companies" component={EditCompanyPage}/>
      <Route exact path="/users" component={UserPage}/>
      <Route 
        exact path="/login" 
        render={() => <LoginPage 
          handleLogin={props.handleLogin}
        />}
      />
    </Switch>
  );
}

export default Main;