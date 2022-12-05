pub trait Solution {
    fn solve_part_one(input: String) -> String;
    fn solve_part_two(input: String) -> String;
}

#[repr(i32)]
pub enum Part {
    P1,
    P2,
}
