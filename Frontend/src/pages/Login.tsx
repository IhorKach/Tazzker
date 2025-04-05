import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import axios from 'axios'
import { Input } from '@/components/ui/input'
import { Button } from '@/components/ui/button'

export default function Login() {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const navigate = useNavigate()

  const handleLogin = async (e: React.FormEvent) => {
    console.log("Получаем токен:") // должно быть просто строка
    e.preventDefault()
    try {
     
      const response = await axios.post("https://localhost:7164/login", {
        username: email,
        password
      })
  
      console.log("Получен токен:", response.data) // должно быть просто строка
  
      localStorage.setItem("token", response.data)
  
      navigate("/tasks")
    } catch (err) {
      console.error("Ошибка при логине:", err)
      setError("Неверный логин или пароль")
    }
  }

  return (
    <form onSubmit={handleLogin} className="max-w-md mx-auto mt-12 space-y-4">
      <h2 className="text-2xl font-bold">Вход</h2>
      {error && <div className="text-red-500 text-sm">{error}</div>}
      <Input
  placeholder="Email или никнейм"
  type="text" // ← Была ошибка. НЕ email!
  value={email}
  onChange={(e) => setEmail(e.target.value)}
/>

      <Input
        placeholder="Пароль"
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <Button type="submit" className="w-full">
        Войти
      </Button>
    </form>
  )
}
