import {ReactElement} from "react"
import { Navigate } from "react-router-dom"


export default function PrivateRoute({ children }: { children: ReactElement }) {
    const token = localStorage.getItem('token')
    return token ? children : <Navigate to="/login" replace />
  }
