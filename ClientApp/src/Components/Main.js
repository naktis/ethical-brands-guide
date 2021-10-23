import React from 'react';
import { Switch, Route } from 'react-router-dom';

import HomePage from './HomePage';
import CreatePage from './CreatePage';
import ViewPage from './ViewPage';
import EditPage from './EditPage';

const Main = () => {
  return (
    <Switch> {/* The Switch decides which component to show based on the current URL.*/}
      <Route exact path='/' component={HomePage}></Route>
      <Route exact path="/create" component={CreatePage} />
      <Route exact path="/view" component={ViewPage} />
      <Route exact path="/edit" component={EditPage} />
    </Switch>
  );
}

export default Main;