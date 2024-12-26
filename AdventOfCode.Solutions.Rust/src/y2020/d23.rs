use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day23::solve_part_one(input),
        Part::P2 => Day23::solve_part_two(input),
    }
}

struct Cups {
    current: u32,
    lookup: Vec<u32>,
}

impl From<&str> for Cups {
    fn from(value: &str) -> Self {
        let cups: Vec<_> = value.chars().map(|ch| ch.to_digit(10).unwrap()).collect();

        let current = cups[0];
        let mut lookup = vec![];
        lookup.resize(cups.len() + 1, 0);
        for i in 0..cups.len() {
            if i == cups.len() - 1 {
                lookup[cups[i] as usize] = cups[0];
            } else {
                lookup[cups[i] as usize] = cups[i + 1];
            }
        }

        Self { current, lookup }
    }
}

impl Cups {
    fn pop_front(&mut self) -> u32 {
        let front = self.lookup[self.current as usize];
        self.lookup[self.current as usize] = self.lookup[front as usize];
        front
    }

    fn insert(&mut self, after: u32, cup: u32) {
        let next = self.lookup[after as usize];
        self.lookup[after as usize] = cup;
        self.lookup[cup as usize] = next;
    }

    fn do_move(&mut self) {
        let three_cups = [self.pop_front(), self.pop_front(), self.pop_front()];
        let mut destination_cup = self.current - 1;
        loop {
            if three_cups.contains(&destination_cup) {
                destination_cup -= 1;
            } else if destination_cup == 0 {
                destination_cup = *self.lookup.iter().max().unwrap();
            } else {
                break;
            }
        }

        self.insert(destination_cup, three_cups[0]);
        self.insert(three_cups[0], three_cups[1]);
        self.insert(three_cups[1], three_cups[2]);

        self.current = self.lookup[self.current as usize];
    }

    fn get_labels(&self, mut after: u32) -> Vec<u32> {
        let mut labels = vec![];
        while self.lookup[after as usize] != 1 {
            after = self.lookup[after as usize];
            labels.push(after);
        }

        labels
    }

    fn star_product(&self) -> u64 {
        self.lookup[1] as u64 * self.lookup[self.lookup[1] as usize] as u64
    }

    fn expand(&mut self, to: u32) {
        let max = *self.lookup.iter().max().unwrap();
        let mut last = self
            .lookup
            .iter()
            .position(|&cup| cup == self.current)
            .unwrap();

        self.lookup.resize(to as usize + 1, 0);
        for cup in max + 1..to + 1 {
            self.lookup[last] = cup;
            last = cup as usize;
        }

        self.lookup[last] = self.current;
    }
}

struct Day23;

#[allow(unused_variables)]
impl Solution for Day23 {
    fn solve_part_one(input: String) -> String {
        let mut cups = Cups::from(input.as_str());
        (0..100).for_each(|_| cups.do_move());

        cups.get_labels(1)
            .iter()
            .map(|n| n.to_string())
            .collect::<Vec<_>>()
            .join("")
    }

    fn solve_part_two(input: String) -> String {
        let mut cups = Cups::from(input.as_str());
        cups.expand(1_000_000);

        (0..10_000_000).for_each(|_| cups.do_move());
        cups.star_product().to_string()
    }
}
