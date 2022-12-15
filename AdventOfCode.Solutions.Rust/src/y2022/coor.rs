#[derive(Copy, Clone, Debug, Eq, PartialEq, Hash)]
pub struct Position<T> {
    pub x: T,
    pub y: T,
}