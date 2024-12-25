pub trait Solution {
    fn solve_part_one(input: String) -> String;
    fn solve_part_two(input: String) -> String;
}

#[repr(i32)]
#[derive(Copy, Clone, PartialEq, Eq, Hash)]
pub enum Day {
    D01 = 1,
    D02 = 2,
    D03 = 3,
    D04 = 4,
    D05 = 5,
    D06 = 6,
    D07 = 7,
    D08 = 8,
    D09 = 9,
    D10 = 10,
    D11 = 11,
    D12 = 12,
    D13 = 13,
    D14 = 14,
    D15 = 15,
    D16 = 16,
    D17 = 17,
    D18 = 18,
    D19 = 19,
    D20 = 20,
    D21 = 21,
    D22 = 22,
    D23 = 23,
    D24 = 24,
    D25 = 25,
}

#[repr(i32)]
#[derive(Copy, Clone, PartialEq, Eq, Hash)]
pub enum Part {
    P1 = 1,
    P2 = 2,
}
